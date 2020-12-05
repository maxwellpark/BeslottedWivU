using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    public Reel[] reels;
    public Layer layerBelow;

    public bool isActive;

    public Reel[] GetMatchesUnderneath()
    {


        return new Reel[] { };
    }
}
