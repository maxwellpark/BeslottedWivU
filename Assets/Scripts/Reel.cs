using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Represents the individual reels (spinning symbols) of the machine 
public class Reel : MonoBehaviour
{
    public bool isSpinning;
    public bool isDestroyed; 
    public float counter;

    // Todo: change to List 
    // Then we can add or remove while playing 
    public string[] symbols;
    
    public Text symbolText;

    public void Init()
    {
        isSpinning = false;
        counter = SlotConstants.spinSpeed;
        InitSymbols();
        symbolText.text = symbols[0];
    }

    private void InitSymbols()
    {
        symbols = DeepCopySymbols(SlotConstants.symbols);
        RandomiseSymbols();
        for (int i = 0; i < symbols.Length; i++)
        {
            Debug.Log("Symbol at index " + i + ": " + symbols[i]);
        }
    }

    // Todo: do we need a symbols class?
    private void RandomiseSymbols()
    {
        for (int i = 0; i < symbols.Length; i++)
        {
            string temp = symbols[i];
            int randomIndex = Random.Range(0, symbols.Length - 1);
            symbols[i] = symbols[randomIndex];
            symbols[randomIndex] = temp;
        }
    }

    private string[] DeepCopySymbols(string[] symbols)
    {
        string[] copiedSymbols = new string[symbols.Length];
        for (int i = 0; i < copiedSymbols.Length; i++)
        {
            copiedSymbols[i] = symbols[i];
        }
        return copiedSymbols;
    }

    public void ToggleState()
    {
        counter = SlotConstants.spinSpeed;
        symbols = SlotConstants.symbols;
        isSpinning = !isSpinning;
    }

    public void HandleSpin()
    {
        if (isSpinning && !isDestroyed)
        {
            if (counter <= 0)
            {
                int index = System.Array.IndexOf(SlotConstants.symbols, symbolText.text);
                symbolText.text = index < symbols.Length - 1 ? symbols[index + 1] : symbols[0];
                counter = SlotConstants.spinSpeed;
            }
            counter--;
        }
    }

    private void Update()
    {
        HandleSpin();
    }
}
