using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverTileBehaviour : MonoBehaviour
{
    public CoverTile selfRef;
           GameObject objRef;
    public CoverTile[,] array;
    int arrayWidth;
    int arrayHeight;

    public GameObject gameManager;

    private void Awake()
    {
        objRef = GameObject.Find("Grid");
        array = objRef.GetComponent<GridController>().coverTileGrid;
        arrayWidth = objRef.GetComponent<GridController>().width;
        arrayHeight = objRef.GetComponent<GridController>().height;
        gameManager = GameObject.Find("GameManager");
    }   

    void OnMouseDown()
    {
        Debug.Log("smth happens");
        if(gameManager.GetComponent<GameManager>().canScan)
        {
            Destroy(gameObject);
            DestroySurroundingOnes();
            gameManager.GetComponent<GameManager>().decreaseSearchNum();
        }
    }

    void DestroySurroundingOnes()
    {
        //bottom left
        if((selfRef.x > 0 && selfRef.y > 0) && array[selfRef.x - 1, selfRef.y - 1] != null)
            Destroy(array[selfRef.x - 1, selfRef.y - 1].gameObject);

        //top right
        if ((selfRef.x < (arrayWidth - 1) && selfRef.y < (arrayHeight - 1) && array[selfRef.x + 1, selfRef.y + 1] != null))
            Destroy(array[selfRef.x + 1, selfRef.y + 1].gameObject);

        //top left
        if ((selfRef.x > 0 && selfRef.y < (arrayHeight - 1)) && array[selfRef.x - 1, selfRef.y + 1] != null)
            Destroy(array[selfRef.x - 1, selfRef.y + 1].gameObject);

        //bottom right
        if ((selfRef.x < (arrayWidth - 1) && selfRef.y > 0) && array[selfRef.x + 1, selfRef.y - 1] != null)
            Destroy(array[selfRef.x + 1, selfRef.y - 1].gameObject);

        //right
        if ((selfRef.x < (arrayWidth - 1)) && array[selfRef.x + 1, selfRef.y] != null)
            Destroy(array[selfRef.x + 1, selfRef.y].gameObject);

        //left
        if ((selfRef.x > 0) && array[selfRef.x - 1, selfRef.y] != null)
            Destroy(array[selfRef.x - 1, selfRef.y].gameObject);

        //top
        if ((selfRef.y < (arrayHeight - 1)) && array[selfRef.x, selfRef.y + 1] != null)
            Destroy(array[selfRef.x, selfRef.y + 1].gameObject);

        //bottom
        if ((selfRef.y > 0) && array[selfRef.x, selfRef.y - 1] != null)
            Destroy(array[selfRef.x, selfRef.y - 1].gameObject);

    }


}
