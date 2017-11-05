using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{

    [SerializeField] private Transform m_destination;
    [SerializeField] private float m_duration;
    [SerializeField] private float m_moveDelay;

    private Transform origin;

    private float startTime;
    private float journeyLength;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        origin = transform;
        journeyLength = Vector3.Distance(origin.position, m_destination.position);
    }

    IEnumerator MoveObject(Vector3 origin, Vector3 target, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            transform.position = Vector3.Lerp(origin, target, (Time.time - startTime) / duration);
            yield return null;
        }
        transform.position = target;
    }

    IEnumerator MoveObjectAfter(float waitTime, Vector3 origin, Vector3 target, float duration)
    {
        yield return new WaitForSeconds(waitTime);
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            transform.position = Vector3.Lerp(origin, target, (Time.time - startTime) / duration);
            yield return null;
        }
        transform.position = target;
    }

    public void FlyTo()
    {

    }

    public void FlyBack()
    {

    }

    public void BirdMove()
    {
        //IEnumerator moveBird = MoveObject(origin.position, m_destination.position, m_duration);
        //StartCoroutine(moveBird);
        IEnumerator moveBird = MoveObjectAfter(m_moveDelay, origin.position, m_destination.position, m_duration);
        StartCoroutine(moveBird);
    }
}
