using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas gameCanvas;
    public Canvas endCanvas;

    public bool canScan;
    public bool canExtract;
    public GameObject searchCounter;
    public GameObject extractCounter;
    public GameObject resourceCounter;
    public GameObject resourcesCounterEnd;
    public int searchNum;
    public int extractNum;
    public int resourcesGathered;

    public bool canSwitchScene;
    private void Awake()
    {
        canExtract = true;
        canScan = false;
        searchNum = 6;
        extractNum = 3;
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
        extractNum = 3;
        resourcesGathered = 0;
        
        StartNewGameSceneSwitch();
    }


    void Update()
    {

        resourceCounter.GetComponent<ResourceCounterTextBehaviour>().UpdateCounter(resourcesGathered);
       


        extractCounter.GetComponent<ExtractButtonBehaviour>().UpdateCounter(extractNum);
    
        searchCounter.GetComponent<MoveCounterBehaviour>().UpdateCounter(searchNum);




        if (searchNum < 1)
            canScan = false;

        if (extractNum < 1)
            canExtract = false;

            if (canSwitchScene)
            {
                canSwitchScene = false;
                EndGameSceneSwitch();
                resourcesCounterEnd.GetComponent<ResourceCounterTextBehaviour>().UpdateCounter(resourcesGathered);
            }
       
    }


    public void decreaseSearchNum()
    {
        if(canScan)
        searchNum--;
    }

    public void decreaseExtractNum()
    {
        if(canExtract)
        extractNum--;
        if (extractNum < 1)
            canSwitchScene = true;

    }


    public void ToggleScan()
    {
        if(canScan != true)
        {
            canScan = true;
            canExtract = false;
        }
    }

    public void ToggleExtract()
    {
        if (canExtract != true)
        {
            canScan = false;
            canExtract = true;
        }
    }

    public void AddResourceBalance(int num)
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
