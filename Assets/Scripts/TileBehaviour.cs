using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        float currR = GetComponent<Renderer>().material.GetColor("_Color").r;
        float currG = GetComponent<Renderer>().material.GetColor("_Color").g;
        float currB = GetComponent<Renderer>().material.GetColor("_Color").b;


        if (GameObject.Find("GameManager").GetComponent<GameManager>().canExtract)
        {
            if (GetComponent<Tile>().intensity > 90)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().AddResourceBalance(30);
                GameObject.Find("GameManager").GetComponent<GameManager>().decreaseExtractNum();
                GetComponent<Tile>().intensity -= 45;
                GetComponent<Renderer>().material.SetColor("_Color", new Color(currR +=0.4f, currG, currB += 0.4f));
            }
            else if (GetComponent<Tile>().intensity < 80 && GetComponent<Tile>().intensity > 40)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().AddResourceBalance(20);
                GameObject.Find("GameManager").GetComponent<GameManager>().decreaseExtractNum();
                GetComponent<Tile>().intensity -= 30;
                GetComponent<Renderer>().material.SetColor("_Color", new Color(currR * 1.5f, currG, currB * 1.5f));
            }
            else if (GetComponent<Tile>().intensity < 40 && GetComponent<Tile>().intensity > 15)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().AddResourceBalance(10);
                GameObject.Find("GameManager").GetComponent<GameManager>().decreaseExtractNum();
                GetComponent<Tile>().intensity -= 15;
                GetComponent<Renderer>().material.SetColor("_Color", new Color(currR * 1.5f, currG, currB * 1.5f));
            }
            else if (GetComponent<Tile>().intensity < 15 && GetComponent<Tile>().intensity >= 1)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().AddResourceBalance(5);
                GameObject.Find("GameManager").GetComponent<GameManager>().decreaseExtractNum();
                GetComponent<Tile>().intensity = 0;
                GetComponent<Renderer>().material.SetColor("_Color", new Color(currR * 1.5f, currG, currB * 1.5f));
            }



            
        }


    }
}
