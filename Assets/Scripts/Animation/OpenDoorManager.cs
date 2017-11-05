using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorManager : MonoBehaviour {

    Animator anim;
    AudioSource aud;
    public GameObject boss;
    public MeshCollider meshCollider;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        //OpenDoor();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenDoor()
    {
        Debug.Log("OpenDoor");
        anim.SetTrigger("OpenDoor");
        aud.Play();
        StartCoroutine(PlayBossEnter());
    }

    IEnumerator PlayBossEnter()
    {
        yield return new WaitForSeconds(1);
        boss.SetActive(true);
        meshCollider.enabled = false;
        yield return new WaitForSeconds(1);
        aud.Play();
        yield return new WaitForSeconds(1);
        meshCollider.enabled = true;
    }

    public void BirdFlyOut()
    {
        Debug.Log("BirdFlyOut");
        anim.SetTrigger("BirdFlyOut");
        aud.Play();
    }
}
