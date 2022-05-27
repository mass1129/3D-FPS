using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{   
    private void Awake()
    {
       
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //사다리와 플레이어의 접촉이 감지되면 PlayerMove에 ladder값을 참으로 만든다.
     private void OnTriggerEnter(Collider other)
    {
        if(other.name =="Player")
        {
            other.GetComponent<PlayerMove>().ladder = true;
        }
    }
    
    //사다리와 플레이어의 접촉이 끝나면 PlayerMove에 ladder값을 거짓으로 만든다.
    private void OnTriggerExit(Collider other)
    {
        if(other.name =="Player")
        {
            other.GetComponent<PlayerMove>().ladder = false;
        }
    }
    
}
