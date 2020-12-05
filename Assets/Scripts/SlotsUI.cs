using UnityEngine;
using UnityEngine.UI;

public class SlotsUI : MonoBehaviour
{
    public LayerManager layerManager;

    public GameObject[] layerGroups;
    public GameObject layerGroupPrefab; 
    public GameObject reelPrefab;
    public GameObject indicatorPrefab; 

    public Text feedbackText;

    // Todo: use sprites 
    public Text currentLayerText;
    public Text currentLayerIndicator;

    // Todo: create  an array of horizontal layout groups to represent the layers 
    // Todo: update arrow indicator based on current layer

    private void Start()
    {
        InitLayerGroups(); 
        SlotMachine.onReelsStopped += UpdateFeedbackText;
    }

    private void InitLayerGroups()
    {
        layerGroups = new GameObject[SlotConstants.layerCount];
        for (int i = 0; i < layerGroups.Length; i++)
        {
            layerGroups[i] = Instantiate(layerGroupPrefab, transform);
            layerGroups[i].name = "Layer Group " + i + 1.ToString();

            GameObject newIndicator = Instantiate(indicatorPrefab, layerGroups[i].transform);
            newIndicator.name = "Indicator " + i + 1.ToString();

            for (int j = 0; j < layerManager.layers[i].reels.Length; j++)
            {
                GameObject newReel = Instantiate(reelPrefab, layerGroups[i].transform);
                newReel.name = "Reel " + j + 1.ToString();
            }
        }
        layerGroups[0].GetComponentInChildren<Text>().text = "->";
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
        layerGroups[currentLayer - 1].GetComponentInChildren<Text>().text = string.Empty;
        layerGroups[currentLayer].GetComponentInChildren<Text>().text = "->";
    }
}
