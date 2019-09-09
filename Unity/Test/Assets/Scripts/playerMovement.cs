using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float m_speed = 0.0f;
    private float m_speedMultiplier = 20.0f;
    Rigidbody rb = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey("d"))
        {
            var amount = m_speed * Time.deltaTime * m_speedMultiplier;
            rb.AddForce(amount, 0.0f, 0.0f, ForceMode.Impulse);
        }

        if (Input.GetKey("a"))
        {
            var amount = m_speed * Time.deltaTime * m_speedMultiplier;
            rb.AddForce(-amount, 0.0f, 0.0f, ForceMode.Impulse);
        }
        if (Input.GetKey("w"))
        {
            var amount = m_speed * Time.deltaTime * m_speedMultiplier;
            rb.AddForce(0.0f, 0.0f, amount, ForceMode.Impulse);
        }

        if (Input.GetKey("s"))
        {
            var amount = m_speed * Time.deltaTime * m_speedMultiplier;
            rb.AddForce(0.0f, 0.0f, -amount, ForceMode.Impulse);
        }
    }
}
