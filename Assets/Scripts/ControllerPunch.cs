using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPunch : MonoBehaviour {

    private SteamVR_TrackedObject m_trackedObj;
    private GameObject m_collidingObject;
    private AudioSource m_audioSource;


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)m_trackedObj.index); }
    }

    void Awake()
    {
        m_trackedObj = GetComponent<SteamVR_TrackedObject>();
        m_audioSource = GetComponent<AudioSource>();
    }

    private void SetCollidingObject(Collider col)
    {
        if (m_collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        m_collidingObject = col.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        //if (Controller.GetAxis() != Vector2.zero)
        //{
        //    //Debug.Log(gameObject.name + Controller.GetAxis());
        //}

        //if (Controller.GetHairTriggerDown())
        //{
        //    Debug.Log(gameObject.name + " Trigger Press");
        //}

        //if (Controller.GetHairTriggerUp())
        //{
        //    Debug.Log(gameObject.name + " Trigger Release");
        //}

        //if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        //{
        //    Debug.Log(gameObject.name + " Grip Press");
        //}

        //if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        //{
        //    Debug.Log(gameObject.name + " Grip Release");
        //}
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
        if (m_collidingObject && m_collidingObject.CompareTag("Slime"))
        {
            print("Slime");
            m_audioSource.Play();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!m_collidingObject)
        {
            return;
        }

        m_collidingObject = null;
    }
}
