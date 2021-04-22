using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyDiveScript : MonoBehaviour
{

    int n = 5;
    float soma;

    // Update is called once per frame
    void Update()
    {
        //Movimenta o personagem na mecanica de Cair
        for (int i = 0; i == (n - 1); i++)
        {
            print("passo:" + i);
            soma =  1 + i/ n - i;
            if(i == (n-1)){
                print("Resultado" + soma);
            }
        }

    }
}
