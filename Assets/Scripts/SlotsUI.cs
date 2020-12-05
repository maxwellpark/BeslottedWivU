using UnityEngine;
using UnityEngine.UI;

public class SlotsUI : MonoBehaviour
{
    public Text feedbackText;

    // Todo: use sprites 
    public Text currentLayerText;
    public Text currentLayerIndicator;

    // Todo: create  an array of horizontal layout groups to represent the layers 
    // Todo: update arrow indicator based on current layer

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

    private void UpdateLayerText(int currentLayer)
    {
        currentLayerIndicator.text = currentLayer + 1.ToString();
    }

    private void UpdateLayerIndicator(int currentLayer)
    {

    }
}
