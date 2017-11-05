using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerManager : MonoBehaviour
{

    public StartManager StartManager;
    private AudioSource m_audioSource;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        if (!m_audioSource)
        {
            print("No Audio SOurce Found");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        m_audioSource.Play();
        if (collision.collider.tag == "StartButton")
        {
            StartManager.StartGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        m_audioSource.Play();
        if (other.tag == "StartButton")
        {
            StartManager.StartGame();
        }
    }
}
