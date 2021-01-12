using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockPopUp : MonoBehaviour
{
    public TextMeshPro textMesh;
    public int damageReceived;


    // Start is called before the first frame update
    private void Awake()
    {

        textMesh = GetComponent<TextMeshPro>();
    }
    void Start()
    {

        textMesh.SetText("Blocked!");
        StartCoroutine(FloatingDamageRoutine());
    }

    IEnumerator FloatingDamageRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0f, 0.9f) * Time.deltaTime);


    }


}