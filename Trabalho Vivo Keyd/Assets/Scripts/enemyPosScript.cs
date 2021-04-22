using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPosScript : MonoBehaviour
{
    public GameObject[] enemyPos;
    public GameObject[] enemies;
    public int qtdEnemies;

    public void spawnEnemy(){
        for (int i = 0; i < qtdEnemies; i++){
            Instantiate(enemies[Random.Range(0, 3)], 
            enemyPos[Random.Range(0, 7)].transform.position, 
            enemies[Random.Range(0, 3)].transform.rotation);
        }
    }
}
