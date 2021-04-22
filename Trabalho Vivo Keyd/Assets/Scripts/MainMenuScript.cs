using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    // public Image Credits;
    // public Image Options;

    void Start()
    {
        FindObjectOfType<SoundManager>().Play("mainmenu01");
        FindObjectOfType<SoundManager>().Play("mainmenu02");
        FindObjectOfType<SoundManager>().Play("mainmenu03");

    }    

    public void CarregaCena(string cena){
        SceneManager.LoadScene(cena);

    }

    public void Aparece(GameObject panel1){
        panel1.gameObject.SetActive(true);
    }
    public void Desaparece(GameObject panel2){
        panel2.gameObject.SetActive(false);
    }
}
