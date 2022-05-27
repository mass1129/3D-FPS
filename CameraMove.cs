using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{   
    //마우스의 값을 누적하기위해 전역변수
    float wheel;

    // Start is called before the first frame update
    void Start()
    {   
        //처음 프로젝트 재생할때 카메라 위치를 초기화한다.(부모객체가 (0,0,0)에 위치라고 가정할때 z축으로 -5만큼 떨어진 위치)
        transform.localPosition = new Vector3(0,0,-5);
    }

    // Update is called once per frame
    void Update()
    {   
        //1. 마우스의 입력값을 이용해서
        //마우스휠 입력값을 wheel1에 받아서
        float wheel1 = Input.GetAxis("Mouse ScrollWheel");
        //누적시킨다.
        wheel += wheel1;
        //끊임없이 누적되어 카메라 이동이 딜레이 되지 않도록 값을 제한한다.
        wheel = Mathf.Clamp(wheel,0,5);
        //2. 카메라이동
        //Mathf.Lerp를 이용하여 position.z 가 -5에서 0사이를 이동하도록한다.
        transform.localPosition = new Vector3(0,0,Mathf.Lerp(-5,0,wheel/5));
    }
}