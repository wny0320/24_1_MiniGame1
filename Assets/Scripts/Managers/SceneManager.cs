using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager
{
    public void ChangeScene(SceneType Scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneBuildIndex: (int)Scene);
    }

    //���߿� �� �� ó�� �۾� �Լ� �ʿ�� �ۼ�
}