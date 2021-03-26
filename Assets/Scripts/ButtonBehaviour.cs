using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public Canvas titleCanvas;
    
    [SerializeField] private AudioClip startSound;
    [SerializeField] private AudioSource audioSource1;



    private void Start()
    {
        audioSource1.GetComponent<AudioSource>();
        titleCanvas.gameObject.SetActive(true);
    }

    private IEnumerator WaitForStart()
    {
        PlaySound(startSound);
        yield return new WaitForSeconds(3);
        GameManager.startButtonPressed = true;
        titleCanvas.gameObject.SetActive(false);
    }

    public void OnPressedStartButton()
    {
        StartCoroutine(WaitForStart());
    }

    void PlaySound(AudioClip clip)
    {
        audioSource1.clip = clip;
        audioSource1.Play();
    }
}
