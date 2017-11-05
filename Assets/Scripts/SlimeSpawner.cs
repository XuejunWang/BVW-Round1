using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlimeSpawner : MonoBehaviour {

    [SerializeField] private GameObject m_slime;
    [SerializeField] private GameObject m_smallSlime;

    private IEnumerator m_coroutine;

    //paras for the waves
    [SerializeField] private EventManager m_eventManager;
    [SerializeField] private float m_spawnDelay;
    [SerializeField] private float m_firstWaveStartTime = 0;
    [SerializeField] private float m_firstWaveSpawnTime;
    [SerializeField] private int m_numOfFirstWave;
    [SerializeField] private float m_secondWaveSpawnTime;
    [SerializeField] private int m_numOfSecondWave ;
    [SerializeField] private float m_StartBossFlashAfter ;

    //interval between waves
    [SerializeField] private float m_intervalTime;
    private AudioSource m_audioSource;
    //waves states
    public bool AllowSpawning = true;
    public bool FirstWaveTriggered = false;
    public bool SecondWaveTriggered = false;
    public bool WaveEnded = false;
    public bool BossWaveTriggered = false;

    //counting
    public int NumOfSlimeGenerated = 0;
    public int CurrentNumOfSlime = 0;
    public bool AllGenerated = false;

    public OpenDoorManager m_openDoorManager;

    [SerializeField] private int m_bossIntervalTime;
    [SerializeField] private GameObject boss;

    // Use this for initialization
    void Start () {

        //StartCoroutine(EnterSecondWave(m_numOfSecondWave, m_intervalTime, m_secondWaveSpawnTime, m_smallSlime));
    }
	
	// Update is called once per frame 
	void Update () {
        // get current num of slimes
        GameObject[] currentSlimes = GameObject.FindGameObjectsWithTag("Slime");
        CurrentNumOfSlime = currentSlimes.Length;

        if (AllGenerated && !WaveEnded && CurrentNumOfSlime == 0)
        {
            WaveEnded = true;
        }

        //if has entered first wave and there is no slimes now, Enter the second wave
        if (!SecondWaveTriggered&& FirstWaveTriggered && WaveEnded)
        {
            StopCoroutine(m_coroutine);
            WaveEnded = false;
            SecondWaveTriggered = true;
            m_coroutine = EnterSecondWave(m_secondWaveSpawnTime, m_numOfSecondWave, m_intervalTime + m_spawnDelay,  m_smallSlime);
            StartCoroutine(m_coroutine);
        }

        if (FirstWaveTriggered && SecondWaveTriggered && WaveEnded && !BossWaveTriggered)
        {
            StopCoroutine(m_coroutine);
            if (boss)
            {
                BossWaveTriggered = true;
                m_coroutine = EnterBossWave(m_bossIntervalTime);
                StartCoroutine(m_coroutine);
            }
        }
    }

    void DoSthAfter(Action _callback)
    {
        _callback();
    }


    void SpawnSlime(int numOfSlime, GameObject slime)
    {
        //spawn
        if (NumOfSlimeGenerated < numOfSlime)
        {
            GameObject currentSlime = Instantiate(slime, transform.position, transform.rotation);
            NumOfSlimeGenerated++;
            currentSlime.GetComponent<SlimeMovement>().Target = GameObject.FindGameObjectWithTag("Player");
        }
        //if has spawned enough slimes, stop coroutine
        if(NumOfSlimeGenerated == numOfSlime)
        {
            AllGenerated = true;
            StopCoroutine(m_coroutine);
        }
    }

    private IEnumerator SlimeGenerateCoroutine(float spawnTime, int numOfSlime, GameObject slime)
    {  
        while (!AllGenerated)
        {
            //wait for interval time
            yield return new WaitForSeconds(spawnTime);
            SpawnSlime(numOfSlime, slime);
        }
    }

    public void StartSpawning(float spawnTime, int numOfSlime, GameObject slime)
    {
        //Debug.Log("end waiting");
        NumOfSlimeGenerated = 0;
        m_coroutine = SlimeGenerateCoroutine(spawnTime, numOfSlime, slime);
        StartCoroutine(m_coroutine);
    }

    public void StopSpawning()
    {
        WaveEnded = false;
        AllGenerated = false;
        //NumOfSlimeGenerated = 0;
        StopCoroutine(m_coroutine);
    }

    //private IEnumerator EnterWave(float spawnTime float intervalTime, int numOfSlime,)
    //{
    //    yield return new WaitForSeconds(intervalTime);
    //    StartSpawning(spawnTime, numOfSlime);
    //}

    public IEnumerator EnterWave(float spawnTime, int numOfSlime, float intervalTime, GameObject slime)
    {
        NumOfSlimeGenerated = 0;
        AllGenerated = false;
        yield return new WaitForSeconds(intervalTime);
        m_eventManager.SmallEnemyBGM();
        StartSpawning(spawnTime, numOfSlime, slime );
    }

    private IEnumerator EnterSecondWave(float spawnTime, int numOfSlime, float intervalTime, GameObject slime)
    {
        NumOfSlimeGenerated = 0;
        AllGenerated = false;
        yield return new WaitForSeconds(intervalTime);
        StartSpawning(spawnTime, numOfSlime, slime);

        DoSthAfter(StopSpawning);
    }

    private IEnumerator EnterBossWave(float intervalTime)
    {
        NumOfSlimeGenerated = 0;
        AllGenerated = false;
        yield return new WaitForSeconds(intervalTime);
        m_eventManager.BossWaveBGM();
        StartCoroutine(m_eventManager.StartFlashAfter(m_StartBossFlashAfter));
        //boss.SetActive(true);
        m_openDoorManager.OpenDoor();
    }

    IEnumerator EnterWaveAfter(float waitTime)
    {
        FirstWaveTriggered = true;
        yield return new WaitForSeconds(waitTime);
        //Enter Wave 1
        m_coroutine = EnterWave(m_firstWaveSpawnTime, m_numOfFirstWave, m_firstWaveStartTime + m_spawnDelay, m_slime);
        StartCoroutine(m_coroutine);
    }

    public void EnterFirstWave()
    {
        StartCoroutine(EnterWaveAfter(0.5f));
    }
}
