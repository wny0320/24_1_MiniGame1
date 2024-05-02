using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected float _moveSpeed;

    //보스 공격 주기 & 플레이어 무기별 공격속도는 무기에 붙이는게 더 나을듯
    //기본 무기가 지급된다면 기본 무기 공격 속도로 사용?
    [SerializeField]
    protected float _attackSpeed;

    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }

    private void Start()
    {
        Init();
    }

    //피격은 인터페이스로 할건지 밑의 함수로 할건지 정해야 될듯
    public virtual void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack - Defense);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            //Ondead - 사망처리
        }
    }

    private void Init() //enum으로 지정된 보스 플레이어 데이터 가져오던지 or 매개변수로 그냥 다 넘기던지
    {
        //스탯 초기값
    }
}
