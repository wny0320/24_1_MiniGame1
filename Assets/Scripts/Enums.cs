//이곳에 enums 들 정의
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
    // Idle 상태가 사실상 필요 없어보임, 컨셉이나 멋 때문이 아니면 기능상으로는 불필요
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