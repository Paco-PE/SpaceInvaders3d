using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicio : MonoBehaviour
{
    public void Jugar(){
        AsyncOperation operacionCarga = SceneManager.LoadSceneAsync("Carga1");
    }

    public void Supervivencia(){
        AsyncOperation operacionCarga = SceneManager.LoadSceneAsync("CargaSupervivencia");
    }

    public void Salir(){
        Debug.Log("Salir...");
        Application.Quit();
    }

    public void Volver(){
        AsyncOperation operacionCarga = SceneManager.LoadSceneAsync(0);
    }

}
