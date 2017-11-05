using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    public HighlightController[] arrHighlight;
    public float startWait = 1f;
    public float spawnWait = 1f;
    public float hurtForce = 1000f;

    bool bSpawn;
    public bool bPunch;
    Rigidbody enemyRigidbody;
    Transform player;
    float camRayLength = 100f;
    int highlightMask;
    public bool bCurrentWave;

    public int[] arrAppearIndex = { 2, 1, 0 };
    protected BossController bossController;
    protected Animator bossAnimator;
    protected AudioSource hurtAudio;
    protected AudioSource attackAudio;

    // Use this for initialization
    void Start()
    {
        bossController = GetComponentInParent<BossController>();
        arrHighlight = bossController.arrHighlight;
        for (int i = 0; i < arrHighlight.Length; i++)
        {
            HighlightController highlightController = arrHighlight[i];
            highlightController.waveController = this;
            highlightController.bPunched = false;
            //highlightController.appearIndex = arrAppearIndex[i];
            highlightController.index = i;
            highlightController.spawnWait = this.spawnWait;
            GameObject highlight = highlightController.gameObject;
            MeshRenderer meshRenderer = highlight.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
            highlight.SetActive(true);
        }
        bossAnimator = GetComponentInParent<Animator>();
        enemyRigidbody = GetComponentInParent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        highlightMask = LayerMask.GetMask("HighlightMask");
        AudioSource[] arrAudio = GetComponentsInParent<AudioSource>();
        hurtAudio = arrAudio[0];
        attackAudio = arrAudio[1];

        StartWave();
    }

    public virtual void ShowNextHighlight()
    {

    }

    protected virtual void StartWave()
    {
        Debug.Log("WaveController.StartWave");
        bSpawn = true;
        StartCoroutine(SpawnHighlight());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !bSpawn)
        {
            bSpawn = true;
            bPunch = true;
            StartCoroutine(SpawnHighlight());
        }
    }

    IEnumerator SpawnHighlight()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            HighlightController currentHighlight = GetCurrentShownHighlight();

            int nextShownIndex = 0;
            if (currentHighlight)
            {
                currentHighlight.bCurrentShown = false;
                bossController.SetTextureIndex(0);
                nextShownIndex = currentHighlight.index + 1;
            }
            if (nextShownIndex < arrHighlight.Length)
            {
                Debug.Log("nextShownIndex " + nextShownIndex);
                HighlightController nextHighlight = GetHighlight(nextShownIndex);
                bossController.SetTextureIndex(nextShownIndex + 1);
                nextHighlight.bCurrentShown = true;
                yield return new WaitForSeconds(spawnWait);
            }
            else
            {
                yield return new WaitForSeconds(spawnWait);
                //for (int i = 0; i < arrHighlight.Length; i++)
                //{
                //    GameObject highlight = arrHighlight[i].gameObject;
                //    MeshRenderer meshRenderer = highlight.GetComponent<MeshRenderer>();
                //    meshRenderer.enabled = true;
                //}
                HighlightController highlightController = GetHighlight(0);
                highlightController.SetCurrentPunch(true);
                bPunch = true;
                break;
            }
        }
    }

    void Update()
    {
        if (bPunch && Input.GetMouseButtonDown(0))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(camRay, out hit, camRayLength, highlightMask))
            {
                Debug.Log("Physics.Raycast(camRay, out hit, camRayLength, highlightMask)");
                CheckHighlight(hit.collider.gameObject);
                //HighlightController highlightController = hit.collider.gameObject.GetComponent<HighlightController>();
                //if (highlightController)
                //{
                //    highlightController.PlayParticles();
                //}
            }
        }
    }

    public virtual void CheckHighlight(GameObject highlight)
    {
        HighlightController currentHighlight = GetCurrentPunchHighlight();
        if (!currentHighlight)
        {
            currentHighlight = GetHighlight(arrAppearIndex[0]);
        }
        if (currentHighlight && currentHighlight.gameObject == highlight)
        {
            Debug.Log("bossAnimator.SetTrigger(hurt);");
            currentHighlight.SetCurrentPunch(false);
            MeshRenderer meshRenderer = currentHighlight.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
            bossAnimator.SetTrigger("hurt");
            hurtAudio.Play();
            int nextPunchIndex = currentHighlight.index + 1;
            if (nextPunchIndex < arrHighlight.Length)
            {
                HighlightController nextHighlight = GetHighlight(nextPunchIndex);
                nextHighlight.SetCurrentPunch(true);
            }
            else
            {
                //enemyRigidbody.AddForce(player.forward * hurtForce);
                bossController.NextWave();
            }
        }
    }

    public virtual void ResetCurrentPunch()
    {
        HighlightController highlightController = GetHighlight(0);
        highlightController.SetCurrentPunch(true);
        for (int i = 0; i < arrHighlight.Length; i++)
        {
            GameObject highlight = arrHighlight[i].gameObject;
            MeshRenderer meshRenderer = highlight.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
        }
        bossAnimator.SetTrigger("attack");
        attackAudio.Play();
    }

    protected HighlightController GetCurrentPunchHighlight()
    {
        for (int i = 0; i < arrHighlight.Length; i++)
        {
            HighlightController highlightController = arrHighlight[i];
            if (highlightController.GetCurrentPunch())
            {
                return highlightController;
            }
        }
        return null;
    }

    protected HighlightController GetCurrentShownHighlight()
    {
        for (int i = 0; i < arrHighlight.Length; i++)
        {
            HighlightController highlightController = arrHighlight[i];
            if (highlightController.bCurrentShown)
            {
                return highlightController;
            }
        }
        return null;
    }

    protected HighlightController GetHighlight(int appearIndex)
    {
        //for (int i = 0; i < arrHighlight.Length; i++)
        //{
        //    HighlightController highlightController = arrHighlight[i];
        //    if (highlightController.appearIndex == appearIndex)
        //    {
        //        return highlightController;
        //    }
        //}
        //return null;
        return arrHighlight[arrAppearIndex[appearIndex]];
    }
}
