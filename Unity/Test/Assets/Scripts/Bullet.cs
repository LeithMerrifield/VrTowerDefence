using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
{
    public GameObject m_object = null;
    public GameObject m_target = null;
    public bool m_active = true;
    private float m_startTime;
    private float m_firingSpeed;
    public Bullet(GameObject Object,GameObject target, float Time, float Speed)
    {
        m_object = Object;
        m_target = target;
        m_startTime = Time;
        m_firingSpeed = Speed;
    }
 
    public bool OnUpdate()
    {
        if (m_object == null || m_target == null || m_active == false)
        {
            return true;
        }
        float disCovererd = (Time.time - m_startTime) * m_firingSpeed;
        try
        {
            // Moves towards the target
            m_object.transform.position = Vector3.MoveTowards(m_object.transform.position,
                                                  m_target.transform.position, m_firingSpeed * Time.deltaTime);
        }
        finally
        {
            // The target has become null so set the ball to be destroyed next update
            if (m_target == null)
            {
                m_active = false;
            }
        }
        return false;
    }

    public void OnDestroy()
    {
        m_active = false;
    }
}
