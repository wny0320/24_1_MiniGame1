using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    // 코딩 스타일이고 회사마다 다르긴 한데 보통 private인 변수는 첫글자 소문자, public은 첫글자 대문자를 주로 씁니다
    [SerializeField, Range(0,10)] // 마우스와 마찬가지
    float walkSpeed = 6.0f;              // 걷는 속도

    // 애니메이션
    //Animator animator;  

    // 여기는 스타일마다 달라서 함수라면 모두 대문자 쓰시는 분들이 있고,
    // 변수와 마찬가지로 private라면 소문자 public이라면 대문자 쓰시는 분들 있어서 이건 취향입니다
    private void playerMove()
    {
        // 좌우 앞뒤 이동

        float inputY = 0;
        float inputX = 0;
        if (Input.GetKey(KeyCode.W)) inputY = 1;
        if (Input.GetKey(KeyCode.S)) inputY = -1;
        if (Input.GetKey(KeyCode.D)) inputX = 1;
        if (Input.GetKey(KeyCode.A)) inputX = -1;


        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * walkSpeed);  // 키보드에 따른 방향으로 이동속도 만큼 이동 
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();


        // this.animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //마찬가지로 함수로 만들어서 업데이트 함수 안에 넣어주세요
        playerMove();
    }
}
