using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovement : MonoBehaviour {

    [SerializeField] private Transform m_destination;
    [SerializeField] private float m_duration;

    private Transform m_origin;
    private float m_startTime;
    private float m_journeyLength;

    void Start () {
        m_startTime = Time.time;
        m_origin = transform;
        m_journeyLength = Vector3.Distance(m_origin.position, m_destination.position);
    }

    IEnumerator MoveObject(Vector3 source, Vector3 target, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / duration);
            yield return null;
        }
        transform.position = target;
    }

    //public void KeyMove()
    //{
    //    IEnumerator moveKey = MoveObject(origin.position, m_destination.position, 1f);
    //    StartCoroutine(moveKey);
    //}

    public void StartButtonPressed()
    {
        IEnumerator moveStartButton = MoveObject(m_origin.position, m_destination.position, m_duration);
        StartCoroutine(moveStartButton);
    }
}
