using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class NextLevelPassage : MonoBehaviour
{
    Animator _sceneTransitionAnimator;
    [SerializeField] GameObject _sceneTransitionOB;

    [SerializeField] GameObject _interactText;



    [SerializeField] bool isInsidePassage;


    private void Awake()
    {
        _sceneTransitionAnimator = _sceneTransitionOB.GetComponent<Animator>();
    }

    private void Start()
    {
        _interactText.SetActive(false);
    }

    private void Update()
    {
        if (isInsidePassage && isRoutineOn == false)
        {


            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(SceneTransitionRoutine());
            }

        }

        if (isInsidePassage)
        {
            _interactText.SetActive(true);
        }
        else
        {
            _interactText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInsidePassage = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInsidePassage = false;
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
