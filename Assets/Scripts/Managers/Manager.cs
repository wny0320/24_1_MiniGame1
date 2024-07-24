using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance = null;
    public static Manager Instance { get { Init(); return instance; } }
    public Coroutine nowPattern = null;

    #region Managers
    GameManager _game = new GameManager();
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    BulletPoolManager _bullet = new BulletPoolManager();
    UIManager _ui = new UIManager();
    SceneManager _scene = new SceneManager();

    public static GameManager Game { get { return instance._game; } }
    public static DataManager Data { get { return instance._data; } }
    public static InputManager Input { get { return instance._input; } }
    public static BulletPoolManager Bullet { get { return instance._bullet; } }
    public static UIManager UI { get { return instance._ui; } }
    public static SceneManager Scene { get { return instance._scene; } }
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

    public void InvokePattern(object _target, MethodInfo _method)
    {
        if(nowPattern == null)
        {
            IEnumerator co = (IEnumerator)_method.Invoke(_target, _method.GetParameters());
            nowPattern = StartCoroutine(co);
        }
    }
}
