using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    public bool isSpinning;
    public float counter; 
    public Text symbolText;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        isSpinning = true;
        counter = SlotConstants.spinSpeed;
        symbolText.text = SlotConstants.symbols[0];
        Application.targetFrameRate = SlotConstants.frameRate;
    }

    private void FixedUpdate()
    {
        if (isSpinning)
        {
            if (counter <= 0)
            {
                int index = System.Array.IndexOf(SlotConstants.symbols, symbolText.text);
                symbolText.text = index < SlotConstants.symbols.Length - 1 ? SlotConstants.symbols[index + 1] : SlotConstants.symbols[0];

                counter = SlotConstants.spinSpeed;
            }
            counter--;
        }
    }
}
