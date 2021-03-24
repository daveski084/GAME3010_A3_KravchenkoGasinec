using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceCounterTextBehaviour : MonoBehaviour
{
    Text resourceCounter;

    private void Start()
    {
        resourceCounter = GetComponent<Text>();
        resourceCounter.text = "Score: ";
    }

    public void UpdateCounter(int newNum)
    {
        resourceCounter.text = "Score: " + newNum.ToString();
    }
}
