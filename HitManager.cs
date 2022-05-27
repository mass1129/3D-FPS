using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitManager : MonoBehaviour
{   
    public static HitManager instance;
    public GameObject imageHit;
    //검은화면 이미지
    public GameObject imageGameOver;
    //텍스트 "Game Over"
    public GameObject textGameOver;

    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        imageHit.SetActive(false);
        //태어날 때 imageGameOver를 보이지 않게 하고싶다.
        imageGameOver.SetActive(false);
        //태어날 때 textGameOver를 보이지 않게 하고싶다.
        textGameOver.SetActive(false);
    }

    IEnumerator IeGameOVer()
    {
        //1. imageGameOver를 보이게 하고싶다.
        imageGameOver.SetActive(true);
        //2. 1초 후에
        yield return new WaitForSeconds(1f);
        //3.textGameOver를 보이게 하고싶다.
        textGameOver.SetActive(true);
    }

    public void GameOver()
    {   
        //게임오버 화면을 띄우고싶다.
        StartCoroutine("IeGameOVer");
    }

    IEnumerator IeHit()
    {
        
        imageHit.SetActive(true);
        
        yield return new WaitForSeconds(0.1f);
       
        imageHit.SetActive(false);
    }
    public void Hit()
    {
        
        StartCoroutine("IeHit");
        
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
