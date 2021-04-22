using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredioScript : MonoBehaviour
{
    public float limite;
    

    private void Update() {
        transform.Translate(0.0f, 
        0.0f, 
        -1.0f * Time.deltaTime * GerenciadorDificuldade.VelocidadePrediosRun);

        if(transform.position.z < limite){
            transform.position = mapGeneration.positionPredio;
        }
    }
}
