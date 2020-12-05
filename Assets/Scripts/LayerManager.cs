using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public Layer[] layers;
    public Layer activeLayer; 

    // Todo: use this class to separate layer switching 
    // instead of the individual layer class

    private IEnumerator enumerator;

    public static event Action<bool> onReelsStopped;

    private void Start()
    {
        activeLayer = layers[0];
        enumerator = activeLayer.reels.GetEnumerator();
    }

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
        activeLayer = layers[Array.IndexOf(layers, activeLayer) + 1];
    }

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

                if (reel.Equals(activeLayer.reels[activeLayer.reels.Length - 1]))
                {
                    enumerator.Reset();
                    onReelsStopped?.Invoke(SymbolsMatch());


                }
            }
        }
    }

    private void FixedUpdate()
    {
        HandleSpin();
    }
}
