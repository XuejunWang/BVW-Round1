using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCageScript : MonoBehaviour {

    private Animator m_animator;
    private AudioSource m_audioSource;
    private bool m_isBroken;
    [SerializeField] public EventManager m_eventManeger;
    [SerializeField] public GameObject m_bird;


	// Use this for initialization
	void Start () {
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        m_isBroken = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colision Enter");

        if(collision.collider.tag == "BoxGrove" && m_isBroken == false)
        {
            m_animator.SetTrigger("breaking");
            PlayBreakSound();
            m_isBroken = true;
            m_animator.SetBool("broken", true);
            //m_eventManeger.StopSpotLight();
            //m_bird.GetComponent<Animator>().SetBool("Flapping", true);
            //m_bird.GetComponent<BirdMovement>().BirdMove();
            m_eventManeger.CagePunched();
        }

        if (collision.collider.tag == "Player" && m_isBroken == false)
        {
            m_animator.SetTrigger("breaking");
            PlayBreakSound();
            m_isBroken = true;
            m_animator.SetBool("broken", true);
            //m_eventManeger.StopSpotLight();
            //m_bird.GetComponent<Animator>().SetBool("Flapping", true);
            //m_bird.GetComponent<BirdMovement>().BirdMove();
            m_eventManeger.CagePunched();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger Enter");

        if (other.tag == "BoxGrove" && m_isBroken == false)
        {
            m_animator.SetTrigger("breaking");
            PlayBreakSound();
            m_isBroken = true;
            m_animator.SetBool("broken", true);
            //m_eventManeger.StopSpotLight();
            //m_bird.GetComponent<Animator>().SetBool("Flapping", true);
            //m_bird.GetComponent<BirdMovement>().BirdMove();
            m_eventManeger.CagePunched();
        }

        if (other.tag == "Player" && m_isBroken == false)
        {
            m_animator.SetTrigger("breaking");
            PlayBreakSound();
            m_isBroken = true;
            m_animator.SetBool("broken", true);
            //m_eventManeger.StopSpotLight();
            //m_bird.GetComponent<Animator>().SetBool("Flapping", true);
            //m_bird.GetComponent<BirdMovement>().BirdMove();
            m_eventManeger.CagePunched();
        }
    }

    private void PlayBreakSound()
    {
        m_audioSource.Play();
    }
}
