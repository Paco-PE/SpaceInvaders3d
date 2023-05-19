using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuOver : MonoBehaviour
{
    void Start(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Inicio(){
        AsyncOperation operacionCarga = SceneManager.LoadSceneAsync("Intro");
    }

    public void Salir(){
        Debug.Log("Salir...");
        Application.Quit();
    }

}
