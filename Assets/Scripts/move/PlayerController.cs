using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    // �ڵ� ��Ÿ���̰� ȸ�縶�� �ٸ��� �ѵ� ���� private�� ������ ù���� �ҹ���, public�� ù���� �빮�ڸ� �ַ� ���ϴ�
    [SerializeField, Range(0,10)] // ���콺�� ��������
    float walkSpeed = 6.0f;              // �ȴ� �ӵ�
    [SerializeField]
    private GameObject ground;

    // �ִϸ��̼�
    //Animator animator;  

    // ����� ��Ÿ�ϸ��� �޶� �Լ���� ��� �빮�� ���ô� �е��� �ְ�,
    // ������ ���������� private��� �ҹ��� public�̶�� �빮�� ���ô� �е� �־ �̰� �����Դϴ�
    private void playerMove()
    {
        // �¿� �յ� �̵�

        float inputY = 0;
        float inputX = 0;
        if (Input.GetKey(KeyCode.W)) inputY = 1;
        if (Input.GetKey(KeyCode.S)) inputY = -1;
        if (Input.GetKey(KeyCode.D)) inputX = 1;
        if (Input.GetKey(KeyCode.A)) inputX = -1;


        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * walkSpeed);  // Ű���忡 ���� �������� �̵��ӵ� ��ŭ �̵�
        transform.position = playerClipping();
    }
    /// <summary>
    /// �÷��̾ �����̴� ���� �׸��� ����, �ٸ� ����� ��� ���� �ʿ�
    /// �߰��� �÷��̾��� ũ�⸦ �����
    /// </summary>
    /// <returns>Vector3, player�� clipping�� ��ǥ</returns>
    private Vector3 playerClipping()
    {
        if (ground == null) return Vector3.zero;

        Transform groundTrans = ground.transform;
        Vector3 groundPos = groundTrans.position;
        Vector3 groundScale = groundTrans.lossyScale;

        Vector3 playerPos = transform.position;
        Vector3 playerScale = transform.lossyScale;

        // Ŭ������ �簢���� ���� �Ʒ����� ������ ���� ���� ����
        Vector2 clipSquareLeftDown = groundPos - groundScale/2 + playerScale/2;
        Vector2 clipSquareRightUp = groundPos + groundScale/2 - playerScale/2;

        float clipedXPos = Mathf.Clamp(playerPos.x, clipSquareLeftDown.x, clipSquareRightUp.x);
        float clipedYPos = Mathf.Clamp(playerPos.y, clipSquareLeftDown.y, clipSquareRightUp.y);
        return new Vector3(clipedXPos, clipedYPos);
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
        //���������� �Լ��� ���� ������Ʈ �Լ� �ȿ� �־��ּ���
        playerMove();
    }
}
