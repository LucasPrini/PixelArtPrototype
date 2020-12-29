using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class GreenLight : MonoBehaviour
{
    //GameObject _greenLight;
    Light2D _greenLight;
    public bool canLit = false;


    // Start is called before the first frame update
    void Start()
    {
        _greenLight = GetComponent<Light2D>();
        //UnityEngine.Experimental.Rendering.Universal.Light2D The2DLights = _greenLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }

    private void Update()
    {
        if (canLit)
        {
            _greenLight.intensity = Mathf.PingPong(Time.time * 12, 8);
        }
        else
        {
            _greenLight.intensity = 0;
        }
    }

    public void litLight()
    {
        //_greenLight.intensity = Mathf.PingPong(Time.time * 10, 8);
    }


}
