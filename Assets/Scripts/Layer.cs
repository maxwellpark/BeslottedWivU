using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    public GameObject reelPrefab;
    public List<Reel> reels; 
    public Layer layerBelow;

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
            //int index = System.Array.IndexOf(reels, matchingReels[i]);
            //Destroy(reels[index].transform);
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
        int index = LayerManager.currentReelIndex;
        if (layerBelow != null)
        {
            // Todo: store the currently spinning reel instead 
            // of tracking by index
            if (reels[index].symbolText.text.Equals(layerBelow.reels[index].symbolText.text))
            {
                reels[index].symbolText.color = Color.red; 
                reels.RemoveAt(index);
                Debug.Log("New reels count: " + reels.Count);
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
