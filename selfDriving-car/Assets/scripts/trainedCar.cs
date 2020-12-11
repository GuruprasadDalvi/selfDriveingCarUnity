using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainedCar : MonoBehaviour
{
    NeuralNetwork myBain;
    private Vector3 inputDirection,sPosition,fiveSecPosition;
    public GameObject leftWheel,rightWheel,backLeftWheel,backRightWheel;
    int wheelrotarion;
    public float[,] outputs;
    void Start()
    {
        myBain=new NeuralNetwork();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        myBain.load();
        
    }
    void moveCar(float acc, float rot)
    {
        this.inputDirection = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, acc * 10), 0.02f);
        this.inputDirection = transform.TransformDirection(this.inputDirection);
        transform.position += this.inputDirection;
        transform.eulerAngles += new Vector3(0, rot, 0);
        leftWheel.transform.eulerAngles= new Vector3(wheelrotarion++, transform.eulerAngles.y, 0);
        rightWheel.transform.eulerAngles= new Vector3(wheelrotarion++, transform.eulerAngles.y, 0);
        backLeftWheel.transform.eulerAngles= new Vector3(wheelrotarion++, transform.eulerAngles.y, 0);
        backRightWheel.transform.eulerAngles= new Vector3(wheelrotarion++, transform.eulerAngles.y, 0);
    }

    float[,] getInput(){
        float[,] inputs;
        Vector3 F = (transform.forward);
        Vector3 FR = (transform.forward + transform.right);
        Vector3 FL = (transform.forward - transform.right);
        Vector3 B= (-transform.forward);
        RaycastHit hit;
        Ray Fr,FRr,FLr,Br;

        inputs= new float[4,1];
        // for(int i =0;i<4;i++)
        //     inputs[i,0]=Mathf.Infinity;
        Fr=new Ray(transform.position,F);
        FRr=new Ray(transform.position,FR);
        FLr=new Ray(transform.position,FL);
        Br=new Ray(transform.position,B);

        // Debug.DrawRay(transform.position, Fr.direction * this.sensorLength, Color.white, 0f);
        // Debug.DrawRay(transform.position, FRr.direction * this.sensorLength, Color.white, 0f);
        // Debug.DrawRay(transform.position, FLr.direction * this.sensorLength, Color.white, 0f);
        // Debug.DrawRay(transform.position, Br.direction * this.sensorLength, Color.white, 0f);

        Physics.Raycast(Fr, out hit);
        if( hit.distance>0){
            inputs[0,0]=hit.distance;
            Debug.DrawRay(transform.position, Fr.direction * hit.distance, Color.red, 0f);
        }

        Physics.Raycast(FRr, out hit);
        if( hit.distance>0){
            inputs[1,0]=hit.distance;
            Debug.DrawRay(transform.position, FRr.direction * hit.distance, Color.red, 0f);
        }

        Physics.Raycast(FLr, out hit);
        if( hit.distance>0){
            inputs[2,0]=hit.distance;
            Debug.DrawRay(transform.position, FLr.direction * hit.distance, Color.red, 0f);
        }

        Physics.Raycast(Br, out hit);
        if( hit.distance>0){
            inputs[3,0]=hit.distance;
            Debug.DrawRay(transform.position, Br.direction * hit.distance, Color.red, 0f);
        }

        return inputs;
    }

    void FixedUpdate(){
        float[,] inputs=getInput();
        this.outputs=this.myBain.feedForward(inputs);
        moveCar(outputs[0,0],outputs[1,0]);
    }

}
