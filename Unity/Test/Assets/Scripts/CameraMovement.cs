using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float m_speed;

    // Update is called once per frame
    void Update()
    {
        var speed = m_speed * Time.deltaTime;

        transform.eulerAngles += CameraRotate(speed);
        transform.position += CameraMove(speed);
    }

    Vector3 CameraRotate(float speed)
    {
        float yaw = 0.0f;
        float pitch = 0.0f;

        if(Input.GetKey(KeyCode.LeftControl))
        {
            yaw += 2.0f * Input.GetAxis("Mouse X");
            pitch -= 2.0f * Input.GetAxis("Mouse Y");
        }

        return new Vector3(pitch, yaw, 0.0f);
    }

    Vector3 CameraMove(float speed)
    {
        Vector3 movement = new Vector3();
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -speed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            movement.z = speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement.z = -speed;
        }
        var newMovement = Camera.main.transform.TransformDirection(movement);
        newMovement.y = 0.0f;
        return newMovement;
    }
}
