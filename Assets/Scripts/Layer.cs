using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    public GameObject reelPrefab;
    public List<Reel> reels;
    public LayerManager layerManager;
    public Layer layerBelow;
    //public Layer layerAbove;

    // Todo: put this into LayerManager
    public static event System.Action onBottomLayerCleared;

    public bool isActive;

    public void InitReels()
    {
        reels = new List<Reel>();
        for (int i = 0; i < SlotConstants.reelCount; i++)
        {
            GameObject newReel = Instantiate(reelPrefab);
            newReel.name = "Reel " + (i + 1).ToString();
            Reel reel = newReel.GetComponent<Reel>();
            reels.Add(reel);
            reel.Init();

        }
    }

    // Todo: return only the current column?
    public Reel[] GetMatchingSymbolsUnderneath()
    {
        Reel[] matchingReels = new Reel[] { };

        if (layerBelow != null)
        {
            for (int i = 0; i < reels.Count; i++)
            {
                if(reels[i].symbolText.text.Equals(layerBelow.reels[i].symbolText.text))
                {
                    matchingReels[matchingReels.Length - 1] = reels[i];
                }
            }
        }
        Debug.Log("Matching reels count: " + matchingReels.Length);
        return matchingReels; 
    }

    public void DestroyReelsBelow() 
    {
        Reel[] matchingReels = GetMatchingSymbolsUnderneath();

        if (matchingReels.Length <= 0)
        {
            return; 
        }

        for (int i = 0; i < matchingReels.Length; i++)
        {
            //int reelIndex = System.Array.IndexOf(reels, matchingReels[i]);
            //Destroy(reels[reelIndex].transform);
            //Debug.Log("Layer below length: " + layerBelow.reels.Length);
        }
    }

    // Temporary method until we figure out a UI solution 
    // for destroying the game objects themselves 
    public void DestroyMatchingSymbols()
    {
        if (layerBelow != null)
        {
            for (int i = 0; i < reels.Count; i++)
            {
                if (reels[i].symbolText.text.Equals(layerBelow.reels[i].symbolText.text))
                {
                    reels[i].isDestroyed = true;
                    reels[i].symbolText.color = Color.red;
                }
            }
        }
    }

    // Todo: Choose whether to destroy all at the end of round, 
    // or individually after each reel stops 
    public void DestroyMatchingSymbol()
    {
        // Todo: just get a reference to the reel above/below...
        int reelIndex = layerManager.currentReelIndex;

        // Todo: encapsulate this in the Manager class 
        // Make a GetLayerIndex method
        int layerIndex = System.Array.IndexOf(layerManager.layers, layerManager.layers);

        if (layerBelow != null)
        {
            // Todo: store the currently spinning reel instead 
            // of tracking by reelIndex
            if (reels[reelIndex].symbolText.text.Equals(layerBelow.reels[reelIndex].symbolText.text))
            {
                // Todo: encapsulate this in the Reel or Layer class
                reels[reelIndex].isDestroyed = true; 
                reels[reelIndex].symbolText.color = Color.red; 
                reels.RemoveAt(reelIndex);

                // Account for the reduction in list size 
                layerManager.currentReelIndex--;

                Debug.Log("New reels size: " + reels.Count);
            }
        }
        // We are at the bottom layer if this executes
        else
        {
            List<Reel> reelsAbove = layerManager.layers[layerIndex].reels; 

            // Todo: track entirely by reelIndex instead of layerBelow referencing  
            // Find out how to do this with getting reference to the layer manager
            if (reels[reelIndex].symbolText.text.Equals(reelsAbove[reelIndex].symbolText.text))
            {
                reelsAbove[reelIndex].isDestroyed = true;
                reelsAbove[reelIndex].symbolText.color = Color.red;
                reelsAbove.RemoveAt(reelIndex);
            }
            if (reelsAbove.Count <= 0)
            {
                onBottomLayerCleared?.Invoke();
            }
        }
    }

    private void LogLayerElements(Reel[] reels)
    {
        for (int i = 0; i < reels.Length; i++)
        {
            Debug.Log(reels[i]);
        }
    }
}
