using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject Mira;    
	public float bulletForce = 100;
    public bool podeAtirar = true;

    Animator gunAnim;

    [System.Serializable]
	public class prefabs
	{  
		[Header("Prefabs")]
		public Transform bulletPrefab;
	}
	public prefabs Prefabs;    

    [System.Serializable]
	public class spawnpoints
	{  
		[Header("Spawnpoints")]
		//Bullet prefab spawn from this point
		public Transform bulletSpawnPoint;

	}
	public spawnpoints Spawnpoints;

    
    void Start(){
        //Pega o animator da arma
        gunAnim = GetComponent<Animator>();

        podeAtirar = true;
    }

    void Update(){
        //verifica se foi pressionado o botão esquedo do mouse
        if(Input.GetMouseButtonDown(0)){
            if (podeAtirar){

                //Corotina para condição de tiro
                StartCoroutine(atirar(0.47f));
                //Ativa a corotina de animação
                StartCoroutine (Atirando());

                //Som de tiro
                FindObjectOfType<SoundManager>().Play("Tiro");

                //Faz a arma rotacionar e apontar para onde na tela o jogador clicou
                Vector3 clickPosition = -Vector3.one;
                clickPosition = fpsCam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,30f));
                this.transform.LookAt(clickPosition);
                
                //Faz a bala aparecer na posição do bulletSpawnPoint
                var bullet = (Transform)Instantiate (
                    Prefabs.bulletPrefab,
                    Spawnpoints.bulletSpawnPoint.transform.position,
                    Spawnpoints.bulletSpawnPoint.transform.rotation);

                // //Adiciona velocidade a bala
                // bullet.GetComponent<Rigidbody>().velocity = 
                // bullet.transform.forward * bulletForce;
            }
        }

        //Desenha uma linha vermelha para saber onde a arma está apontando
        Debug.DrawRay(Mira.transform.position, Mira.transform.forward * 10, Color.red);	

    }

    //Controla a booleana podeAtirar
    private IEnumerator atirar(float t){
            podeAtirar = false;
            yield return new WaitForSeconds (t);
            podeAtirar = true;
    }

    //Corotina Atirando()
    private IEnumerator Atirando() {
        //Seta a variavel booleana do animator para true
        gunAnim.SetBool("Atirando", true);
		//Esperar o tempo
		yield return new WaitForSeconds (0.15f);
		//Trocando a animação da arma para IDLE
        gunAnim.SetBool("Atirando", false);
	}

    //Reseta a rotation da arma no Evento da animação
    public void resetRotate(){
        transform.rotation = Quaternion.identity;
    }

}
