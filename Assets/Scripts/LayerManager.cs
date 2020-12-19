using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public GameObject layerPrefab; 
    public Layer[] layers;
    public Layer activeLayer; 

    public int currentReelIndex;
    // Store a list of not-destroyed reels, then iterate through?

    public static event Action<bool> onReelsStopped;
    public static event Action<int> onLayerTransition;

    private void Awake()
    {
        InitLayers();
    }

    private void InitLayers()
    {
        layers = new Layer[SlotConstants.layerCount];

        for (int i = 0; i < layers.Length; i++)
        {
            GameObject newLayerObject = Instantiate(layerPrefab);
            newLayerObject.name = "Layer " + (i + 1).ToString();

            Layer newLayer = newLayerObject.GetComponent<Layer>();
            newLayer.layerManager = this;
            //newLayer.layerBelow = i < layers.Length - 1 ? layers[i + 1] : null;
            layers[i] = newLayer;
            
            newLayer.InitReels();
        }

        SetLayersBelow();
        activeLayer = layers[0];
    }

    // Todo: assign layers below in the init layers for loop 
    // Why are they always null with current solution?
    // Shouldn't need to wait, as they are reference types 
    private void SetLayersBelow()
    {
        for (int i = 0; i < layers.Length - 1; i++)
        {
            layers[i].layerBelow = layers[i + 1];
        }
    }

    // Todo: move reel start-stop logic elsewhere
    private bool AllReelsStopped()
    {
        return activeLayer.reels.TrueForAll(r => !r.isSpinning);
    }

    private void StartAllReels()
    {
        activeLayer.reels.ForEach(r => r.isSpinning = true);
    }

    private int GetIndexIncrement()
    {
        int increment = 1;
        for (int i = currentReelIndex; i < activeLayer.reels.Count - 1; i++)
        {
            if (activeLayer.reels[i + 1].isDestroyed) 
            {
                increment++;
            }
            else
            {
                return increment; 
            }
        }
        return increment; 
    }

    // Todo: Refactor this 
    // Can we just return the reel, and then check above/below based on that?
    private int GetNextReelIndex()
    {
        // Can we make this work for end-of-round stage too?
        int nextHighest = activeLayer.reels.FirstOrDefault(r => !r.isDestroyed && r.index > currentReelIndex).index;
        return nextHighest > 0 ? nextHighest : activeLayer.reels.FirstOrDefault(r => !r.isDestroyed).index;
    }

    // Todo: null checks
    private int GetNextReelIndexFor()
    {
        for (int i = activeLayer.reels.Count - 1; i >= 0; i--)
        {
            if (!activeLayer.reels[i].isDestroyed && activeLayer.reels[i].isSpinning 
                && activeLayer.reels[i] != activeLayer.reels[currentReelIndex])
            {
                return i;
            }
        }
        return currentReelIndex;
    }

    private int GetNextIndexInSubLists()
    {
        IEnumerable<Reel> lastPortion = activeLayer.reels.Where(r => r.index > currentReelIndex);
        Reel reelInLastPortion = lastPortion.FirstOrDefault(r => !r.isDestroyed);
        if (reelInLastPortion!= null) //nc?
        {
            return reelInLastPortion.index;
        }
        else
        {
            IEnumerable<Reel> firstPortion = activeLayer.reels.Where(r => r.index <= currentReelIndex);
            Reel reelInFirstPortion = firstPortion.FirstOrDefault(r => !r.isDestroyed);
            if (reelInFirstPortion != null)
            {
                return reelInFirstPortion.index;
            }
        }
        return 0;
    }

    // Todo: put this in Reel or wherever is most suitable 
    // Make this a part of the Layer MatchSymbols method?
    private bool SymbolsMatch()
    {
        string symbolToMatch = activeLayer.reels[0].symbolText.text;
        return activeLayer.reels.All(s => s.Equals(symbolToMatch));
    }

    private bool LayerIsDestroyed()
    {
        return activeLayer.reels.All(r => r.isDestroyed);
    }

    private void MoveToNextLayer()
    {
        int newActiveLayerIndex = Array.IndexOf(layers, activeLayer) + 1;
        activeLayer = layers[newActiveLayerIndex];
        onLayerTransition?.Invoke(newActiveLayerIndex);
    }

    // Todo: encapsulate more of this logic 
    private void HandleSpin()
    {   
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Check to see if we are beginning the spin round
            if (AllReelsStopped())
            {
                StartAllReels();
            }
            else
            {
                activeLayer.reels[currentReelIndex].ToggleState();
                activeLayer.DestroyMatchingSymbol();

                // Check to see if we are ending the spin round
                if (AllReelsStopped())
                {
                    onReelsStopped?.Invoke(SymbolsMatch());

                    if (LayerIsDestroyed())
                    {
                        MoveToNextLayer();
                    }
                    //currentReelIndex = 0;
                    //return; 
                }
                // Skip reels that have already been destroyed
                //currentReelIndex = GetNextReelIndexFor();
                //currentReelIndex += GetIndexIncrement();
                //currentReelIndex = GetNextReelIndex();

                // Determine next reel to cycle to 
                if (activeLayer.reels.Count() <= 1)
                {
                    currentReelIndex = GetNextReelIndexFor();
                }
                // Don't change index if there's only one reel remaining
            }
        }
    }

    private void Update()
    {
        HandleSpin();
    }
}
