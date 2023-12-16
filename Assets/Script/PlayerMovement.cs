using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        //ī�޶�...
        Camera camera;
        //�׺���̼�...
        NavMeshAgent agent;
        //�ִϸ�����...
        Animator animator;

        //���� ����...
        [SerializeField]
        float AttackRange = 5f;
        //�̵� ����...
        bool isMove = false;
        //���� ����...
        bool isAttack = false;

        void Awake()
        {
            //�ʱ�ȭ...
            camera = Camera.main;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            //�̵����� Ȯ��(�����Ÿ� > �����Ÿ�) = true...
            isMove = agent.remainingDistance > agent.stoppingDistance;
            //�ִϸ��̼� ���...
            animator.SetBool("IsMove", isMove);
            //���콺 �����ʹ�ư...
            if (Input.GetMouseButton(1))
            {
                //�����ɽ�Ʈ ���...
                RaycastHit hit;
                //��ũ�� ȭ�鿡 ���̸�(���콺 ��ġ��) ���...
                if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    //�׺���̼��� ��� ��ġ�� �̵�...
                    agent.SetDestination(hit.point);
                    //���̰� ���� ������Ʈ�� �±װ�(Enemy)
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        //����...
                        attack();
                    }
                    //A��ư�� ������...
                    if(Input.GetKeyDown(KeyCode.A))
                    {
                        //������ true
                        isAttack = true;
                        //���� ���¿��� ���콺 ���� Ŭ����...
                        if(isAttack && Input.GetMouseButton(0))
                        {
                            //���û��� ����
                            isAttack = false;
                            //�̵� ��ġ�� ������ġ���� ������...
                            if (agent.remainingDistance <= AttackRange)
                            {
                                attack();
                            }
                        }
                    }
                }
            }
            //SŰ�� ������
            if (Input.GetKeyDown(KeyCode.S))
            {
                //�̵� �ִϸ��̼� ����
                animator.SetBool("IsMove", false);
                //��ǥ ����
                agent.ResetPath();
            }
        }
        void attack()
        {
            //��ǥ���� �����Ÿ��� ���� �������� ������
            if (agent.remainingDistance <= AttackRange && isMove)
            {
                //��ǥ �����
                agent.ResetPath();
                agent.velocity = Vector3.zero;
                Debug.Log(2);
                //���� �ִϸ��̼�
                animator.SetTrigger("AttackTrigger");
            }
        }
    }
}
