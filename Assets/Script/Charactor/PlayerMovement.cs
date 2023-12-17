using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //�׺���̼� ��������
    public NavMeshAgent agent;
    //�ִϸ��̼� ��������
    Animator anim;

    //ȸ���̵��ӵ�
    public float rotateSpeedMovement = 0.075f;
    //ȸ�� �ӵ�
    public float rotateVelocity;
    //���� �ð�
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
        //Ÿ���� �����Ѵٸ� �ƴϸ�
        if(heroCombatScript.targetedEnemy != null)
        {
            //Ÿ���� ���۳�Ʈ�� �����Ѵٸ�
            if(heroCombatScript.targetedEnemy.GetComponent<HeroCombat>() != null)
            {
                //������ ������� �ʴٸ�
                if (!heroCombatScript.targetedEnemy.GetComponent<HeroCombat>().isHeroAlive)
                {
                    //Ÿ���� ����
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
