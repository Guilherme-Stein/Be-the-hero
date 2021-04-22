using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrosPos : MonoBehaviour
{
    //Array com todos os carros
    public GameObject[] carros;
    //Array com tadas as posições que querem ser usadas para o spawn de carros
    public GameObject[] spawnPos;

    public int qtdCarros;

    public static bool spawnarCarros; 



    // Start is called before the first frame update
    void Start(){
        spawnarCarros = true;

        //Spawna um dos carros do array em uma das posições de spawn do array
            Instantiate(carros[Random.Range(0, 2)].gameObject, 
                spawnPos[Random.Range(0, 3)].transform.position, 
                carros[Random.Range(0, 2)].transform.rotation);

        StartCoroutine(carSpawn(GerenciadorDificuldade.TempoCarros));

    }



    public IEnumerator carSpawn(float esperar){
            yield return new WaitForSeconds(esperar);
            if(spawnarCarros){
                Instantiate(carros[Random.Range(0, 2)].gameObject, 
                    spawnPos[Random.Range(0, 3)].transform.position, 
                    carros[Random.Range(0, 2)].transform.rotation);
            }
            yield return StartCoroutine(carSpawn(GerenciadorDificuldade.TempoCarros));
        
    }
}
