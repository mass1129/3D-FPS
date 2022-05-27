using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//마우스의 입력값을 이용해서 회전하고 싶다.
public class CameraRotate : MonoBehaviour
{   
    //마우스의 값을 누적하기위해 전역변수 
    float rx;
    float ry;
    //deltaTime이 속도를 1/60으로 감소시키기때문에 증폭해준다.
    public float rotSpeed = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //마우스의 입력값을 이용해서 
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        //마우스를 x축(좌우)으로 움직일때 y축을 회전하고 싶다.**transform정보를 변경할때는 deltaTime을 곱해야한다.
        rx += rotSpeed*my*Time.deltaTime;
        //마우스를 y축(상하)으로 움직일때 x축을 회전하고 싶다.
        ry += rotSpeed*mx*Time.deltaTime;
        //print(mx+ "," +my);
        //rx의 회전 각을 제한하고 싶다.  +80 ~ -80
        //Mathf유틸리티 클래스에 Clamp가 있는데 현재값(rx)가 최솟값과 최대값사이에 들어갈수있도록 해준다.
        rx = Mathf.Clamp(rx,-80, 80);
        //회전하고 싶다. 
        transform.eulerAngles = new Vector3(-rx,ry,0);
    }

    
}
