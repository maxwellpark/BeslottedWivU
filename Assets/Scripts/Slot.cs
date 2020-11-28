using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public bool isSpinning;
    public float spinSpeed;
    public float counter; 
    public string[] symbols; 
    public Text symbolText;


    private void Start()
    {
        spinSpeed = 256; 
        counter = spinSpeed;
        symbols = new string[] { "#", "!", "@", "£", "$", "%", "&" };
        symbolText.text = symbols[0];
    }

    private void FixedUpdate()
    {
        if (isSpinning)
        {
            if (counter > 0)
            {
                counter--;
            }
            int index = System.Array.IndexOf(symbols, symbolText.text);
            symbolText.text = index < symbols.Length - 1 ? symbols[index + 1] : symbols[0];

            counter = spinSpeed;
        }
    }
}
