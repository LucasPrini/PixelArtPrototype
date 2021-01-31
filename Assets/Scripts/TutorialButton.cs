using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] GameObject _tutorialCanvas;

    private void Start()
    {
        _tutorialCanvas.SetActive(false);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _tutorialCanvas.SetActive(false);
        }
    }
    public void OpenTutorial()
    {
        _tutorialCanvas.SetActive(true);
    }

    public void CloseTutorial()
    {
        _tutorialCanvas.SetActive(false);
    }
}
