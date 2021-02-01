using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCredits : MonoBehaviour
{
    GameObject _bossOB;
    [SerializeField] GameObject _creditsCanvas;

    private void Awake()
    {
        _bossOB = GameObject.Find("BeholderBoss");
    }
    // Start is called before the first frame update
    void Start()
    {
        _creditsCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_bossOB == null && isCreditsRoutineOn == false)
        {
            StartCoroutine(EndingCreditsRoutine());
        }
    }

    bool isCreditsRoutineOn;
    IEnumerator EndingCreditsRoutine()
    {
        isCreditsRoutineOn = true;
        yield return new WaitForSeconds(3);

        _creditsCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}
