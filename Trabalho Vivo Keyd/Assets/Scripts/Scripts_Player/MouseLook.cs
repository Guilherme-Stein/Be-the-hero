using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensibilidadeMouse = 100f;
    public Transform playerBody;
    private Vector3 Point = new Vector3 (0f,0f,0f);
    public Transform olhar;
    public float speed;
    public bool _hit = false;

    public Transform target;
    public float range = 15f;
    public string enemyTag = "Enemy";

    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);

    }

    void UpdateTarget(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortDistance){
                shortDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortDistance <= range){
            target = nearestEnemy.transform;

        }else{
            target = null;
        }

	}

    void Update()
    {
    //     float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.deltaTime;
    //     float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse * Time.deltaTime;

    //     rotacaox -= mouseY;
    //     rotacaox = Mathf.Clamp(rotacaox, -90f, 90f);

    //     transform.localRotation = Quaternion.Euler(rotacaox, 0f , 0f);
    //     playerBody.Rotate(Vector3.up * mouseX);
        // // playerBody.Rotate(Vector3.left * mouseY);

        if(target == null){
            Quaternion LookRotation = Quaternion.LookRotation(olhar.position - 
                             transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                             LookRotation, Time.deltaTime * speed);
            return;
        }

        Quaternion toRotation = Quaternion.LookRotation(target.position - 
                        transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, 
                            toRotation, Time.deltaTime * speed);


    }

    //Ao entrar em colisão com o inimigo
    //  void OnTriggerStay(Collider other) {
    //     if(other.gameObject.tag == "Inimigo"){
    //         // teste = other.gameObject.transform;
    //         Point = other.gameObject.transform.position;
    //         _hit = true;

    //     }
        
    //     if(other.gameObject.tag == "ResetLook"){
    //         _hit = false;

    //     }
        
        
    // }
    

    //Funções para olhar no objeto desejado
    // void olheDeVolta(){
    //     Quaternion toRotation = Quaternion.LookRotation(olhar.position - 
    //                         transform.position, Vector3.up);
    //     transform.rotation = Quaternion.Slerp(transform.rotation, 
    //                         toRotation, Time.deltaTime * speed);
    // }
    // void olheInimigo(){
    //     Quaternion toRotation = Quaternion.LookRotation(target.position - 
    //                     transform.position, Vector3.up);
    //     transform.rotation = Quaternion.Slerp(transform.rotation, 
    //                         toRotation, Time.deltaTime * speed);
    // }

    void OnDrawGizmosSelected() {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, range);
    }
    
}
