using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public Layer[] layers;
    public Layer activeLayer; 

    private IEnumerator enumerator;

    public static event Action<bool> onReelsStopped;
    public static event Action<int> onLayerTransition; 

    private void Start()
    {
        activeLayer = layers[0];
        enumerator = activeLayer.reels.GetEnumerator();
    }

    private void InitLayers()
    {
            
    }

    // Todo: rename to AllReelsStopped and move elsewhere
    private bool AllStopped()
    {
        for (int i = 0; i < activeLayer.reels.Length; i++)
        {
            if (activeLayer.reels[i].isSpinning)
            {
                return false;
            }
        }
        return true;
    }

    private void StartAll()
    {
        for (int i = 0; i < activeLayer.reels.Length; i++)
        {
            activeLayer.reels[i].ToggleState();
        }
    }

    // Todo: put this in Reel or wherever is most suitable 
    private bool SymbolsMatch()
    {
        string symbolToMatch = activeLayer.reels[0].symbolText.text;
        return activeLayer.reels.All(s => s.Equals(symbolToMatch));
    }

    private bool LayerIsDestroyed()
    {
        Debug.Log("Active layer length: " + activeLayer.reels.Length);
        return activeLayer.reels.Length <= 0; 
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
            // Todo: determine whether to start all based on 
            // the current enumerator value 
            if (AllStopped())
            {
                StartAll();
                enumerator.MoveNext();
            }
            else
            {
                Reel reel = enumerator.Current as Reel;
                reel.ToggleState();
                enumerator.MoveNext();

                // Check if the last reel in the sequence has been stopped 
                if (reel.Equals(activeLayer.reels[activeLayer.reels.Length - 1]))
                {
                    enumerator.Reset();
                    onReelsStopped?.Invoke(SymbolsMatch());

                    // Destroy any matching symbols after each spin round 
                    activeLayer.DestroyReelsBelow();

                    if (LayerIsDestroyed())
                    {
                        MoveToNextLayer();
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        HandleSpin();
    }
}
