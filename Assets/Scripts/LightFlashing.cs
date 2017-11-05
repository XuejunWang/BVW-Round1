using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlashing : MonoBehaviour {
    public float m_timer = 0.5f ;
    public float m_stopAfter = 5f;
    private AudioSource m_alarmClip;

    public Light FlashLight;
    private IEnumerator m_coroutine;
    private float m_startTime;

    private void Awake()
    {
        FlashLight = GetComponent<Light>();
        m_alarmClip = GetComponent<AudioSource>();
        //m_coroutine = Flashing(m_timer);
    }

    private void Update()
    {
        if (m_startTime !=0 )
        {
            if (Time.time > m_stopAfter + m_startTime)
            {
                StopFlashing();
            }
        }
    }

    public void StartFlashing()
    {
        m_startTime = Time.time;
        m_coroutine = Flashing(m_timer);
        StartCoroutine(m_coroutine);
        m_alarmClip.Play();
    }

    public void StopFlashing()
    {
        //m_coroutine = Flashing(m_timer);
        StopCoroutine(m_coroutine);
        FlashLight.enabled = false;
    }

    private IEnumerator Flashing(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            FlashLight.enabled = !FlashLight.enabled;
        }
    }

    //private IEnumerator yieldPlaySound(float waitTime, AudioSource audio)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    audio.Play();
    //}


}
