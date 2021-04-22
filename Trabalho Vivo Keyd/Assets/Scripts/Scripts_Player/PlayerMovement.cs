using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{

    public GameObject Arma;

    //Dano
    public GameObject telaVermelha;
    public float playerVida = 4;
    public static bool Damage;


    public Text VidaTxt;
    public static bool spawnRisingFall = false;
    
    private Collider playerCollider;
    private Animator anim;


    public GameObject mapGenerationScript;

    void Start(){
        playerCollider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        playerCollider.enabled = true;

        playerVida = 4.0f;
        VidaTxt.text = playerVida.ToString();
        telaVermelha.SetActive(false);

        mapGeneration.playerPos = gameObject.GetComponent<Transform>();
    }
    void Update(){
        VidaTxt.text = playerVida.ToString();

        if(playerVida <= 0){
            VidaTxt.enabled = false;
            mapGeneration.gameOver = true;
        }

        if (Damage){
            playerVida -= 1;
            StartCoroutine(Dano());
            Damage = false;
        }
    }

    private IEnumerator Dano(){
        telaVermelha.SetActive(true);
        yield return new WaitForSeconds (0.1f);
        telaVermelha.SetActive(false);

    }

    private IEnumerator trocaDeMec(){
        mapGeneration.jogandoCaindo = true;
        mapGeneration.jogandoCorrendo = false;
        anim.SetBool("pTroca", true);
        yield return new WaitForSeconds(1.00f);
        anim.SetBool("pTroca", false);
        
    }

    public void OnTriggerEnter(Collider trig){

        //Collider do predio inclinado para a troca de mecanica
        if(trig.gameObject.CompareTag("Fall")){
            Arma.GetComponent<Arma>().podeAtirar = false;
            StartCoroutine(trocaDeMec());
            GerenciadorDificuldade.TrocaFall = true;
            mapGeneration._Troca = true;
            spawnRisingFall = true;
        }

        if (trig.gameObject.CompareTag("troca")){
            mapGenerationScript.GetComponent<mapGeneration>().playerCorrendo.SetActive(false);
        }
        
        //usado na caida
        if(trig.gameObject.CompareTag("Run")){
            transform.position = mapGeneration.pontoDeLargada;
            GerenciadorDificuldade.TrocaRun = true;
            mapGeneration._Troca = true;
            prediosProc._Stop = false;
            mapGeneration.jogandoCaindo = false;
            mapGeneration.jogandoCorrendo = true;
            carrosPos.spawnarCarros = true;
        }

        if(trig.gameObject.CompareTag("Enemy")){
            FindObjectOfType<SoundManager>().Play("Dano");
            Damage = true;
            Destroy(trig.gameObject);
        }

        if (trig.gameObject.CompareTag("ParaCarro")){
            carrosPos.spawnarCarros = false;
        }
    }

    
    
 
   

        

}
