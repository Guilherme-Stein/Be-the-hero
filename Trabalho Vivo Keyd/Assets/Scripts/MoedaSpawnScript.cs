using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoedaSpawnScript : MonoBehaviour
{
    public GameObject moeda;

    void Start(){
            Instantiate(moeda.gameObject, 
            new Vector3(transform.position.x, 
            transform.position.y, 7.0f), 
            moeda.transform.rotation);

    }

}
