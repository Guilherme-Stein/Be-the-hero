using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
	[Range(5, 100)]
	[Tooltip("Após quanto tempo a bala deve ser destruida?")]
	public float destroyAfter;
	[Tooltip("Se enabled a bala se destroi com o impacto")]
	public bool destroyOnImpact = false;
	[Tooltip("Minimum time after impact that the bullet is destroyed")]
	public float minDestroyTime;
	[Tooltip("Maximum time after impact that the bullet is destroyed")]
	public float maxDestroyTime;
	[Tooltip("Velocidade do projétil")]
	public float velBala;

	[Header("Prefab de Efeitos de Impacto da bala")]
	public Transform [] impactoPrefabs;

	private void Start (){
		//Inicia o timer para a bala ser destruida caso não haja ação nenhuma
		StartCoroutine (DestroyAfter());
	}

	private void FixedUpdate() {
		transform.Translate(0.0f, 0.0f, velBala * Time.deltaTime);
	}

	//If the bullet collides with anything
	private void OnCollisionEnter (Collision collision){
		//Se destroyonimpact for false, inicia 
		//a corotina com um tempo aletorio de destroy
		if (!destroyOnImpact) {
			StartCoroutine (DestroyTimer ());
		}
		//Se não, destroi a bala
		else {
			Destroy (gameObject);
		}

		// If bullet collides with "Metal" tag
		// if (collision.transform.tag == "Enemy") 
		// {
		// 	//Instantiate random impact prefab from array
		// 	Instantiate (impactoPrefabs [Random.Range 
		// 		(0, impactoPrefabs.Length)], transform.position, 
		// 		Quaternion.LookRotation (collision.contacts [0].normal));
		// 	Destroy bullet object
		// 	Destroy(gameObject);
		// }

		

		// //Se a bala collider com uma determinada tag instancia um efeito onde houver a colisao
		// if (collision.transform.tag == "ExplosiveBarrel") 
		// {
		// 	//Toggle "explode" on explosive barrel object
		// 	collision.transform.gameObject.GetComponent
		// 		<ExplosiveBarrelScript>().explode = true;
		// 	//Destroy bullet object
		// 	Destroy(gameObject);
		// }
	}

	private void OnTriggerEnter(Collider collider) {
		//If bullet collides with "Target" tag
		if (collider.transform.CompareTag("Enemy")) {
			//Destroi o objeto
			Destroy(collider.gameObject);
			//Destroi a bala
			Destroy(gameObject);
		}
	}

	private IEnumerator DestroyTimer () {
		//espera um tempo aleatorio entre o tempo minimo e o tempo maximo
		yield return new WaitForSeconds
			(Random.Range(minDestroyTime, maxDestroyTime));
		//Destroi a bala
		Destroy(gameObject);
	}

	private IEnumerator DestroyAfter () {
		yield return new WaitForSeconds (destroyAfter);
		//Destroi a bala
		Destroy (gameObject);
	}
}

