using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    public Reel[] reels;
    private IEnumerator enumerator;

    void Start()
    {
        enumerator = reels.GetEnumerator();
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

    private void StartAll()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].isSpinning = true; 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Rator = " + enumerator);

            if (AllStopped())
            {
                StartAll();
                enumerator.Reset();
            }
            else
            {

            }
        }
    }
}
