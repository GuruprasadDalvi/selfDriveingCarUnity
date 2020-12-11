using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGeneticManger : MonoBehaviour
{
    public List<GameObject> cars;
    public GameObject car;
    public newCarController currCarController;
    bool bainSet=false;
    void Start()
    {   
        Instantiate(car);
        currCarController=car.GetComponent<newCarController>();
        
    }

    void Update()
    {
        if(!bainSet){
        //currCarController.myBrin.load();
        bainSet=true;
        }
        
    }
    
    
}
