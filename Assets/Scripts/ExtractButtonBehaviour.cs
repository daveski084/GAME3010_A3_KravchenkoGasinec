using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtractButtonBehaviour : MonoBehaviour
{
    Text moveCounter;

    private void Start()
    {
        moveCounter = GetComponent<Text>();
        moveCounter.text = "Extractions: ";
    }

    public void UpdateCounter(int newNum)
    {
        moveCounter.text = "Extractions: " + newNum.ToString();
    }

}
