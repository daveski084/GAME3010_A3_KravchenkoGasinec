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
        moveCounter.text = "Time left: ";
    }

    public void UpdateCounter(float newNum)
    {
        moveCounter.text = "Time left: " + newNum.ToString();
    }

}
