using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PrediosCaindoScript : MonoBehaviour
{

    public float velocidade;
    public float limite;
    public float resetar;
    float cont;

    void Start(){
        cont = 0;
    }

    void Update(){
        //Faz os predios andarem em Y
        transform.Translate(0.0f, 1.0f * Time.deltaTime * velocidade, 0.0f );

        //faz os prédios resetarem a posição ao chegarem no Limite
        if(transform.position.z < limite){
            transform.position = new Vector3(transform.position.x, transform.position.y, resetar);
            cont++;
        }

        if(cont >= 3){
            SceneManager.LoadScene("Cena_Run");
        }
    }
}
