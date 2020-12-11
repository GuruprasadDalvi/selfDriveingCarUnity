using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCarController : MonoBehaviour
{
    public float fitness,front,left,right,back;

    float traveledDistance;

    public NeuralNetwork myBrin;
    public float[,] outputs;
    public bool alive;

    public int incrementer;

    private Vector3 inputDirection;

    void Start()
    {
        this.alive=true;
        if(incrementer<1)
            this.myBrin=new NeuralNetwork();
        this.traveledDistance=0f;
        this.fitness=0;
        myBrin.load();
        
        
    }

    void Update()
    {
        
    }

    void FixedUpdate(){
        if(this.alive){
            float[,] inputs=getInput();
            this.outputs=this.myBrin.feedForward(inputs);
            moveCar(this.outputs[0,0],this.outputs[1,0]);
        }

    }


    void OnTriggerEnter(Collider ob){
        if(ob.tag=="wall" && this.alive){
            this.alive=false;
        }
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
        Fr=new Ray(transform.position,F);
        FRr=new Ray(transform.position,FR);
        FLr=new Ray(transform.position,FL);
        Br=new Ray(transform.position,B);


        Physics.Raycast(Fr, out hit);
        if( hit.distance!=0 && hit.collider.tag=="wall"){
            inputs[0,0]=hit.distance;
            this.front=hit.distance;
            Debug.DrawRay(transform.position, Fr.direction * hit.distance, Color.red, 0f);
        }
        else{
        Debug.DrawRay(transform.position, Fr.direction * hit.distance, Color.white, 0f);
        }

        Physics.Raycast(FRr, out hit);
        if( hit.distance!=0 && hit.collider.tag=="wall"){
            inputs[1,0]=hit.distance;
            this.right=hit.distance;
            Debug.DrawRay(transform.position, FRr.direction * hit.distance, Color.red, 0f);
        }
        else{
        Debug.DrawRay(transform.position, FRr.direction * hit.distance, Color.white, 0f);
        }

        Physics.Raycast(FLr, out hit);
        if( hit.distance!=0 && hit.collider.tag=="wall"){
            inputs[2,0]=hit.distance;
            this.left=hit.distance;
            Debug.DrawRay(transform.position, FLr.direction * hit.distance, Color.red, 0f);
        }
        else{
        Debug.DrawRay(transform.position, FLr.direction * hit.distance, Color.white, 0f);
        }

        Physics.Raycast(Br, out hit);
        if( hit.distance!=0 && hit.collider.tag=="wall"){
            inputs[3,0]=hit.distance;
            this.back=hit.distance;
            Debug.DrawRay(transform.position, Br.direction * hit.distance, Color.red, 0f);
        }
        else{
        Debug.DrawRay(transform.position, Br.direction * hit.distance, Color.white, 0f);
        }

        return inputs;
    }




    void moveCar(float acc, float rot)
    {
        this.inputDirection = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, acc * 10), 0.02f);
        this.inputDirection = transform.TransformDirection(this.inputDirection);
        transform.position += this.inputDirection;
        transform.eulerAngles += new Vector3(0, rot, 0);
    }


}
