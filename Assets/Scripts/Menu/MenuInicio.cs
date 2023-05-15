using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicio : MonoBehaviour
{
    public void Jugar(){
        AsyncOperation operacionCarga = SceneManager.LoadSceneAsync(1);
    }

    public void Salir(){
        Debug.Log("Salir...");
        Application.Quit();
    }

    public void Supervivencia(){
        Debug.Log("Supervivencia");
        //AsyncOperation operacionCarga = SceneManager.LoadSceneAsync(1);
    }

}
