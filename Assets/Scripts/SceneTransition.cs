using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    int SceneBuildIndex;

    void GetSceneIndex()
    {
        SceneBuildIndex = EditorSceneManager.GetActiveScene().buildIndex;
    }
}
