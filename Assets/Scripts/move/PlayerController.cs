using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float WalkSpeed = 6.0f;              // 걷는 속도

    // 애니메이션
    //Animator animator;  


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

        // 좌우 앞뒤 이동
        
        float inputY = 0;
        float inputX = 0 ;
        if (Input.GetKey(KeyCode.W)) inputY = 1;
        if (Input.GetKey(KeyCode.S)) inputY = -1;
        if (Input.GetKey(KeyCode.D)) inputX = 1;
        if (Input.GetKey(KeyCode.A)) inputX = -1;
          

        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * WalkSpeed);  // 키보드에 따른 방향으로 이동속도 만큼 이동 




    }
}
