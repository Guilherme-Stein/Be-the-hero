using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prediosProc : MonoBehaviour
{
    public float limite;
    float tempo;
    public static bool spawnNow;
    public static bool spawnFall;
    public static bool _Stop;


    private void Update() {
        
        transform.Translate(0.0f, 
        0.0f, 
        -1.0f * Time.deltaTime * GerenciadorDificuldade.VelocidadePrediosRun);

        if(transform.position.z < limite){

            if(mapGeneration.tempo < GerenciadorDificuldade.TempoTroca && _Stop == false){
                spawnNow = true;
                Destroy(gameObject);
            }else if(mapGeneration.tempo >= GerenciadorDificuldade.TempoTroca && _Stop == false){
                spawnFall = true;
                Destroy(gameObject);
            }else{
                Destroy(gameObject);
            }
            
        }
    }
}
