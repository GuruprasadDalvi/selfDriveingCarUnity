using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticManger : MonoBehaviour
{    //Initilizing Veriables
    public List<GameObject> cars;                                                           //List For Car Population
    public int population,genration;
    public GameObject car,bestCar;                                                          //Car= CarPrefab; bestCar= Best performing Car Of Genration
    public newCarController currCarController;                                              //For Getting CarController Component
    public float maxFitness,firstFitness,secondFitness;
    public NeuralNetwork firstFitBrain,secondFitBrain,fitBrain;                             //For brains Of Best performers 
    void Start()
    {   
        maxFitness=firstFitness=secondFitness=0;                                            //Seting All Fitness To Zero
        firstFitBrain=new NeuralNetwork();
        secondFitBrain=new NeuralNetwork();
        makePopulatio();
        
    }

    void Update()
    {
        for(int i=0;i<this.cars.Count;i++)
        {
            currCarController=cars[i].GetComponent<newCarController>();

            if(currCarController.fitness>maxFitness){                                       //Updateing Max Fitness
                maxFitness=currCarController.fitness;
                fitBrain=currCarController.myBrin;
                fitBrain.save(maxFitness);
            }
            if(currCarController.fitness>this.firstFitness){                                //Setting New First Fit Parents
                this.firstFitness=currCarController.fitness;
                this.firstFitBrain=currCarController.myBrin;
                bestCar=cars[i];
            }
            if(currCarController.fitness<this.firstFitness && currCarController.fitness>secondFitness){                                //Setting New Second Fit Parents
                this.secondFitness=currCarController.fitness;
                this.secondFitBrain=currCarController.myBrin;
            }

            if(!currCarController.alive){
                cars[i].SetActive(false);
                Destroy(cars[i]);                                                           //Remove Dead Offsprings
                cars.Remove(cars[i]);
            }

        }
        if(this.cars.Count==0){
            makeNewGenration();                                                            //Makeing New Genration
        for(int i=0;i<this.population;i++){                                                //Mutating And Inheriting New Genration
            currCarController=cars[i].GetComponent<newCarController>();
            currCarController.incrementer=genration;
            NeuralNetwork brain= new NeuralNetwork();
            currCarController.myBrin=brain;
            currCarController.myBrin.inherit(firstFitBrain,secondFitBrain);                 //Inherite From Best Two Of Previous Gemration
            if (this.fitBrain!=null && (Random.Range(1,100)<20))
            {
                currCarController.myBrin.inherit(currCarController.myBrin,this.fitBrain);   //Inherite The best Brain From Past
            }
            currCarController.myBrin.mutate();
        }
        }
        
    }
    void makePopulatio(){                                                                   //Makes Population
        for(int i=0;i<this.population;i++){
            GameObject c= Instantiate(car);
            cars.Add(c);
        }
        bestCar=cars[1];
    }

    void makeNewGenration(){                                                                //Makes new Genration
        genration+=1;
        makePopulatio();
        firstFitness=0;
        secondFitness=0;
    }
    
    
}
