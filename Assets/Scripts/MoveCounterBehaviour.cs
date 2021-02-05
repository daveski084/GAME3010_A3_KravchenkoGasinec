using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this class is made for keeping track of the number for search moves
public class MoveCounterBehaviour : MonoBehaviour
{
    Text moveCounter;

    private void Start()
    {
        moveCounter = GetComponent<Text>();
        moveCounter.text = "Searches: ";
    }

    public void UpdateCounter(int newNum)
    {
        moveCounter.text = "Searches: " + newNum.ToString();
    }

}
