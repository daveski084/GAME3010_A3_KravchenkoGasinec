using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public Canvas gameCanvas;
    public Canvas endCanvas;
    public Canvas difficultyCanvas;
    public Canvas endCanvasWin;

    public bool canScan;
    public bool canExtract;
    public GameObject movesNumber; //this is going to be the number for showing how many moves left
    public GameObject timer;//in A3 it is a timer
    public GameObject scoreCounter;//in A3 it is a score counter
    // Renamed it so it is more readable. - Dave
    public GameObject resourcesCounterEnd;//supposed to be used for a game over scene
    public int scoreEnd;
    public int goal;
    public static int goalSet;
    public static bool conditionsMet;
    public static bool canTimerStart, isDifficultySet, startButtonPressed;
    public static int moves;
   
  

    public float timeLeft { get; set; }//says for itself
    public int movesLeft { get; set; }// in A3 ir is a number for showing how many moves left
    // Renamed it so it is more readable. - Dave
    public int resourcesGathered { get; set; }//in A3 it is a score

    public bool canSwitchScene;
    private void Awake()
    {
        instance = GetComponent<GameManager>();
        canExtract = true;
        canScan = false;
       // movesLeft = moves;
        timeLeft = 60.0f;
        resourcesGathered = 0;
        conditionsMet = false;
       StartNewGameSceneSwitch();
    }
    void Start()
    {
       // movesLeft = moves;
        canSwitchScene = false;
        canTimerStart = false;
    }

    public void ResetStats()
    {
        canExtract = true;
        canScan = false;
        movesLeft = moves;
        timeLeft = 60.0f;
        resourcesGathered = 0;
        goal = goalSet;
        StartNewGameSceneSwitch();
    }


    void Update()
    {
        movesLeft = moves;
        goal = goalSet;

        //print(goal);

        if (startButtonPressed == true)
        {
            SetDifficultyCanvas(true);

            if (isDifficultySet == true)
            {
                SetDifficultyCanvas(false);

                if (canTimerStart == true)
                {
                    if (timeLeft > 0.001)
                        timeLeft -= Time.deltaTime;
                    else
                    {
                        timeLeft = 0.0f;
                        canSwitchScene = true;
                    }
                }
                if(movesLeft <= 0)
                {
                    conditionsMet = false;
                    canSwitchScene = true;
                }
                if (ResourceCounterTextBehaviour.scoreNum >= goal && movesLeft >= 0)
                {
                    timeLeft = 0.0f;
                    conditionsMet = true;
                    canSwitchScene = true;
                }

                scoreCounter.GetComponent<ResourceCounterTextBehaviour>().UpdateCounter(resourcesGathered);//updates the score
                timer.GetComponent<ExtractButtonBehaviour>().UpdateCounter(timeLeft);//updates the timer 
                movesNumber.GetComponent<MoveCounterBehaviour>().UpdateCounter(movesLeft);//updates the number of moves left

                if (canSwitchScene)
                {
                    if (conditionsMet == true)
                    {
                        gameCanvas.gameObject.SetActive(false);
                        endCanvasWin.gameObject.SetActive(true);
                    }
                    else
                    {
                        canSwitchScene = false;
                        EndGameSceneSwitch();
                        //resourcesCounterEnd.GetComponent<ResourceCounterTextBehaviour>().UpdateCounter(resourcesGathered);
                    }
                }

            }

        } 
       
    }


    public void decreaseSearchNum()//deleting this causes error, A1 mechanic's related function, procceed carefully
    {
        if(canScan)
        movesLeft--;
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
        endCanvasWin.gameObject.SetActive(false);
    }

    public void EndGameSceneSwitch()
    {
        gameCanvas.gameObject.SetActive(false);
        endCanvas.gameObject.SetActive(true);
    }

    public void SetDifficultyCanvas(bool status)
    {
        difficultyCanvas.gameObject.SetActive(status);
    }

}
