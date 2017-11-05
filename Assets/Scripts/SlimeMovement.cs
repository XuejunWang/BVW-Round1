using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeMovement : MonoBehaviour {

    private NavMeshAgent m_navMeshAgent;
    private Rigidbody m_rigidBody;
    private Animator m_slimeAnimator;
    private static AudioSource[] m_audioSources;

    [SerializeField] public GameObject Target;
    [SerializeField] public GameObject m_particle;
    [SerializeField] private float m_slimeSpeed;
    [SerializeField] private float m_stopRange = 0.8F;
    [SerializeField] private float m_timeWaitForDyingAnimaiton = 0.5f;
    private const float m_locomationAnimationSmoothTime = .1f;
    private Animator m_animator;

    //private AudioClip smallEnemySpawn;
    private AudioClip smallEnemyDie;

    // Use this for initialization
    void Start () {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_rigidBody = GetComponent<Rigidbody>();
        m_navMeshAgent.speed = m_slimeSpeed;
        m_audioSources = GetComponents<AudioSource>();
        m_animator = GetComponent<Animator>();

        int index = Random.Range(1, 5);
        ChangeSoundToIndex(index);
        //SetScale(0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        if (m_navMeshAgent.isOnNavMesh == true)
        {
            m_navMeshAgent.destination = Target.transform.position;
            float speedPercent = m_navMeshAgent.velocity.magnitude / m_navMeshAgent.speed;

            if (m_navMeshAgent.remainingDistance > m_stopRange)
            {
                m_navMeshAgent.isStopped = false;
            }
            else
            {
                m_navMeshAgent.isStopped = true;
            }
        }
        else
        {
            Debug.Log("Game Object is not on Nav Mesh");
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collision Enter");
    //    if (collision.collider.tag == "SlimeDeath")
    //    {
    //        Debug.Log("SlimeDeath");
    //        m_animator.SetTrigger("hurt");
    //        IEnumerator coroutine = SmallEnemyDie();
    //        StartCoroutine(coroutine);
    //    }
    //    if (collision.collider.tag == "BoxGrove")
    //    {
    //        Debug.Log("BoxGrove");
    //        m_animator.SetTrigger("hurt");
    //        IEnumerator coroutine = SmallEnemyDie();
    //        StartCoroutine(coroutine);
    //    }
    //    if (collision.collider.tag == "Player")
    //    {
    //        Debug.Log("Player");
    //        m_animator.SetTrigger("hurt");
    //        IEnumerator coroutine = SmallEnemyDie();
    //        StartCoroutine(coroutine);
    //    }
    //}


    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Trigger Enter");
    //    if (other.tag == "SlimeDeath")
    //    {
    //        Debug.Log("SlimeDeath");
    //        m_animator.SetTrigger("hurt");
    //        IEnumerator coroutine = SmallEnemyDie();
    //        StartCoroutine(coroutine);
    //    }
    //    if (other.tag == "BoxGrove")
    //    {
    //        Debug.Log("BoxGrove");
    //        m_animator.SetTrigger("hurt");
    //        IEnumerator coroutine = SmallEnemyDie();
    //        StartCoroutine(coroutine);
    //    }
    //    if (other.tag == "Player")
    //    {
    //        Debug.Log("Player");
    //        m_animator.SetTrigger("hurt");
    //        IEnumerator coroutine = SmallEnemyDie();
    //        StartCoroutine(coroutine);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter");
        //if (collision.collider.tag == "SlimeDeath")
        //{
        //    Debug.Log("SlimeDeath");
        //    m_animator.SetTrigger("hurt");
        //    SmallEnemyDieAfter(m_timeWaitForDyingAnimaiton);
        //}
        if (collision.collider.tag == "BoxGrove")
        {
            Debug.Log("BoxGrove");
            //m_audioSources[2].Play();
            m_animator.SetTrigger("hurt");
            SmallEnemyDieAfter(m_timeWaitForDyingAnimaiton);
        }
        //if (collision.collider.tag == "Player")
        //{
        //    Debug.Log("Player");
        //    m_animator.SetTrigger("hurt");
        //    SmallEnemyDieAfter(m_timeWaitForDyingAnimaiton);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        //if (other.tag == "SlimeDeath")
        //{
        //    Debug.Log("SlimeDeath");
        //    m_animator.SetTrigger("hurt");
        //    SmallEnemyDieAfter(m_timeWaitForDyingAnimaiton);
        //}
        if (other.tag == "BoxGrove")
        {
            Debug.Log("BoxGrove");
            m_animator.SetTrigger("hurt");
            SmallEnemyDieAfter(m_timeWaitForDyingAnimaiton);
        }
        //if (other.tag == "Player")
        //{
        //    Debug.Log("Player");
        //    m_animator.SetTrigger("hurt");
        //    SmallEnemyDieAfter(m_timeWaitForDyingAnimaiton);
        //}
    }

    private IEnumerator SmallEnemyDieCoroutine(float dieAnimationTime)
    {
        yield return new WaitForSeconds(dieAnimationTime);
        Destroy(gameObject);
    }

    private void ChangeSoundToIndex(int index)
    {
        for(int i = 0; i<m_audioSources.Length; i++)
        {
            if(i == index)
            {
                m_audioSources[i].Play();
            }
            else
            {
                m_audioSources[i].Stop();
            }
        }
    }

    private void SmallEnemyDieAfter(float dieAnimationTime)
    {
        if (gameObject && m_audioSources[0])
        {
            ChangeSoundToIndex(0);
        }
        if (m_particle != null)
        {
            m_particle.SetActive(true);
        }
        StartCoroutine(SmallEnemyDieCoroutine(dieAnimationTime));
    }

    //private void SmallEnemyDie()
    //{
    //    if (gameObject && m_audioSources[1])
    //    {
    //        m_audioSources[0].Stop();
    //        m_audioSources[1].Play();
    //    }
    //    Destroy(gameObject);
    //}

    public void SetScale(float scale)
    {
        transform.localScale = transform.localScale * scale;
    }

}
