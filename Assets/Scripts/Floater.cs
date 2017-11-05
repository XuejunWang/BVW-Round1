using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour {
    // paras
    [SerializeField] private float m_degreesPerSecond = 15.0f;
    [SerializeField] private float m_amplitude = 0.5f;
    [SerializeField] private float m_frequency = 1f;
    private float m_bias;

    Vector3 tempPos = new Vector3();

    private void Awake()
    {
        m_bias = Random.Range(0f, 1f) * Mathf.PI;
    }

    // Update is called once per frame
    void Update () {
        tempPos = transform.position;
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * m_degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * m_frequency + m_bias) *  m_amplitude;
        transform.position = tempPos;
    }
    
    public void SetFloatParas(float amplitute, float frequency, float degreePerSecond)
    {
        m_amplitude = Random.Range(0, amplitute);
        m_degreesPerSecond = degreePerSecond;
        m_frequency = frequency;
    }
}
