using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase nativa para la correcta carga de escenas
public class GameSceneManager
{
    //M�todo que carga una escena del juego actual. Los nombres de las esceneas siempre est�n en min�sculas
    //Llamar escribiendo: GameSceneManager.LoadScene(nombreEscena)
    /* sceneName: nombre de la escena.
     *      Los nombres disponibles son: menu, level1, level2, boss, bonus1, bonus2, intro, gameOver, end
     */
    public static void LoadScene(string sceneType)
    {
        string sceneNameString = $"{GameData.CurrentGameId}_{sceneType}";
        bool sceneExists = CheckIfSceneExists(sceneNameString);
        if (sceneExists)
            SceneManager.LoadScene(sceneNameString);
    }

    //M�todo que carga la escena de men� del juego actual
    //Llamar escribiendo: GameSceneManager.StartGame()
    public static void StartGame()
    {
        LoadScene("menu");
    }

    public static void ExitGame() 
    {
        string sceneNameString = "ArcadeMenu";
        bool sceneExists = CheckIfSceneExists(sceneNameString);
        if (sceneExists)
            SceneManager.LoadScene(sceneNameString);
    }

    static bool CheckIfSceneExists(string sceneNameString)
    {
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(sceneNameString);
        if (buildIndex != -1)
            return true;
        else
        {
            Debug.LogError($"Escena {sceneNameString} no encontrada en Build Settings");
            return false;
        }
    }
    
    
    //M�todo que carga la siguiente escena del juego actual, cuando el jugador a superado una. En caso de ser la escena final, se regresa al men�
    //Llamar escribiendo: GameSceneManager.NextLevel()
    public static void NextLevel()
    {
        string nextScene = "";
        string currentScene = SceneManager.GetActiveScene().name;
        currentScene = currentScene.Split("_")[1];
        switch (currentScene)
        {
            case "menu": nextScene = "intro";
                break;
            case "intro": nextScene = "level1"; 
                break;
            case "level1": nextScene = "bonus1";
                break;
            case "bonus1": nextScene = "level2";
                break;
            case "level2": nextScene ="bonus2";
                break;
            case "bonus2": nextScene = "boss";
                break;
            case "boss": nextScene = "end";
                break;
            case "end": nextScene = "menu";
                break;
            case "gameOver": nextScene = "level1";
                break;
        }

        LoadScene(nextScene);
    }

    //M�todo que carga la escena de gameOver cuando el jugador pierde. Si se llama mientras se est� en gameOver, se regresa al men�
    //Llamar escribiendo: GameSceneManager.GameOver()
    public static void GameOver()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        currentScene = currentScene.Split("_")[1];
        if (currentScene != "gameOver")
            LoadScene("gameOver");
        else
            LoadScene("menu");
    }
}