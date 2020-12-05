﻿using UnityEngine;
using UnityEngine.UI;

// Represents the individual reels (spinning symbols) of the machine 
public class Reel : MonoBehaviour
{
    public bool isSpinning;
    public bool isDestroyed; 
    public float counter; 
    public Text symbolText;

    public void Init()
    {
        isSpinning = false;
        counter = SlotConstants.spinSpeed;
        symbolText.text = SlotConstants.symbols[0];
    }

    public void ToggleState()
    {
        counter = SlotConstants.spinSpeed;
        isSpinning = !isSpinning;
    }

    public void HandleSpin()
    {
        if (isSpinning && !isDestroyed)
        {
            if (counter <= 0)
            {
                int index = System.Array.IndexOf(SlotConstants.symbols, symbolText.text);

                symbolText.text = index < SlotConstants.symbols.Length - 1 ?
                    SlotConstants.symbols[index + 1] : SlotConstants.symbols[0];

                counter = SlotConstants.spinSpeed;
            }
            counter--;
        }
    }

    private void FixedUpdate()
    {
        HandleSpin();
    }
}
