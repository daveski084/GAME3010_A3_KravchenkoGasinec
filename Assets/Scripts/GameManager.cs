using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public Canvas gameCanvas;
    public Canvas endCanvas;

    public bool canScan;
    public bool canExtract;
    public GameObject movesNumber; //this is going to be the number for showing how many moves left
    public GameObject extractCounter;//in A3 it is a timer
    public GameObject resourceCounter;//in A3 it is a score counter
    public GameObject resourcesCounterEnd;//supposed to be used for a game over scene


    public float timeLeft { get; set; }//says for itself
    public int searchNum { get; set; }// in A3 ir is a number for showing how many moves left
    public int resourcesGathered { get; set; }//in A3 it is a score

    public bool canSwitchScene;
    private void Awake()
    {
        timeLeft = 15;
        instance = GetComponent<GameManager>();
        canExtract = true;
        canScan = false;
        searchNum = 6;
        timeLeft = 15.0f;
        resourcesGathered = 0;
        StartNewGameSceneSwitch();
    }


    

    void Start()
    {
        canSwitchScene = false;
    }

    public void ResetStats()
    {
        canExtract = true;
        canScan = false;
        searchNum = 6;
        timeLeft = 15.0f;
        resourcesGathered = 0;
        
        StartNewGameSceneSwitch();
    }


    void Update()
    {
        if (timeLeft > 0.001)
            timeLeft -= Time.deltaTime;
        else
            timeLeft = 0.0f;


        resourceCounter.GetComponent<ResourceCounterTextBehaviour>().UpdateCounter(resourcesGathered);//updates the score

        extractCounter.GetComponent<ExtractButtonBehaviour>().UpdateCounter(timeLeft);//updates the timer 

        movesNumber.GetComponent<MoveCounterBehaviour>().UpdateCounter(searchNum);//updates the number of moves left

            if (canSwitchScene)
            {
                canSwitchScene = false;
                EndGameSceneSwitch();
                resourcesCounterEnd.GetComponent<ResourceCounterTextBehaviour>().UpdateCounter(resourcesGathered);
            }
       
    }


    public void decreaseSearchNum()//deleting this causes error, A1 mechanic's related function, procceed carefully
    {
        if(canScan)
        searchNum--;
    }

    public void decreaseExtractNum()//deleting this causes error, A1 mechanic's related function, procceed carefully
    {
        if(canExtract)
        //extractNum--;
        if (timeLeft < 1)
            canSwitchScene = true;
    }




    public void AddResourceBalance(int num)//deleting this causes error, A1 mechanic's related function, procceed carefully
    {
        resourcesGathered += num;
        Debug.Log(resourcesGathered);
    }


    public void StartNewGameSceneSwitch()
    {
        gameCanvas.gameObject.SetActive(true);
        endCanvas.gameObject.SetActive(false);
    }

    public void EndGameSceneSwitch()
    {
        
        gameCanvas.gameObject.SetActive(false);
        endCanvas.gameObject.SetActive(true);
       
    }


}
