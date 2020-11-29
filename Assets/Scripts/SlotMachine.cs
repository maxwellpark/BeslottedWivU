using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    public Reel[] reels; 

    void Start()
    {
        
    }

    private bool AllStopped()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            if (reels[i].isSpinning)
            {
                return false; 
            }
        }
        return true;
    }

    void Update()
    {
        
    }
}
