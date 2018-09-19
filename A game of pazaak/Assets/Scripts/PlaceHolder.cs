using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour {

    public bool hasCard = false;

    public void SetPHCardFlag()
    {
        hasCard = !hasCard;
    }
}
