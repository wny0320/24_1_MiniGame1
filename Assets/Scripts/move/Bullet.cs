using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        BulletDestroy();

    }


    void BulletDestroy()
    {
        if (transform.position.y > 9 || transform.position.x > 9)
        {
            Destroy(gameObject);
        }
    }
}
