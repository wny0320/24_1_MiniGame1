//�̰��� enums �� ����
public enum WeaponType
{
    None,
}

public enum PlayerState
{
    Idle,
    Move,
    Dodge,
    Parrying,
    Die,
}

public enum BossState
{
    // Idle ���°� ��ǻ� �ʿ� �����, �����̳� �� ������ �ƴϸ� ��ɻ����δ� ���ʿ�
    Idle,
    Move,
    Pattern,
    Die,
}

public enum Boss1Pattern
{
    Pattern0,
    Pattern1,
    Pattern2,
    Pattern3,
    Pattern4,
    Pattern5,
}
public enum NowBoss
{
    Null,
    Boss1,
    Boss2,
    Boss3,
    Boss4,
}

public enum SceneType
{
    Main = 0,
    menutest = 1,   //START Scene
    Stage1 = 2,     //BUllet Scene
}