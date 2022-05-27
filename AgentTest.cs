using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NavMeshAgent사용할려면 추가
using UnityEngine.AI;

public class AgentTest : MonoBehaviour
{   
    //태어날때 NavMeshAgent를 받는 변수 선언
    NavMeshAgent agent;
    //목적지를 정해준다
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {   
        //태어날때 agent변수에 NavMeshAgent컴포넌트가 담김
        agent = GetComponent<NavMeshAgent>();
        //agent의 자료형이 NavMeshAgent이기 때문에 destination 기능이 있다.]
        //스타트에 있으면 계속 추적 x
        //agent.destination = target.position;

    }

    // Update is called once per frame
    void Update()
    {
        //계속 추적하게 하고싶으면 update에 작성
        agent.destination = target.position;
    }
}
