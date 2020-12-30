using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboPointsManager : MonoBehaviour
{
    [SerializeField] GameObject _comboPointsTextOB;
    TextMeshProUGUI _comboPointsText;
    public float playerComboPoints = 0;

    private void Awake()
    {
        _comboPointsText = _comboPointsTextOB.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _comboPointsText.SetText("CP: " + playerComboPoints);
    }
}
