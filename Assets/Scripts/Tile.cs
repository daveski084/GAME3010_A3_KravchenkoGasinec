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
    public static int difficulty;

    private static Color selectedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    private SpriteRenderer render;
    private static Tile previousSelected;
    private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private bool matchFound = false;
    [SerializeField] private AudioClip matchSound, clickSound;
    private AudioSource audioSource;


    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }


    void OnMouseDown()
    {
        if(GameManager.instance.movesLeft < 1 || GameManager.instance.timeLeft < 0.1f)
        {
            return;
        }
        
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
                if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
                {
                    GameManager.instance.movesLeft--;
                    GameManager.moves--;
                    SwapSprite(previousSelected.render);
                    previousSelected.ClearAllMatches();
                    previousSelected.DeSelect();
                    ClearAllMatches();
                }
                else
                {
                    previousSelected.GetComponent<Tile>().DeSelect();
                    Select();
                }
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
    }

    private GameObject GetAdjacent(Vector2 castDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name.ToString());
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
    { 
        List<GameObject> matchingTiles = new List<GameObject>(); 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir); 
        while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        { 
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
        }
        return matchingTiles; 
    }

    private void ClearMatch(Vector2[] paths) 
    {
        List<GameObject> matchingTiles = new List<GameObject>(); 
        for (int i = 0; i < paths.Length; i++)
        {
            matchingTiles.AddRange(FindMatch(paths[i]));
        }
        if (matchingTiles.Count >= difficulty) // 
        {
            GameManager.instance.resourcesGathered += (matchingTiles.Count + 1);
            ResourceCounterTextBehaviour.scoreNum += (matchingTiles.Count + 1);

            for (int i = 0; i < matchingTiles.Count; i++) 
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            matchFound = true; 
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
            PlayAudioClip(matchSound);
            matchFound = false;
            StopCoroutine(GridController.instance.FindNullTiles());
            StartCoroutine(GridController.instance.FindNullTiles());
        }
    }

    void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
