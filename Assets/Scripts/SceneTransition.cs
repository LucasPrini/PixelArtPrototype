using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] GameObject _levelNameTextOB;
    TextMeshProUGUI _levelNameTextUI;

    int SceneBuildIndex;

    private void Awake()
    {
        _levelNameTextUI = _levelNameTextOB.GetComponent<TextMeshProUGUI>();
        GetSceneIndex();
        if (SceneBuildIndex == 1)
        {
            _levelNameTextUI.SetText("The Prison");
        }
        if (SceneBuildIndex == 2)
        {
            _levelNameTextUI.SetText("The Tower");
        }
    }


    private void Start()
    {

    }

    void GetSceneIndex()
    {
        SceneBuildIndex = EditorSceneManager.GetActiveScene().buildIndex;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }


}
