using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    roaming,
    chasingTarget,
    attackingTarget,
}
public class WizardState : MonoBehaviour
{
    WizardAI _wizardAI;
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        _wizardAI = GetComponent<WizardAI>();
    }

    // Update is called once per frame
    void Update()
    {
        StateController();
    }

    void StateController()
    {

    }
}
