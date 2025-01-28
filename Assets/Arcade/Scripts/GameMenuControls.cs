using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade{
public class GameMenuControls : MonoBehaviour
{
    float time = 5f;

    IEnumerator Start(){
        yield return new WaitForSeconds(time);
        GameSceneManager.ExitGame();
    }

    public void BeginGame() 
    {
        GameSceneManager.NextLevel();
    }

    private void Update() {
        if(Input.GetButtonDown("P1_Start") || Input.GetButtonDown("P2_Start"))
            BeginGame();
    }
}

}