using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{

    public float m_health = 150;
    public float m_radius = 5f;
    public bool m_dead = false;
    public GameObject m_deathText;
    private Vector3 m_originalScale;
    private AudioSource m_audioData;


    private void Start()
    {
        m_originalScale = transform.localScale;
    }

    private void Update()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_radius);
        foreach (var i in hitColliders)
        {
            if (m_health <= 0 || m_dead == true)
            {
                m_dead = true;
                m_deathText.SetActive(true);
                continue;
            }
            if (i.tag == "Player")
            {
                m_health -= 10;
                m_audioData = GetComponent<AudioSource>();
                if (!m_audioData.isPlaying)
                {
                    m_audioData.Play(0);
                }
                if (transform.localScale != m_originalScale)
                {
                    transform.localScale /= 2f;
                }
                else
                {
                    transform.localScale *= 2f;
                }
                Destroy(i.gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }
}
