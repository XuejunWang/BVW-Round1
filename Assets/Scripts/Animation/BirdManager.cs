using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdManager : MonoBehaviour {

    Animator anim;
    public Animator birdAnim;
    public OpenDoorManager openDoorManager;
    public GameObject whiteBox;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    public void Fly()
    {
        anim.SetTrigger("BirdFly");
    }

    public void TakeOff()
    {
        Debug.Log("BirdManager.TakeOff");
        birdAnim.SetTrigger("TakeOff");
    }

    public void Escape()
    {
        Debug.Log("Escape");
        anim.SetTrigger("BirdEscape");
    }

    public void Land()
    {
        birdAnim.SetTrigger("Land");
    }

    public void Jump()
    {
        birdAnim.SetTrigger("Jumping");
    }

    public void OpenDoor()
    {
        whiteBox.SetActive(true);
        openDoorManager.BirdFlyOut();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("end");
    }
}
