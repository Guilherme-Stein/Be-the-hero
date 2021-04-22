using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MonoBehaviour
{


    void Update(){
        if(mapGeneration.jogandoCaindo){
            transform.Translate(Vector3.up 
            * -GerenciadorDificuldade.VelocidadeCaidaFall 
            * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider trigger) {
        // if(other.gameObject.CompareTag("troca")){
        //     transform.position = new Vector3(0.0f, transform.position.y, 5.0f);
        // }

        
    }

    
}
