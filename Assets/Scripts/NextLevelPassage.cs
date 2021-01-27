using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextLevelPassage : MonoBehaviour
{
    Animator _sceneTransitionAnimator;
    [SerializeField] GameObject _sceneTransitionOB;

    private void Awake()
    {
        _sceneTransitionAnimator = _sceneTransitionOB.GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && isRoutineOn == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(SceneTransitionRoutine());
            }
        }
    }

    bool isRoutineOn;
    IEnumerator SceneTransitionRoutine()
    {
        isRoutineOn = true;
        _sceneTransitionAnimator.SetTrigger("StartSceneTransition");
        yield return new WaitForSeconds(1.35f);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex + 1);
        isRoutineOn = false;

    }




}
