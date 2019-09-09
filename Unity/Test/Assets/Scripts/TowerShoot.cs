using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class TowerShoot : MonoBehaviour
{
    public GameObject m_prefabBullet = null;
    public Transform m_spawn = null;
    public float m_firingSpeed = 1.0f;
    public float m_Radius = 5.0f;

    private GameObject m_target = null;
    private float m_startTime;
    private float m_journeyLength;
    private List<Bullet> m_bulletPool = new List<Bullet>();
    private Timer m_timer;
    private IEnumerator m_shoot;

    void Start()
    {
        try
        {
            m_spawn = gameObject.transform.Find("Spawn");
        }
        catch
        {
            m_spawn = gameObject.transform;
        }

        m_shoot = Shoot();
        StartCoroutine(m_shoot);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (m_target != null)
            {
                GameObject newObject = Instantiate(m_prefabBullet, new Vector3(m_spawn.position.x,
                                                   m_spawn.position.y,
                                                   m_spawn.position.z),
                                                   Quaternion.identity);

                m_bulletPool.Add(new Bullet(newObject, m_target, Time.time, m_firingSpeed));
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_Radius);
        m_target = null;
        foreach (var i in hitColliders)
        {
            if (i.tag == "Player")
            {
                m_target = i.gameObject;
            }
        }

        var tempList = new List<Bullet>(m_bulletPool);

        if (m_bulletPool.Count > 0)
        {
            foreach (var i in tempList)
            {
                if (i.OnUpdate())
                {
                    Destroy(i.m_object);
                    m_bulletPool.Remove(i);
                }
            }
        }
    }

    public void Enable()
    {
        enabled = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }
    
    public void Resize()
    {
        var temp = GetComponentsInChildren<Transform>();
        foreach (var i in temp)
        {
            if (i.name == "Radius")
            {
                i.localScale = new Vector3(m_Radius, m_Radius, m_Radius);
            }
        }
    }
}
