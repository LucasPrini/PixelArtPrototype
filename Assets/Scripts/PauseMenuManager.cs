using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _inGameMenuCanvas;

    private void Awake()
    {
        _inGameMenuCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            

            if (Time.timeScale == 0)
            {
                ResumeGame();
                _inGameMenuCanvas.SetActive(false);
            }
            else
            {
                PauseGame();
                _inGameMenuCanvas.SetActive(true);
            }

        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;

    }
}
