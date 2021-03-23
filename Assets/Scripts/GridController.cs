using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController instance;

   
    public List<Sprite> characters = new List<Sprite>();
    public bool IsShifting { get; set; }

    [SerializeField]
    public GameObject tilePrefab;//will be working with this one
    public GameObject coverTile;

    public int width;//needed 
    public int height;//needed

    Tile resourcesCentre;//won't be needed
    public Tile[,] gridArray;//wont be needd
    public GameObject[,] coverGrid;//wont be needed
    public CoverTile[,] coverTileGrid;//wont be needed

    private void Awake()
    {
        height = width = 8;
    }

    private void Start()
    {
        instance = GetComponent<GridController>();
        SetUpGrid();
       // SetUpCoverGrid();
    }



    public void SetUpGrid()
    {
        Sprite[] previousLeft = new Sprite[height];
        Sprite previousBelow = null;

        if (gridArray != null)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    //Destroy(gridArray[x, y].tile);
                    gridArray[x, y] = null;
                }
            }
            gridArray = null;
        }
        
        gridArray = new Tile[width, height];

        int counter = 0;

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    GameObject temp = Instantiate(tilePrefab, new Vector3((x - (height / 2) + 0.5f) , (y - (width / 2) + 0.5f) , 0), Quaternion.identity);

                    gridArray[x, y] = temp.GetComponent<Tile>();
                    gridArray[x, y].id = counter;
                    counter++;

                    gridArray[x, y].x = x;
                    gridArray[x, y].y = y;

                    List<Sprite> possibleCharacters = new List<Sprite>(); 
                    possibleCharacters.AddRange(characters); 

                    possibleCharacters.Remove(previousLeft[y]); 
                    possibleCharacters.Remove(previousBelow);


                    Sprite newSprite = possibleCharacters[Random.Range(0, possibleCharacters.Count)];
                    temp.GetComponent<SpriteRenderer>().sprite = newSprite;

                    previousLeft[y] = newSprite;
                    previousBelow = newSprite;


                }
            }


        for(int i = 0; i < (height*width)/51.2f;i++)
        {
            // SetRandSpot();
            //FillOutTiles(resourcesCentre);
        }
        //SetUpCoverGrid();
    }

    public IEnumerator FindNullTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (gridArray[x, y].GetComponent<SpriteRenderer>().sprite == null)
                {
                    yield return StartCoroutine(ShiftTilesDown(x, y));
                    break;
                }
            }
        }
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArray[x, y].GetComponent<Tile>().ClearAllMatches();
            }
        }
    }

    private IEnumerator ShiftTilesDown(int x, int yStart, float shiftDelay = .03f)
    {
        IsShifting = true;
        List<SpriteRenderer> renders = new List<SpriteRenderer>();
        int nullCount = 0;

        for (int y = yStart; y < height; y++)
        {  // 1
            SpriteRenderer render = gridArray[x, y].GetComponent<SpriteRenderer>();
            if (render.sprite == null)
            { // 2
                nullCount++;
            }
            renders.Add(render);
        }

        for (int i = 0; i < nullCount; i++)
        { // 3
            yield return new WaitForSeconds(shiftDelay);// 4
            for (int k = 0; k < renders.Count - 1; k++)
            { // 5
                renders[k].sprite = renders[k + 1].sprite;
                renders[k + 1].sprite = GetNewSprite(x, height - 1); // 6
            }
        }
        IsShifting = false;
    }

    private Sprite GetNewSprite(int x, int y)
    {
        List<Sprite> possibleCharacters = new List<Sprite>();
        possibleCharacters.AddRange(characters);

        if (x > 0)
        {
            possibleCharacters.Remove(gridArray[x - 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (x < width - 1)
        {
            possibleCharacters.Remove(gridArray[x + 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (y > 0)
        {
            possibleCharacters.Remove(gridArray[x, y - 1].GetComponent<SpriteRenderer>().sprite);
        }

        return possibleCharacters[Random.Range(0, possibleCharacters.Count)];
    }


    //void SetRandSpot()
    //{
    //    int tempInt = Random.Range(0, width * height - 1);

    //    for (int x = 0; x < gridArray.GetLength(0); x++)
    //    {
    //        for (int y = 0; y < gridArray.GetLength(1); y++)
    //        {
    //            if (gridArray[x, y].id == tempInt)
    //            {
    //                var tileRenderer = gridArray[x, y].tile.GetComponent<Renderer>();
    //                tileRenderer.material.SetColor("_Color", new Color(0,1,0,1));


    //                gridArray[x, y].intensity = 100;
    //                gridArray[x, y].hasResources = true;
    //                gridArray[x, y].x = x;
    //                gridArray[x, y].y = y;

    //                resourcesCentre = gridArray[x, y];
    //            }
    //        }
    //    }
    //}

    //void FillOutTiles(Tile tile)
    //{

    //    for (int x = 0; x < gridArray.GetLength(0); x++)
    //    {
    //        for (int y = 0; y < gridArray.GetLength(1); y++)
    //        {
    //            if(!gridArray[x,y].hasResources)
    //            {
    //                gridArray[x, y].intensity += 100 / (Mathf.Abs(tile.x - x) + Mathf.Abs(tile.y - y)) / 2;


    //                var tileRenderer = gridArray[x, y].tile.GetComponent<Renderer>();

    //                Color currentColor = gridArray[x, y].GetComponent<Renderer>().material.GetColor("_Color");

    //                float currR = gridArray[x, y].GetComponent<Renderer>().material.GetColor("_Color").r;
    //                float currG = gridArray[x, y].GetComponent<Renderer>().material.GetColor("_Color").g;
    //                float currB = gridArray[x, y].GetComponent<Renderer>().material.GetColor("_Color").b;


    //                float tempR = 1.0f / (Mathf.Abs(tile.x - x) + Mathf.Abs(tile.y - y)) / 2;
    //                float tempG = 0;
    //                float tempB = 1.0f / (Mathf.Abs(tile.x - x) + Mathf.Abs(tile.y - y)) / 2;

    //                tileRenderer.material.SetColor("_Color", new Color(currR - tempR, currG - tempG, currB - tempB));
    //            }

    //        }
    //    }

    //}
    //public void SetUpCoverGrid()
    //{
    //    if (coverTileGrid != null)
    //    {
    //        for (int x = 0; x < coverTileGrid.GetLength(0); x++)
    //        {
    //            for (int y = 0; y < coverTileGrid.GetLength(1); y++)
    //            {
    //                Destroy(coverTileGrid[x, y].tile);
    //                coverTileGrid[x, y] = null;
    //            }
    //        }
    //        coverTileGrid = null;
    //    }


    //    coverTileGrid = new CoverTile[width, height];

    //    int counter = 0;

    //    for (int x = 0; x < coverTileGrid.GetLength(0); x++)
    //    {
    //        for (int y = 0; y < coverTileGrid.GetLength(1); y++)
    //        {

    //            GameObject temp = Instantiate(coverTile, new Vector3((x - (height / 2) + 0.5f)*0.5f, (y - (width / 2) + 0.5f) * 0.5f, 0), Quaternion.identity);
    //            coverTileGrid[x, y] = temp.GetComponent<CoverTile>();
    //            coverTileGrid[x, y].id = counter;
    //            counter++;

    //            coverTileGrid[x, y].x = x;
    //            coverTileGrid[x, y].y = y;
    //        }
    //    }
    //}
}

