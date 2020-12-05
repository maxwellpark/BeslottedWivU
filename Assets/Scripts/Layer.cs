﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    public Reel[] reels;
    public Layer layerBelow;

    public bool isActive;

    public Reel[] GetMatchesUnderneath()
    {
        Reel[] matchingReels = new Reel[] { };

        for (int i = 0; i < reels.Length; i++)
        {
            if(reels[i].symbolText.text.Equals(layerBelow.reels[i].symbolText.text))
            {
                matchingReels[matchingReels.Length - 1] = reels[i];
            }
        }
        return matchingReels;
    }

    public void DestroyReelsBelow(Reel[] matchingReels) 
    {
        for (int i = 0; i < matchingReels.Length; i++)
        {
            Destroy(reels[i].transform);
            Debug.Log(layerBelow.reels.Length);
        }
    }
}
