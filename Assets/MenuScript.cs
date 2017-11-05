using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

    private Animator m_animator;
	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
        StartCoroutine(PlaySoundAfter());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator PlaySoundAfter()
    {
        yield return new WaitForSeconds(1.65f);
        m_animator.enabled = true;
    }
}
