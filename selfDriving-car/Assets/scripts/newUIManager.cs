using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newUIManager : MonoBehaviour
{
    public GeneticManger manger;
    public Text genration,maxFitness,CurrentMax;
    public Slider slider;
    void Start()
    {
        
    }

    // Getting Information From genetic Manager And Displaying It On the Screen
    void Update()
    {
        genration.text="Genration: "+manger.genration.ToString();
        maxFitness.text="Max Fitness: "+manger.maxFitness.ToString();
        CurrentMax.text="Current Max Fitness: "+manger.firstFitness.ToString();
        Time.timeScale=slider.value;
        
    }
}
