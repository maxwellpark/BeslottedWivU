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
        int indexRange = SlotConstants.symbols.Length - 1;
        int count = indexRange; 
        symbols = new string[indexRange];

        // Shuffle elements  
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, indexRange);
            symbols[i] = SlotConstants.symbols[randomIndex];
            indexRange--;
        }
    }

    // Todo: do we need a symbols class?
    private void RandomiseSymbols()
    {

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
