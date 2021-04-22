using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{ 
    public float alcance;
    public Vector3 playerPosition;
    
    void Start(){
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);

    }

    void Update(){
        if (playerPosition != new Vector3(0.0f, 0.0f, 0.0f))
        {
            transform.position = Vector3.MoveTowards(transform.position, 
            new Vector3(playerPosition.x, 12.0f, playerPosition.z), 
            Time.deltaTime * GerenciadorDificuldade.VelocidadeInimigo);
        }
            
    }

    void UpdateTarget(){
        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in Player)
        {
            playerPosition = player.transform.position;
        }

	}
    
    void OnDrawGizmosSelected() {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, alcance);
    }
}
