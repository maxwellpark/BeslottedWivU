using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public bool isSpinning;
    public char[] symbols; 
    public Text symbolText;

    private void Start()
    {
        symbols = new char[] { '#', '!', '@', '£', '$', '%', '&' };
    }
}
