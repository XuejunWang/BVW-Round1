using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour {

    [SerializeField] public Light m_spotLight;
    [SerializeField] private AudioSource m_lightSound;
    [SerializeField] private float m_startSpotLightTime = 5f;
    //[SerializeField] private float m_stopSpotLightTime = 10f;

    [SerializeField] private float m_loopBGmStartTime = 18f;

    [SerializeField] private LightFlashing m_lightFlash;
    //[SerializeField] private float m_alarmStartTime = 15f;

    [SerializeField] private GameObject m_normalLightParents;
    [SerializeField] private GameObject m_slimeSpawner;
    private SlimeSpawner[] m_slimeSpawners;
    [SerializeField] private float m_enterFisrtWaveAfter = 12f;
    [SerializeField] private GameObject m_bird;

    [SerializeField] private Light m_endingLight;

    private IEnumerator m_loopBGMCoroutine;
    private AudioSource[] m_audioSources;

    public BirdManager birdManager;

    // Use this for initialization
    void Start () {
        m_spotLight.enabled = false;
        m_slimeSpawners = m_slimeSpawner.GetComponentsInChildren<SlimeSpawner>();

        m_audioSources = GetComponents<AudioSource>();
        EnteringBGM();

        IEnumerator spotCoroutine = StartSpotLight(m_startSpotLightTime);
        StartCoroutine(spotCoroutine);

        m_loopBGMCoroutine = LoopBGMCoroutine(m_loopBGmStartTime);
        StartCoroutine(m_loopBGMCoroutine);

        DontDestroyOnLoad(m_audioSources[4]);
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    private void StopLoopBGMCoroutine()
    {
        StopCoroutine(m_loopBGMCoroutine);
    }

    private IEnumerator StartSpotLight(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (!m_spotLight.enabled)
        {
            m_spotLight.enabled = true;
        }
        m_lightSound.Play();
        StartCoroutine(BirdTalking1_2());
    }

    private IEnumerator BirdTalking1_2()
    {
        m_bird.GetComponent<Animator>().SetBool("Talking", true);
        yield return new WaitForSeconds(12f);
        m_bird.GetComponent<Animator>().SetBool("Talking", false);
    }

    public void StopSpotLight()
    {
        m_spotLight.enabled = false;
    }

    public IEnumerator StartFlashAfter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        m_lightFlash.FlashLight.enabled = true;
        m_lightFlash.StartFlashing();
    }

    public IEnumerator StartNormaLight(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Light[] lights = m_normalLightParents.GetComponentsInChildren<Light>();
        foreach(Light light in lights)
        {
            light.enabled = true;
        }
    }

    public void BossDie()
    {
        EndingBGM();
        m_endingLight.enabled = true;
        birdManager.Escape();
    }

    IEnumerator LoopBGMCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LoopAmbiant();
    }

    IEnumerator EnterFirstWaveAfter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //SmallEnemyBGM();
        foreach (SlimeSpawner spawner in m_slimeSpawners)
        {
            spawner.EnterFirstWave();
        }
    }

    public void CagePunched()
    {
        HintBGM();
        StopLoopBGMCoroutine();
        StartCoroutine(StartFlashAfter(5.5f));
        StartCoroutine(StartNormaLight(m_enterFisrtWaveAfter));
        StartCoroutine(EnterFirstWaveAfter(m_enterFisrtWaveAfter - 3f));

        birdManager.Fly();
    }

    void ChangeBGMTo(int index)
    {
        for(int i = 0; i < m_audioSources.Length; i++)
        {
            if (i == index)
            {
                m_audioSources[i].Play();
            } else
            {
                m_audioSources[i].Stop();
            }
        }
    }
    public void EnteringBGM()
    {
        ChangeBGMTo(0);
    }

    public void HintBGM()
    {
        ChangeBGMTo(1);
    }

    public void SmallEnemyBGM()
    {
        ChangeBGMTo(2);
    }

    public void BossWaveBGM()
    {
        ChangeBGMTo(3);
    }

    public void EndingBGM()
    {
        ChangeBGMTo(4);
    }

    public void LoopAmbiant()
    {
        ChangeBGMTo(05);
    }
}
