using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NeuralNetwork
{
    /*
        Network Structure
            One InputLayer:     4 Neurons
            One HiddenLayer:    4 Neurons
            One OutputLayer:    2 Neurons
        Inputs
            Distance From In All Four(Front, FrontLeft, FrontRight, Back) Direction
        Outputs
            Acceleration And Steering Angle 
        Activation Funtion: TanH(x)
    */
    public float[,] w1, w2;                                                                 //Weights For Each Layer
    public float[] inputLayer, hiddenLayer, outputLayer;
    public float b1, b2;                                                                    //Baise For Each Layer

    public NeuralNetwork()
    {
        b1 = Random.Range(-1.000f, 1.000f);                                                 //Initilizing Random Baises
        b2 = Random.Range(-1.000f, 1.000f);
        w1 = new float[4, 4];
        w2 = new float[2, 4];


        for (int i = 0; i < 4; i++)                                                         //Making W1 With Random Values
        {
            for (int j = 0; j < 4; j++)
            {
                w1[i, j] = Random.Range(-1.000f, 1.000f);
            }
        }

        for (int i = 0; i < 2; i++)                                                         //Making W2 With Random Values
        {
            for (int j = 0; j < 4; j++)
            {
                w2[i, j] = Random.Range(-1.000f, 1.000f);
            }
        }

    }

    float tanH(float x)                                                                     //Activation Fuction
    {
        float y;
        y = (2 / (1 + Mathf.Exp(-2 * x))) - 1;
        return y;
    }

    public float[,] metrixMultiplication(float[,] m1, float[,] m2, float baise)             //Self-Explanatory
    {
        int w1, w2, h1, h2;
        h1 = m1.GetLength(0);
        w1 = m1.GetLength(1);
        h2 = m2.GetLength(0);
        w2 = m2.GetLength(1);
        float[,] result;
        result = new float[h1, w2];

        for (int i = 0; i < h1; i++)
        {
            for (int j = 0; j < w2; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < h2; k++)
                {
                    result[i, j] += m1[i, k] * m2[k, j];
                }
                result[i, j] = tanH(result[i, j] + baise);
            }
        }
        return result;
    }

    public float[,] feedForward(float[,] inputs)                                            //Feed Forward For Neural Network
    {   
        float[,] result;
        result = metrixMultiplication(w1, inputs, b1);
        result = metrixMultiplication(w2, result, b2);
        return result;
    }

    public void mutate()                                                                    //Mutation Function With 10% Mutation Rate
    {
        for (int i = 0; i < 4; i++)                                                         //Mutating W1
        {
            for (int j = 0; j < 4; j++)
            {
                if (Random.Range(0, 100) <=10)
                {
                    w1[i, j] = w1[i, j] + Random.Range(-0.1000f, 0.1000f);
                }
            }
        }

        for (int i = 0; i < 2; i++)                                                         //Mutating W2
        {
            for (int j = 0; j < 4; j++)
            {
                if (Random.Range(0, 100) <= 10)
                {
                    w2[i, j] = w2[i, j] + Random.Range(-0.1000f, 0.1000f);
                }
            }
        }

        if (Random.Range(0, 100) <= 10)                                                         //Mutating Baises
        {
            b1 = b1 + Random.Range(-0.0100f, 0.0100f);
            b2 = b2 + Random.Range(-0.0100f, 0.0100f);
        }
    }


    public void inherit(NeuralNetwork p1, NeuralNetwork p2)
    {
        //Inheriting W1
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (Random.Range(0, 100) <= 50)
                {
                    w1[i, j] = p1.w1[i, j];
                }
                else
                {
                    w1[i, j] = p2.w1[i, j];
                }
            }
        }

        //Inheriting W2
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (Random.Range(0, 100) <= 50)
                {
                    w2[i, j] = p1.w2[i, j];
                }
                else
                {
                    w2[i, j] = p2.w2[i, j];
                }
            }
        }

        //Inheriting Beises
        if (Random.Range(0, 100) < 50)
        {
            b1 = p1.b1;
            b2 = p1.b2;
        }
        else
        {
            b1 = p2.b1;
            b2 = p2.b2;
        }
    }
    public void save(float fitness)                                                         //Saving Weights
    {
        try
        {
            string fitnessInString = System.IO.File.ReadAllText(@"Trained Data\fitness.txt");
            Debug.Log(fitnessInString);
            float fitnessInFloat = float.Parse(fitnessInString);
            if (fitnessInFloat < fitness)
            {

                using (System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"Trained Data\W1.txt"))
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            file1.Write(w1[i, j].ToString());
                            file1.Write("\t");
                        }
                        file1.Write("\n");
                    }

                using (System.IO.StreamWriter file2 = new System.IO.StreamWriter(@"Trained Data\W2.txt"))
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            file2.Write(w2[i, j].ToString());
                            file2.Write("\t");

                        }
                        file2.Write("\n");
                    }

                using (System.IO.StreamWriter file3 = new System.IO.StreamWriter(@"Trained Data\baises.txt"))
                {
                    file3.Write(b1);
                    file3.Write("\n");
                    file3.Write(b2);
                }
                using (System.IO.StreamWriter file4 = new System.IO.StreamWriter(@"Trained Data\fitness.txt"))
                {
                    file4.Write(fitness);
                }

                Debug.Log("Weights Saved");


            }


        }
        catch (DirectoryNotFoundException)
        {   
            System.IO.DirectoryInfo Di=Directory.CreateDirectory("Trained Data");
            Debug.Log("In Catch");
            using (System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"Trained Data\W1.txt"))
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        file1.Write(w1[i, j].ToString());
                        file1.Write("\t");
                    }
                    file1.Write("\n");
                }

            using (System.IO.StreamWriter file2 = new System.IO.StreamWriter(@"Trained Data\W2.txt"))
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        file2.Write(w2[i, j].ToString());
                        file2.Write("\t");

                    }
                    file2.Write("\n");
                }

            using (System.IO.StreamWriter file3 = new System.IO.StreamWriter(@"Trained Data\baises.txt"))
            {
                file3.Write(b1);
                file3.Write("\n");
                file3.Write(b2);
            }
            using (System.IO.StreamWriter file4 = new System.IO.StreamWriter(@"Trained Data\fitness.txt"))
            {
                file4.Write(fitness);
            }

            Debug.Log("Weights Saved");
        }
  
    }

    public void load(){                                                                     //Loading Saved Natwork
        string we1 = System.IO.File.ReadAllText(@"Trained Data\W1.txt");
        string we2 = System.IO.File.ReadAllText(@"Trained Data\W2.txt");
        string b = System.IO.File.ReadAllText(@"Trained Data\baises.txt");
        string[] w1Array=we1.Split('\n');
        for (int i = 0; i < 4; i++){
        string[] values=w1Array[i].Split('\t');
                {
                    for (int j = 0; j < 4; j++)
                    {
                        w1[i,j]=float.Parse(values[j]);
                    }
                }
        }
        string[] w2Array=we2.Split('\n');
        for (int i = 0; i < 2; i++){
        string[] values=w2Array[i].Split('\t');
                {
                    for (int j = 0; j < 4; j++)
                    {
                        w2[i,j]=float.Parse(values[j]);
                    }
                }
        }

        string[] baiseArray=b.Split('\n');
        b1=float.Parse(baiseArray[0]);
        b2=float.Parse(baiseArray[1]);


        Debug.Log("Brain Loaded");
    }

}
