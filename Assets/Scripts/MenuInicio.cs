using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicio : MonoBehaviour
{
    public void Jugar(){
        AsyncOperation operacionCarga = SceneManager.LoadSceneAsync(1);
        //StartCoroutine(Carga());
    }

    public void Salir(){
        Debug.Log("Salir...");
        Application.Quit();
    }

    /*private IEnumerator Carga(){
        AsyncOperation operacionCarga = SceneManager.LoadSceneAsync(1);
    }*/
}
