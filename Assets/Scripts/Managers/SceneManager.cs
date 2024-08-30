using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager
{
    public void ChangeScene(SceneType Scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneBuildIndex: (int)Scene);
    }

    //나중에 씬 별 처리 작업 함수 필요시 작성
}