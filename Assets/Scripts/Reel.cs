using UnityEngine;
using UnityEngine.UI;

// Represents the individual reels (spinning symbols) of the machine 
public class Reel : MonoBehaviour
{
    public bool isSpinning;
    public bool isDestroyed; 
    public float counter;

    public string[] symbols; 
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

    private void FixedUpdate()
    {
        HandleSpin();
    }
}
