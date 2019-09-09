using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public Transform m_startPoint;
    public Transform m_endPoint;
    public GameObject m_enemy = null;
    public GameObject m_canvas = null;
    public float m_spawnRate = 1f;
    public List<Enemy> m_enemyPool = new List<Enemy>();
    public int m_speed = 5;
    private IEnumerator Spawning;

    private void Awake()
    {
        if(m_canvas != null)
        {
            m_canvas.SetActive(true);
        }
    }

    void Start()
    {
        Spawning = SpawnEnemy();
        StartCoroutine(Spawning);
    }

    private void Update()
    {
        var tempList = new List<Enemy>(m_enemyPool);
        foreach (var i in tempList)
        {
            if(i.OnUpdate())
            {
                m_enemyPool.Remove(i);
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            if(m_enemy != null)
            {
                var newObject = Instantiate(m_enemy, m_startPoint);
                m_enemyPool.Add(new Enemy(newObject, m_endPoint, m_speed));
                m_enemyPool[m_enemyPool.Count - 1].SetDestination(m_endPoint);
            }
            yield return new WaitForSeconds(m_spawnRate);
        }
    }
}
