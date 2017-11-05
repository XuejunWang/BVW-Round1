using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public HighlightController[] arrHighlight;
    public WaveController[] arrWave;
    public Texture[] arrTexture;
    UnityEngine.AI.NavMeshAgent nav;

    bool bStart;
    bool bMove;
    Animator bossAnimator;
    AudioSource deadAudio;
    public EventManager evenManager;
    Rigidbody bossRigidbody;
    public float speed = 1f;
    public Color playerColor;
    public GameObject bossObject;
    bool bDead;
    MeshCollider meshCollider;
    Transform player;
    public Vector3 deadPosition;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < arrHighlight.Length; i++)
        {
            HighlightController highlightController = arrHighlight[i];
            GameObject highlight = highlightController.gameObject;
            highlight.SetActive(false);
        }
        bossAnimator = GetComponent<Animator>();
        AudioSource[] arrAudio = GetComponentsInParent<AudioSource>();
        deadAudio = arrAudio[2];
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        bossRigidbody = GetComponent<Rigidbody>();
        meshCollider = GetComponent<MeshCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        bMove = true;
    }

    private void Update()
    {
        if (bMove && !bDead)
        {
            if (gameObject.transform.position.z < player.position.z + 0.5)
            {
                return;
            }
            bossRigidbody.velocity = gameObject.transform.forward * speed;
        }
        if (bDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, deadPosition, Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && bMove)
        {
            Debug.Log("OnCollisionEnter");
            bMove = false;
            if (!bStart)
            {
                bStart = true;
                WaveController wave = arrWave[0];
                wave.bCurrentWave = true;
                wave.gameObject.SetActive(true);
            }
        }

        //BossDie();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !bMove)
        {
            Debug.Log("OnCollisionExit");
            bMove = true;
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") && bMove)
    //    {
    //        Debug.Log("OnTriggerEnter Player");
    //        bMove = false;
    //        if (!bStart)
    //        {
    //            bStart = true;
    //            WaveController wave = arrWave[0];
    //            wave.bCurrentWave = true;
    //            wave.gameObject.SetActive(true);
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player") && !bMove)
    //    {
    //        Debug.Log("OnTriggerExit Player");
    //        bMove = true;
    //    }
    //}

    public void NextWave()
    {
        WaveController wave = GetCurrentWave();
        if (wave)
        {
            wave.gameObject.SetActive(false);
            wave.bCurrentWave = false;
            int waveIndex = System.Array.IndexOf(arrWave, wave);
            Debug.Log("BossController.NextWave waveIndex " + waveIndex);
            waveIndex++;
            if (waveIndex < arrWave.Length)
            {
                wave = arrWave[waveIndex];
                wave.gameObject.SetActive(true);
                wave.bCurrentWave = true;
            }
            else
            {
                BossDie();
            }
        }
    }

    void BossDie()
    {
        bossAnimator.SetTrigger("dead");
        nav.enabled = false;
        deadAudio.Play();
        evenManager.BossDie();
        bDead = true;
        bossRigidbody.velocity = new Vector3(0, 0, 0);
        meshCollider.enabled = false;
        bMove = false;
    }

    WaveController GetCurrentWave()
    {
        for (int i = 0; i < arrWave.Length; i++)
        {
            WaveController wave = arrWave[i];
            if (wave.bCurrentWave)
            {
                return wave;
            }
        }
        return null;
    }

    public void SetTextureIndex(int index)
    {
        SkinnedMeshRenderer meshRenderer = bossObject.GetComponent<SkinnedMeshRenderer>();
        meshRenderer.material.mainTexture = arrTexture[index];
    }
}
