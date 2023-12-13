using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector2 m_position;
    private float camSpeed = 10f;
    private float scrollSpeed = 10f;

    private bool m_toggle;

    public Transform Target;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Toggle();
        }
        m_position = Input.mousePosition;
        if (!m_toggle)
        {
            //X축 이동
            if (m_position.x >= 1900 || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = new Vector3(transform.position.x + camSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else if (m_position.x <= 20 || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = new Vector3(transform.position.x - camSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            //Y축 이동
            if (m_position.y >= 1060 || Input.GetKey(KeyCode.UpArrow))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + camSpeed * Time.deltaTime);
            }
            else if (m_position.y <= 20 || Input.GetKey(KeyCode.DownArrow))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - camSpeed * Time.deltaTime);
            }
            //플레이어 위치로 이동
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z - 10);
            }
        }
        else
        {
            if(Target == null)
            {
                m_toggle = true;
                return;
            }
            else
            {
                transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z - 10);
            }
        }
        ZoomInAndOut();
    }

    private void Toggle()
    {
        m_toggle = !m_toggle;
    }

    private void ZoomInAndOut()
    {
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 40, 100);
        float scroollWheel = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log(scroollWheel);
        Camera.main.fieldOfView += scroollWheel * scrollSpeed;
    }
}
