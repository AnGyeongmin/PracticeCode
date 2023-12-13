using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Camera camera;

        private bool isMove;
        private bool isEnemy;
        private Vector3 destination;

        private float moveSpeed = 5f;

        private float AttackRange = 5f;

        private void Awake()
        {
            camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;
                if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    SetDestination(hit.point); 
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        isEnemy = true;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                isMove = false;
            }

            Move();
        }

        private void SetDestination(Vector3 dest)
        {
            destination = dest;
            isMove = true;
            isEnemy = false;
        }

        private void Move()
        {
            if (isMove)
            {
                var dir = destination - transform.position;
                transform.position += dir.normalized * Time.deltaTime * moveSpeed;
            }
            if (Vector3.Distance(transform.position, destination) <= 0.1f)
            {
                isMove = false;
            }
            if (isEnemy)
            {
                if (Vector3.Distance(transform.position, destination) <= AttackRange)
                {
                    isMove = false;
                }
            }
        }
    }
}
