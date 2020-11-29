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
            reels[i].ToggleState(); 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
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

                if (reel.Equals(reels[reels.Length -1]))
                {
                    enumerator.Reset();

                    // Todo: check if 3 symbols match
                }
            }
        }
    }
}
