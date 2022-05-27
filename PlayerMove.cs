using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{   
    public float speed =5;
    public float gravity = -9.81f;
    public float jumpPower = 10;
    float yVelocity;
    //사다리에 접촉 여부 변수, 접촉시 참, 비접촉시 거짓.
    public bool ladder;

    CharacterController cc;
     public enum State
    {   
        //이동
        Move,
        //사다리 접촉
        OnLadder,
        //사다리 타기
        Climbing,
        
       
    }

    public State state;

    // Start is called before the first frame update
    void Start()
    {   
        //초기상태는 "이동"
        state = State.Move;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {      
        if(state == State.Move)
        {
            UpdateMove();
        }
        else if(state == State.OnLadder)
        {
            UpdateOnLadder();
        }
        else if(state == State.Climbing)
        {
            UpdateClimbing();
        }
        

    }
        //이동하다가 사다리에 닿으면 "사다리 접촉"상태로 전이
        private void UpdateMove()
        {
        
        yVelocity += gravity*Time.deltaTime;
       
        if(Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
        }
        float h = Input.GetAxis("Horizontal");

        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;

        dir= Camera.main.transform.TransformDirection(dir);

        dir.Normalize();

        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
        //사다리와 충돌이 감지되었을때
        if (ladder)
        {   
            //"사다리 접촉" 상태로 전이
            state= State.OnLadder;
            
        }
        }

        //사다리 접촉시 시작지점을 기억하기위해 변수 선언
        public Vector3 ldStartPosition;
        private void UpdateOnLadder()
        {   
            //키보드 "W", "S"를 입력받아
            float f1 = Input.GetAxisRaw("Vertical");
            // 사다리에 닿은상태에서(이 조건을 안넣으면 W키 다운시 끊임없이 "Climbing"상태) W를 누르면
            if(f1>0&&ladder)
            {   
                //사다리타기 시작지점을 저장하고(사다리에서 내려오기 기능을 구현하기 위해 이 변수가 필요)
                ldStartPosition = transform.position;
                //"Climbing"상태로 전이
                state= State.Climbing;
                
            }
            //추가적으로 사다리 내려오는 기능 구현
            //"OnLadder"상태에서 D를 누르면
            else if (f1<0)
            {   
                //다시 "Move"상태로 전이
                state=State.Move;
            }
            
        }

        
        
        private void UpdateClimbing()
        {   
            //"사다리 타기" 상태일 때는
            //키보드의 입력값을 받아서
            float f = Input.GetAxisRaw("Vertical");
            //y축 방향으로 벡터를 만들고
            Vector3 dir = new Vector3(0,f,0);
            //노멀라이즈한뒤
            dir.Normalize();
            //그 방향으로 이동한다.
            cc.Move(dir * speed * Time.deltaTime);
            //상하로 이동하다가 끝지점에서 사다리와 떨어지면
            if(!ladder)
            {   //이동상태로 전이
                state= State.Move;
            
            }
            //사다리를 타다가 아래방향키를 눌러 내려와서 시작지점으로 돌아오면
            else if(transform.position == ldStartPosition)
            {   
                //사다리 접촉상태로 전이->여기서 s누를시 "Move"상태로 전이 ->사다리에서 내려오기 기능
                state = State.OnLadder;
            }
            
        
      
}

}
