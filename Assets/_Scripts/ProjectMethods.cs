using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectMethods : MonoBehaviour {

    public static float TwoDecimalRound(float x)
    {
        x = Mathf.Round(x * 100) / 100;
        return x;
    }

    public static void NoGameControllerComponentMsg()
    {
        Debug.LogError("There is no GameController component in the scene");
    }

    public static void NoLivesComponentMsg()
    {
        Debug.LogError("There is no Lives component found in the scene");
    }

    public static void NoPlayerObjectMsg()
    {
        Debug.LogError("There is no gameObject with tag 'Player' found in the scene");
    }

    public static void NoAlienControllerComponentMsg()
    {
        Debug.LogError("There is no AlienController component found in the scene");
    }

    public static void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
