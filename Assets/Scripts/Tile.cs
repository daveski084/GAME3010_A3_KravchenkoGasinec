using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x = 0;
    public int y = 0;
    GameObject tile;
    public int id = 0;
    public int intensity = 0;
    public bool hasResources = false;
    public bool isSelected;

    private static Color selectedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    private SpriteRenderer render;
    private static Tile previousSelected;
    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private bool matchFound = false;


    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    void OnMouseDown()
    {
        
        if (render.sprite == null || GridController.instance.IsShifting)
        {
            return;
        }

        if (isSelected)
        { 
            DeSelect();
        }
        else
        {
            if (previousSelected == null)
            { 
                Select();
                
            }
                else
                {
                    SwapSprite(previousSelected.render);
                    previousSelected.DeSelect();
                }
            
        }
    }


    private void Select()
    {
        isSelected = true;
        render.color = selectedColor;
        previousSelected = gameObject.GetComponent<Tile>();
    }

    private void DeSelect()
    {
        render.color = Color.white;
        isSelected = false;
        previousSelected = null;
    }

    public void SwapSprite(SpriteRenderer render2)
    { 
        if (render.sprite == render2.sprite)
        {
            
            return;
        }
        
        Sprite tempSprite = render2.sprite; 
        render2.sprite = render.sprite; 
        render.sprite = tempSprite; 
        //SFXManager.instance.PlaySFX(Clip.Swap);
    }

    private GameObject GetAdjacent(Vector2 castDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    private List<GameObject> GetAllAdjacentTiles()
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        for (int i = 0; i < adjacentDirections.Length; i++)
        {
            adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
        }
        return adjacentTiles;
    }

    private List<GameObject> FindMatch(Vector2 castDir)
    { // 1
        List<GameObject> matchingTiles = new List<GameObject>(); // 2
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir); // 3
        while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        { // 4
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
        }
        return matchingTiles; // 5
    }

    private void ClearMatch(Vector2[] paths) // 1
    {
        List<GameObject> matchingTiles = new List<GameObject>(); // 2
        for (int i = 0; i < paths.Length; i++) // 3
        {
            matchingTiles.AddRange(FindMatch(paths[i]));
        }
        if (matchingTiles.Count >= 2) // 4
        {
            for (int i = 0; i < matchingTiles.Count; i++) // 5
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            matchFound = true; // 6
        }
    }


    public void ClearAllMatches()
    {
        if (render.sprite == null)
            return;

        ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
        if (matchFound)
        {
            render.sprite = null;
            matchFound = false;
            //SFXManager.instance.PlaySFX(Clip.Clear);
        }
    }


}
