using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Todo: make this class handle higher-level operations. 
// LayerManager has superseded it.
// Remove redundant code. 
public class SlotMachine : MonoBehaviour
{
    public Reel[] reels;
    private IEnumerator enumerator;

    public static event Action<bool> onReelsStopped; 

    private void Start()
    {
        Application.targetFrameRate = SlotConstants.frameRate;
    }

    private void ResetMachine()
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

    private void StartAll()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].ToggleState(); 
        }
    }

    private bool SymbolsMatch()
    {
        string symbolToMatch = reels[0].symbolText.text;
        return reels.All(s => s.Equals(symbolToMatch));
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    // Todo: determine whether to start all based on 
        //    // the current enumerator value 
        //    if (AllStopped())
        //    {
        //        StartAll();
        //        enumerator.MoveNext();
        //    }
        //    else
        //    {
        //        Reel reel = enumerator.Current as Reel;
        //        reel.ToggleState();
        //        enumerator.MoveNext();

        //        if (reel.Equals(reels[reels.Length -1]))
        //        {
        //            enumerator.Reset();
        //            onReelsStopped?.Invoke(SymbolsMatch());
        //        }
        //    }
        //}
    }
}
