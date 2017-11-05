using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    [SerializeField] private float fadeInTime = 1f;

    private float m_timer = 1f;
    private float m_alpha = 0;
    private float m_scale = 0.1f;

	// Use this for initialization
	void Start () {
        m_timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;

        if (m_timer <= fadeInTime)
        {
            var material = GetComponentInChildren<Renderer>().material;
            var scale = GetComponent<Transform>().localScale;
            var color = material.color;
            m_alpha += Time.deltaTime / fadeInTime;
            m_scale += Time.deltaTime / fadeInTime;

            material.color = new Color(color.r, color.g, color.b, m_alpha);
            //transform.localScale = new Vector3(scale.x * m_scale, scale.y * m_scale, scale.z * m_scale);
        }
    }
}
