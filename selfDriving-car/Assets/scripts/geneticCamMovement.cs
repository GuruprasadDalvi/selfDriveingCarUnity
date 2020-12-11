using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geneticCamMovement : MonoBehaviour
{
    //Initilizing Veriables
    public GeneticManger manger;
    public float xOffset,yOffset,zOffset;

    void Update()
    {
            Vector3 ps;
            try{
                // Moving Camera Towards the Best Performing Car
                ps.x=manger.bestCar.transform.position.x+xOffset;
                ps.y=manger.bestCar.transform.position.y+yOffset;
                ps.z=manger.bestCar.transform.position.z+zOffset;
                transform.position=Vector3.Lerp(transform.position,ps,0.05f);
            }
            catch{
                Debug.Log("Best Not Found!!");
            }
        
    }
}
