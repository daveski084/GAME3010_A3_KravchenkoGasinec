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
                    Destroy(gridArray[x, y].gameObject);
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

                    temp.transform.parent = transform; // 1
                    Sprite newSprite = possibleCharacters[Random.Range(0, possibleCharacters.Count)];
                    temp.GetComponent<SpriteRenderer>().sprite = newSprite;

                    previousLeft[y] = newSprite;
                    previousBelow = newSprite;

                }
            }
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
        {  
            SpriteRenderer render = gridArray[x, y].GetComponent<SpriteRenderer>();
            if (render.sprite == null)
            { 
                nullCount++;
            }
            renders.Add(render);
        }

        for (int i = 0; i < nullCount; i++)
        { 
            yield return new WaitForSeconds(shiftDelay);
            for (int k = 0; k < renders.Count - 1; k++)
            { 
                renders[k].sprite = renders[k + 1].sprite;
                renders[k + 1].sprite = GetNewSprite(x, height - 1); 
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
}

