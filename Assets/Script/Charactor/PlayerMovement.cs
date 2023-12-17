using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //네비게이션 가져오기
    public NavMeshAgent agent;
    //애니메이션 가져오기
    Animator anim;

    //회전이동속도
    public float rotateSpeedMovement = 0.075f;
    //회전 속도
    public float rotateVelocity;
    //지연 시간
    public float motionSmoothTime = .1f;

    private HeroCombat heroCombatScript;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        heroCombatScript = GetComponent<HeroCombat>();
    }

    void Update()
    {
        //타겟이 존재한다면 아니면
        if(heroCombatScript.targetedEnemy != null)
        {
            //타겟의 컴퍼넌트가 존재한다면
            if(heroCombatScript.targetedEnemy.GetComponent<HeroCombat>() != null)
            {
                //영웅이 살아있지 않다면
                if (!heroCombatScript.targetedEnemy.GetComponent<HeroCombat>().isHeroAlive)
                {
                    //타겟은 없음
                    heroCombatScript.targetedEnemy = null;
                }
            }
        }

        float speed = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);

        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if(hit.collider.tag == "Floor")
                {
                    agent.SetDestination(hit.point);
                    heroCombatScript.targetedEnemy = null;
                    agent.stoppingDistance = 0;

                    Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        rotationToLookAt.eulerAngles.y,
                        ref rotateVelocity,
                        rotateSpeedMovement * (Time.deltaTime * 5));

                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                }
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            agent.ResetPath();
        }
    }
}
