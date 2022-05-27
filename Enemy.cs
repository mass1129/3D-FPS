using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{   NavMeshAgent agent;
    GameObject target;
    
    public float findDistance = 5;

    public float speed = 1;
    public float attackDistance =1.5f;
    
    public float traceDistace =6;
    float currentTime;
    float attackTime=1f;
    PlayerHP php;
    //Return에서 Idle로 전이할때 필요한 초기 위치 변수를 선언한다.(초기자리로 돌아가면 Idle로 전이할 계획)
    public Vector3 startPosition;
    // Start is called before the first frame update
    public enum State
    {
        Idle,
        Move,
        Return,
        Attack,
        Die,
       
    }

    public State state;
    
    
    IEnumerator Start()
    {   
        agent = GetComponent<NavMeshAgent>();
        state = State.Idle;
       
        target = GameObject.Find("Player");
        php = target.GetComponent<PlayerHP>();
        //Enemy가 생성되고나서 이후 생성되는 다른 Enemy객체와 충돌하고 
        //NavMeshAgent 특성으로 위치가 조금 변하기때문에 1초후에 초기위치를 기억한다.
        yield return new WaitForSeconds(1);
        startPosition = transform.position;
    }

    
    // Update is called once per frame
    void Update()
    {   
        
        if(state == State.Idle)
        {
            UpdateIdle();
        }
        else if(state == State.Move)
        {
            UpdateMove();
        }
        else if(state == State.Attack)
        {
            UpdateAttack();
        }
        else if (state == State.Return)
        {
            UpdateReturn();
        }
        
    }

    
    
    
    
    private void UpdateIdle()
    {   
        
        
        float distance = Vector3.Distance(transform.position, target.transform.position);
        //Enemy가 대기(Idle) 상태일 때 플레이어를 감지하는 시야각을 전방의 좌우 각각 45도로 제한하는 기능을 구현한다.
        //1. Idle상태일때 
        //2. 플레이어와 Enemy의 벡터값을 targetDir이라고 정의하고
        Vector3 targetDir = target.transform.position - transform.position;
        //3. targetDir과 Enemy의 전방시야벡터값이 이루는 각을 구해서
        float angle = Vector3.Angle(targetDir,transform.forward);
        //4. 그 각도가 -45이상 45이하이고 대상이 감지거리 안에 있을때
        if(-45<angle &&angle<45 && distance < findDistance)
        {
    
            //5. Move상태로 전이한다.
            state = State.Move;
            
            agent.destination = target.transform.position;
        }
        }
        
    
    
   
    private void UpdateMove()
    {
        
        agent.destination = target.transform.position;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if(distance<attackDistance)
        {
            
            state = State.Attack;
            agent.isStopped = true;

        }
        
       else if (distance>traceDistace)
        {   
            
            state = State.Return;
    }
    }
    



    
    private void UpdateAttack()
    {
        
        currentTime += Time.deltaTime;
        
        if(currentTime>attackTime)
        {
            
            currentTime=0;
            
            float distance = Vector3.Distance(transform.position, target.transform.position);
            
            
            if(distance>attackDistance&& php.HP>0)
            {   
                
                state = State.Move;
                agent.isStopped = false;
            }
            
            else if(distance<=attackDistance && php.HP>0)
            {
                
                php.AddDamage();
                
                HitManager.instance.Hit();
            }
            else if(php.HP==0)
                {   
                    
                    HitManager.instance.GameOver();
                    
                    state = State.Return;

                    agent.isStopped = false;
                }
            }
        }
    
    
    private void UpdateReturn()
    {   
        //시작지점으로 이동한다.
        agent.destination = startPosition;
        
        float distance = Vector3.Distance(transform.position, target.transform.position);

        //대상이 죽었는데도 계속 따라가는것을 막기위해 
        //인지범위 안에 대상이 있고 플레이어가 살아있을때 다시 추격한다.
        if (distance < findDistance && php.HP>0)
        {
            
            state = State.Move;
        }
        //시작지점에 도착하면 Idle상태로 전이한다.
        else if(transform.position == startPosition)
        {
            state = State.Idle;
        }
       
       
    }

    public void AddDamage(int damage)
    {
        Destroy(gameObject);
        agent.isStopped = true;
    }

    private void OnDestroy()
    {
        EnemyManager.instance.COUNT--;
    }

}

