using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    public float spawnWait = 1f;

    public int index;
    bool bCurrentPunch;

    public bool bCurrentShown;
    public bool bPunched;

    public WaveController waveController;
    IEnumerator coroutine;
    ParticleSystem hitParticles;

    private void Start()
    {
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }

    public bool GetCurrentPunch()
    {
        return bCurrentPunch;
    }

    public void SetCurrentPunch(bool value)
    {
        bCurrentPunch = value;

        if (bCurrentPunch)
        {
            coroutine = TimeLimit();
            StartCoroutine(coroutine);
        }
        else
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }

    IEnumerator TimeLimit()
    {
        yield return new WaitForSeconds(spawnWait);
        //bCurrentPunch = false;
        Debug.Log("TimeLimit " + bPunched);
        if (bPunched)
        {
            bPunched = false;
            waveController.ShowNextHighlight();
        }
        else
        {
            bCurrentPunch = false;
            waveController.ResetCurrentPunch();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BoxGrove"))
        {
            waveController.CheckHighlight(gameObject);
            //PlayParticles();
            //bPunched = true;
        }
    }

    public void PlayParticles()
    {
        hitParticles.Play();
    }
}
