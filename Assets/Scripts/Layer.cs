using UnityEngine;

public class Layer : MonoBehaviour
{
    public GameObject reelPrefab;
    public Reel[] reels;
    public Layer layerBelow;

    public bool isActive;

    private void Awake()
    {
    }

    public void InitReels()
    {
        reels = new Reel[SlotConstants.reelCount];
        for (int i = 0; i < reels.Length; i++)
        {
            GameObject newReel = Instantiate(reelPrefab);
            newReel.name = "Reel " + i + 1.ToString();
            Reel reel = newReel.GetComponent<Reel>();
            reels[i] = reel;
            reel.Init();

        }
    }

    // Todo: return only the current column?
    public Reel[] GetMatchingSymbolsUnderneath()
    {
        LogLayerElements(reels);

        Reel[] matchingReels = new Reel[] { };

        if (layerBelow != null)
        {
            for (int i = 0; i < reels.Length; i++)
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
            int index = System.Array.IndexOf(reels, matchingReels[i]);
            Destroy(reels[index].transform);
            Debug.Log("Layer below length: " + layerBelow.reels.Length);
        }
    }

    // Temporary method until we figure out a UI solution 
    // for destroying the game objects themselves 
    public void DestroyMatchingSymbols()
    {
        if (layerBelow != null)
        {
            for (int i = 0; i < reels.Length; i++)
            {
                if (reels[i].symbolText.text.Equals(layerBelow.reels[i].symbolText.text))
                {
                    reels[i].isDestroyed = true;
                    reels[i].symbolText.color = Color.red;
                }
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
