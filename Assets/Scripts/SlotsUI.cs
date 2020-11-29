using UnityEngine;
using UnityEngine.UI;

public class SlotsUI : MonoBehaviour
{
    public Text feedbackText;

    private void Start()
    {
        SlotMachine.onReelsStopped += UpdateFeedbackText;
    }

    private void UpdateFeedbackText(bool symbolsMatch)
    {
        feedbackText.text = symbolsMatch ? SlotConstants.victoryText : SlotConstants.defeatText;

        // Todo: add a Text widget to the slot machine canvas 
        // Todo: interpolate which symbols were matched 
    }
}
