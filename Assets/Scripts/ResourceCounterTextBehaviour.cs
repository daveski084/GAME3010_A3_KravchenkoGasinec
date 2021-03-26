using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceCounterTextBehaviour : MonoBehaviour
{
    public static int scoreNum;
    Text resourceCounter;

    private void Start()
    {
        resourceCounter = GetComponent<Text>();
        resourceCounter.text = "Score: ";
    }

    public void UpdateCounter(int newNum) // deprecated
    {
        resourceCounter.text = "Score: " + newNum.ToString();
    }

    private void Update()
    {
        resourceCounter.text = "Score: " + scoreNum.ToString();
    }
}
