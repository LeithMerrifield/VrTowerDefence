using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy
{
    public GameObject m_object = null;
    public bool m_active = true;
    public int m_speed = 5;
    private Transform m_destination;
    private NavMeshAgent m_navMeshAgent = null;

    public Enemy(GameObject Object, Transform target, int Speed)
    {
        m_object = Object;
        m_navMeshAgent = m_object.GetComponent<NavMeshAgent>();
        m_destination = target;
        m_navMeshAgent.speed = Speed;
    }

    public bool OnUpdate()
    {
        if (m_object == null)
        {
            m_active = false;
            return true;
        }

        return false;
    }

    public void OnDestroy()
    {
        m_active = false;
    }

    public void SetDestination(Transform destination)
    {
        if (destination != null && m_navMeshAgent != null)
        {
            m_destination = destination;
            m_navMeshAgent.destination = m_destination.position;
        }
    }
}
