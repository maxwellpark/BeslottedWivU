using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public GameObject layerPrefab; 
    public Layer[] layers;
    public Layer activeLayer; 

    public static int currentReelIndex;

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
        for (int i = 0; i < layers.Length; i++)
        {
            try
            {
                layers[i].layerBelow = layers[i + 1];
            }
            catch (IndexOutOfRangeException ex)
            {
                Debug.Log(ex); 
                layers[i].layerBelow = null; 
            }
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

    // Todo: put this in Reel or wherever is most suitable 
    private bool SymbolsMatch()
    {
        string symbolToMatch = activeLayer.reels[0].symbolText.text;
        return activeLayer.reels.All(s => s.Equals(symbolToMatch));
    }

    private bool LayerIsDestroyed()
    {
        return activeLayer.reels.All(r => r.symbolText.color.Equals(Color.red));

        //Debug.Log("Active layer length: " + activeLayer.reels.Length);
        //return activeLayer.reels.Length <= 0; 
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
            if (AllReelsStopped())
            {
                StartAllReels();
            }
            else
            {
                activeLayer.reels[currentReelIndex].ToggleState();

                // Check if the last reel in the sequence has been stopped 
                if (currentReelIndex >= activeLayer.reels.Count - 1)
                {
                    onReelsStopped?.Invoke(SymbolsMatch());

                    // Destroy any matching symbols after each spin round 
                    activeLayer.DestroyMatchingSymbol();

                    if (LayerIsDestroyed())
                    {
                        MoveToNextLayer();
                    }
                    currentReelIndex = 0;
                    return; 
                }
                currentReelIndex++; 
            }
        }
    }

    private void FixedUpdate()
    {
        HandleSpin();
    }
}
