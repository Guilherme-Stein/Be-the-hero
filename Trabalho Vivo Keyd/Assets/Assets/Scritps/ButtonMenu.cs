using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Cena_Run");
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToAd()
    {
        SceneManager.LoadScene("Ad");
    }

    public void QuitGame()
    {
        Debug.Log("Fechando o jogo");
        Application.Quit();
    }
    

}
