using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuntimeSetup : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void RuntimeInitializeOnLoad()
    {
        Application.targetFrameRate = 120;

        var loadedScenes = SceneManager.sceneCount;
        for (int i = 0; i < loadedScenes; i++)
        {
            if(SceneManager.GetSceneAt(i).name == "Singletons")
            {
                return;
            }   
        }

        SceneManager.LoadScene("Singletons", LoadSceneMode.Additive);
    }
}
