using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Trigger");
        if (collision.collider.tag == "StartButton")
        {
            SceneManager.LoadScene("Main");
        }
        if (collision.collider.tag == "Player")
        {
            SceneManager.LoadScene("Main");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.tag == "StartButton")
        {
            SceneManager.LoadScene("Main");
        }
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("Main");
        }
    }
}
