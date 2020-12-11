using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speedUpPtorcess : MonoBehaviour
{
    public Slider timeScapleSlider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale=timeScapleSlider.value;
        
    }
}
