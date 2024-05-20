using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance = null;
    public static Manager Instance { get { Init(); return instance; } }

    #region Managers
    GameManager _game = new GameManager();
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    BulletPoolManager _bullet = new BulletPoolManager();
    UIManager _ui = new UIManager();

    public static GameManager Game { get { return instance._game; } }
    public static DataManager Data { get { return instance._data; } }
    public static InputManager Input { get { return instance._input; } }
    public static BulletPoolManager Bullet { get { return instance._bullet; } }
    public static UIManager UI { get { return instance._ui; } }
    #endregion

    private void Awake()
    {
        Init();
        Bullet.OnAwake();
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        Input.OnUpdate();        
    }

    private static void Init()
    {
        if(instance == null)
        {
            GameObject go = FindObjectOfType<Manager>().gameObject;
            if(go == null)
            {
                go = new GameObject { name = "Manager" };
                go.AddComponent<Manager>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Manager>();
        }
    }
}
