using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public GameObject[] Predios;
    public GameObject[] spawnNext;
    Vector3 teste;
    GameObject atualPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
        
            var pre1 = Instantiate(Predios[0], 
            Predios[0].transform.position, 
            Predios[0].transform.rotation);
          
        for (int i = 0; i < 3; i++)
        {
            atualPrefab = Predios[0];
            
            teste = atualPrefab.transform.GetChild(0).position;

            var pre2 = Instantiate(Predios[0], 
            teste, 
            Predios[0].transform.rotation);
        }

    }

}
