using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoveScript : MonoBehaviour
{ 

    // Update is called once per frame
    void Update()
    {
        // faz o carro andar pelo z
		transform.Translate(0.0f, 0.0f, GerenciadorDificuldade.VelocidadeCarros * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other) {
        //Quando colidir com o trigger do primeiro predio da caida o carro se destroi
        if (other.gameObject.CompareTag("CarroDestroy")){
            Destroy(gameObject);
        }
        
    }
    private void OnCollisionEnter(Collision col) {
        //Se destroi qundo colidido com o player
        if (col.gameObject.CompareTag("PlayerFall")){
            PlayerMovement.Damage = true;
            Destroy(gameObject);
        }
    }
}
