using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arcade{
public class GameOverControls : MonoBehaviour
{
    float time = 5f;

    IEnumerator Start(){
        yield return new WaitForSeconds(time);
        GameSceneManager.GameOver();
    }

    void Update()
    {
        if(Input.GetButtonDown("P1_Start") || Input.GetButtonDown("P2_Start"))
            GameSceneManager.NextLevel(); 
    }
}
}
