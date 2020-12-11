using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newCarController : MonoBehaviour
{
    public float fitness,time,interval,front,left,right,back,distance;

    float traveledDistance;
    public List<Collider> completedCheckPoints;
    public GameObject leftWheel,rightWheel,backLeftWheel,backRightWheel;

    public NeuralNetwork myBrin;
    public float[,] outputs;
    public bool alive;

    public int incrementer;
    private float preFitness=0;

    private Vector3 inputDirection;
    public int wheelrotarion;

    void Start()
    {
        this.completedCheckPoints=new List<Collider>();
        this.alive=true;
        if(incrementer<1)
            this.myBrin=new NeuralNetwork();
        this.traveledDistance=0f;
        this.fitness=0;
        
        
    }

    void Update()
    {
        this.time+=Time.deltaTime;
        this.interval+=Time.deltaTime;
        if(this.interval>2f){                                                               //Die IF Position Not Changed For 2 Sceonds
            if(preFitness==fitness && this.alive){
                this.alive=false;
            }
            interval=0f;
            preFitness=fitness;
        }
        
    }

    void FixedUpdate(){
        if(this.alive){                                                                     //Moving Car Using Neural Network(Brain Of the Car)
            float[,] inputs=getInput();
            this.outputs=this.myBrin.feedForward(inputs);
            moveCar(this.outputs[0,0],this.outputs[1,0]);
        }

    }

    void OnTriggerEnter(Collider ob){
        if(ob.tag=="CheckPoint"){                                                           //Incrementing Fitness On each CheckPoint if Not Already Visited
            if(!this.completedCheckPoints.Contains(ob)){
            this.distance+=1;
            this.fitness+=10;
            this.completedCheckPoints.Add(ob);
            }
        }
        if(ob.tag=="wall" && this.alive){                                                   //Die if Hit The Wall
            this.alive=false;
        }
    }



    float[,] getInput(){                                                                    //Getting Inputs With RayCasting In All Four Direction
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
        /*
            I Took This Function From "https://www.youtube.com/watch?v=C6SZUU8XQQ0" This Video 
            And Modified a little 
            Highly Suggest You To Watch That Serise If You Want To make A Self Driving Car In Uinty
        */
        this.inputDirection = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, acc * 10), 0.02f);
        this.inputDirection = transform.TransformDirection(this.inputDirection);
        transform.position += this.inputDirection;                                           //Accelerating Car
        transform.eulerAngles += new Vector3(0, rot, 0);                                    //Rotating Car
        //Rotating Wheels
        leftWheel.transform.eulerAngles= new Vector3(wheelrotarion++, transform.eulerAngles.y, 0);
        rightWheel.transform.eulerAngles= new Vector3(wheelrotarion++, transform.eulerAngles.y, 0);
        backLeftWheel.transform.eulerAngles= new Vector3(wheelrotarion++, transform.eulerAngles.y, 0);
        backRightWheel.transform.eulerAngles= new Vector3(wheelrotarion++, transform.eulerAngles.y, 0);
    }


}
