using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class mapGeneration : MonoBehaviour
{
    //script arma
    public GameObject Arma;

    public GameObject prediosCaindoParent;
    public GameObject InimigosParent;
    public static bool _Troca;

    private int randomPos;
    private int posAnterior = 0;

    [Header("Jogador")]
    public GameObject Heroes;
    public GameObject playerCorrendo;
    public GameObject playerCaindo;
    public GameObject faseCorrendo;
    public static bool jogandoCorrendo;
    public static bool jogandoCaindo;

    public GameObject Point_A;
    public static Vector3 pontoDeLargada;


    [Header("Pontuação e Moedas")]
    public float score;
    public Text scoreTxt;
    public static float moedas;
    public Text moedasTxt;
    public GameObject Hud;

    public Text contagem;
     //Game Over
    public static bool gameOver;
    public Text gameOverTxt;

    [Header("Prédios")]
    public GameObject[] Predios; //Todos prédios usados na cena
    private float tamanhoPredio = 18.3f;
    private float tamanhoPredioCaindo = -7.0f;
    private float spawnZ = 0.0f;
    private float spawnCaindo = 0.0f;
    private float predioProcPos = 0.0f;
    private int lastPrefabIndex = 0;
    public static int contadorSpawn = 0;

    private bool primeirosPredios;

 
    public GameObject predioCaindo; //primeiro prédio para instanceamento dos próximos

    public static Vector3 positionPredio;

   

    public float speed;

    // public static Transform playerRo;
    public static Transform playerPos;

    public static float tempoDeJogo;
    public static float tempo;

    [Header("Enemy Spawn")]
    public GameObject[] enemyPos;
    public GameObject[] enemies;

    private void Awake() {


        Time.timeScale = 0;

        _Troca = false;
        tempo = 0;

        prediosProc.spawnNow = false;
        prediosProc.spawnFall = false;
    }

    void Start(){
        StartCoroutine(ResumeGame());

        playerCorrendo.SetActive(true);
        playerCaindo.SetActive(false);

        //da play nos 4 sons que compoem a trilha de fundo
        FindObjectOfType<SoundManager>().Play("Fundo01");
        FindObjectOfType<SoundManager>().Play("Fundo02");
        FindObjectOfType<SoundManager>().Play("Fundo03");
        FindObjectOfType<SoundManager>().Play("Fundo04");

        //gameover desabilitados até o player tingir 0 de vida
        gameOver = false;
        gameOverTxt.enabled = false;

        moedas = 0;
        //Começa o jogo com a mecãnica de corrida ativa e a de caida desativada
        jogandoCorrendo = true;
        jogandoCaindo = false;

        // // //posiciona o player no começo da fase
        // Heroes.transform.position = point_A01.position;

        pontoDeLargada = Point_A.transform.position;
       
        // //Collider de troca de mecanica começa desativado
        // PontoB.SetActive(false);
        spawnCaindo = tamanhoPredioCaindo;

        primeirosPredios = true;
    }

    void Update(){

        // if (primeirosPredios){
        //     //Estancia qtd determinada de predios
        //     for (int i = 0; i <= GerenciadorDificuldade.QuantidadePrediosRun; i++)
        //     {
        //         if(i < 2){
        //             SpawnPredio(0);
        //         }else{
        //             SpawnPredio(0);
        //         }
        //     }
        //     SpawnPredioCont();
        //     primeirosPredios = false;
        // }

        tempoDeJogo += Time.deltaTime;
        if(jogandoCorrendo){
            tempo += Time.deltaTime;
        }
        moedasTxt.text = moedas.ToString("##,##");
        scoreTxt.text = score.ToString("##,##");
        score += Time.deltaTime * speed;

        //Após o contador chegar a 6 a troca para a mecanica de caida acontece
        if(_Troca){    
            if (jogandoCaindo){
                foreach (Transform child in InimigosParent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                tempo = 0;
                // playerCorrendo.SetActive(false);
                playerCaindo.SetActive(true); 
                //condição apara a criação de prédios na mecanica de cair
                    if (PlayerMovement.spawnRisingFall){
                        for (int i = 0; i <= GerenciadorDificuldade.QuantidadePrediosFall; i++){
                            if(i < GerenciadorDificuldade.QuantidadePrediosFall){
                                SpawnPredioFall(Random.Range(4,7));
                            }else{
                                SpawnPredioFall(7);
                            }
                        }                
                        PlayerMovement.spawnRisingFall = false;
                    }
                
            }else if (jogandoCorrendo){
                foreach (Transform child in prediosCaindoParent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
                spawnCaindo = tamanhoPredioCaindo;
                playerCorrendo.SetActive(true);
                playerCaindo.SetActive(false); 
                Arma.GetComponent<Arma>().podeAtirar = true;
                //Estancia qtd determinada de predios
                for (int i = 0; i <= GerenciadorDificuldade.QuantidadePrediosRun; i++)
                {
                    if(i < 2){
                        SpawnPredio(0);
                    }else{
                        SpawnPredio(0);
                    }
                }
            }
            _Troca = false;



            // distanceTravelled += speed * Time.deltaTime;
            // playerPos.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            // playerPos.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        }

        
        
       //condição para método de criação de prédios na cena 
       if (prediosProc.spawnNow){
           SpawnPredioCont();
           if (Random.Range(1,3) == 1){
                spawnEnemy();
           }
           prediosProc.spawnNow = false;
        }

        //condição para criação do predioAngular 
       if (prediosProc.spawnFall){
            SpawnPredioCont(3);
            prediosProc._Stop = true;
            prediosProc.spawnFall = false;
            spawnZ = 0;
        }

        //Condição que quando gameOver for true
        if(gameOver){
            Time.timeScale = 0;
            StartCoroutine(ResetGame(2.0f));
            gameOverTxt.enabled = true;
            prediosProc._Stop = false;


        }
    }


    private void SpawnPredio(int prefabIndex = -1){
        GameObject go ;
        if(prefabIndex == -1)
            go = Instantiate(Predios[RandomPrefabIndex()]);
        else
            go = Instantiate(Predios[prefabIndex]);

            go.transform.SetParent (transform);
            go.transform.position = new Vector3(-0.0f, 0.0f, go.transform.position.z + spawnZ);
            spawnZ += tamanhoPredio;
            predioProcPos = spawnZ;
            print("proxima posicao do predio: " + predioProcPos);
    }

    private void SpawnPredioFall(int prefabIndex){
        var fall = Instantiate(Predios[prefabIndex], 
        new Vector3(predioCaindo.transform.position.x, 
        predioCaindo.transform.position.y + spawnCaindo, 
        predioCaindo.transform.position.z), 
        predioCaindo.transform.rotation);
        fall.transform.SetParent(prediosCaindoParent.transform);
        spawnCaindo += tamanhoPredioCaindo;
    }

    private void SpawnPredioCont(int prefabIndex = -1){
        GameObject go ;
        if(prefabIndex == -1)
            go = Instantiate(Predios[RandomPrefabIndex()]);
        else
            go = Instantiate(Predios[prefabIndex]);
            go.transform.SetParent (transform);
            go.transform.position = new Vector3(0.0f, 0.0f, go.transform.position.z + predioProcPos);
            print("proxima posicao do predio: " + predioProcPos);


    }

    private int RandomPrefabIndex(){
        // if(Predios.Length <= 1){
        //     return 0;
        // }
        int randomIndex = lastPrefabIndex;
        randomIndex = Random.Range(0,3);
        lastPrefabIndex = randomIndex;
        return(randomIndex);
    }


    IEnumerator ResetGame(float espere){
        yield return new WaitForSecondsRealtime(espere);
        SceneManager.LoadScene("Ad");
    }

    public void spawnEnemy(){
        for (int i = 0; i < GerenciadorDificuldade.QuantidadeInimigos; i++){
            GameObject enemyGo;
            enemyGo = Instantiate(enemies[Random.Range(0, 3)]);
            enemyGo.transform.SetParent(InimigosParent.transform);
            enemyGo.transform.position = enemyPos[posEnemy()].transform.position;
              

        }
    }

    private int posEnemy(){
        randomPos = Random.Range(0, enemyPos.Length - 1);
        if (randomPos != posAnterior){
                return randomPos;
                posAnterior = randomPos;
            }else{
                return 0;
            }

    }

    public void UpdateScore (int score){
      scoreTxt.text = "Score: " + score;
    }

    private IEnumerator ResumeGame(){
        yield return new WaitForSecondsRealtime(1.0f);
        contagem.text = 3.ToString();
        yield return new WaitForSecondsRealtime(1.0f);
        contagem.text = 2.ToString();
        yield return new WaitForSecondsRealtime(1.0f);
        contagem.text = 1.ToString();
        yield return new WaitForSecondsRealtime(1.0f);
        contagem.text = "Start";
        yield return new WaitForSecondsRealtime(1.0f);
        contagem.enabled = false;
        Time.timeScale = 1;
            for (int i = 0; i <= GerenciadorDificuldade.QuantidadePrediosRun; i++)
            {
                if(i < 2){
                    SpawnPredio(0);
                }else{
                    SpawnPredio(0);
                }
            }
    }

  
}
