using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {


    public float startDelay = 20f;

    Animator anim;
    float startTimer;
    bool bStart;
    AudioSource bgAudio;
    [SerializeField] private GameObject startButton;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        bgAudio = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        if (!bStart)
        {
            //startButton.GetComponent<Animator>().SetTrigger("Pressed");
            startButton.GetComponent<KeyMovement>().StartButtonPressed();
            anim.SetTrigger("GameStart");
            bStart = true;
            bgAudio.Play();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
        if (bStart)
        {
            startTimer += Time.deltaTime;
            if (startTimer >= startDelay)
            {
                SceneManager.LoadScene("main");
            }
        }
    }
}
