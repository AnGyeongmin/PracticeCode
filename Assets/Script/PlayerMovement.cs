using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        //카메라...
        Camera camera;
        //네비게이션...
        NavMeshAgent agent;
        //애니메이터...
        Animator animator;

        //공격 범위...
        [SerializeField]
        float AttackRange = 5f;
        //이동 여부...
        bool isMove = false;
        //공격 여부...
        bool isAttack = false;

        void Awake()
        {
            //초기화...
            camera = Camera.main;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            //이동여부 확인(남은거리 > 정지거리) = true...
            isMove = agent.remainingDistance > agent.stoppingDistance;
            //애니메이션 재생...
            animator.SetBool("IsMove", isMove);
            //마우스 오른쪽버튼...
            if (Input.GetMouseButton(1))
            {
                //레이케스트 사용...
                RaycastHit hit;
                //스크린 화면에 레이를(마우스 위치에) 쏘면...
                if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    //네비게이션을 사용 위치로 이동...
                    agent.SetDestination(hit.point);
                    //레이가 맞은 오브젝트의 태그가(Enemy)
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        //어택...
                        attack();
                    }
                    //A버튼을 누르면...
                    if(Input.GetKeyDown(KeyCode.A))
                    {
                        //어택은 true
                        isAttack = true;
                        //어택 상태에서 마우스 왼쪽 클릭시...
                        if(isAttack && Input.GetMouseButton(0))
                        {
                            //어택상태 해제
                            isAttack = false;
                            //이동 위치가 어택위치보다 작을때...
                            if (agent.remainingDistance <= AttackRange)
                            {
                                attack();
                            }
                        }
                    }
                }
            }
            //S키를 누르면
            if (Input.GetKeyDown(KeyCode.S))
            {
                //이동 애니메이션 중지
                animator.SetBool("IsMove", false);
                //목표 삭제
                agent.ResetPath();
            }
        }
        void attack()
        {
            //목표까지 남은거리가 공격 범위보다 작으면
            if (agent.remainingDistance <= AttackRange && isMove)
            {
                //목표 지우고
                agent.ResetPath();
                agent.velocity = Vector3.zero;
                Debug.Log(2);
                //공격 애니메이션
                animator.SetTrigger("AttackTrigger");
            }
        }
    }
}
