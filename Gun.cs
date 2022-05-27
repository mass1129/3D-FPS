using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{   
    public ParticleSystem bulletImpact;
    // Start is called before the first frame update
    //Ray를 이용해서 바라보고 닿은곳에 총을 쏘고싶다. (총알자국을 남기고싶다.)
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //만약에 마우스 왼쪽버튼을 누르면
        if(Input.GetButton("Fire1"))
        {
        //1.시선을 만들고 Ray ray = new Ray(원점, 방향);
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //2. 그 시선을 이용해서 바라봤는데 만약 닿은곳이 있다면?
        RaycastHit hitInfo;
        //Phsics클리스에 Raycast는 바라보는것, hitInfo에 닿은곳의 정보가 담김, out
        if (Physics.Raycast(ray, out hitInfo))
        {   
            //3. 닿으공 정보를 출력하고싶다.
            print(hitInfo.transform.name);
            //3. 닿은곳에 총알자국을 가져다놓고싶다.hitInfo.transform.position은 중점.
            //우리가 놓고싶은곳은 부딪힌 겉면.  hitInfo.point
            bulletImpact.transform.position = hitInfo.point;
            //4. 총알자국 VFX를 재생하고싶다.
            bulletImpact.Stop();
            bulletImpact.Play();
            //5. 총알자국의 방향을 닿은곳의 Nomal방향으로 회전하고싶다. nomalize은 정규화.nomal은 수직벡터.
            //총알자국의 forward방향과 닿은곳의 Normal방향을 일치시키고싶다.
            bulletImpact.transform.forward = hitInfo.normal;

            //만약 hitInfo가 Enemy컴포넌트를 가지고있다면?
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
            //enemy의 AddDamge의 함수를 호출하고싶다.
            enemy.AddDamage(1);
            }
        }
    }
    }
}