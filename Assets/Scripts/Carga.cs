using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Carga : MonoBehaviour
{
    public Slider Cargador;
    private int valor = 0;

    void Start()
    {
        StartCoroutine(CargarAsync());
    }

    IEnumerator CargarAsync()
    {
        while (valor < 100)
        {
            float Progreso = Mathf.Clamp01(valor / 100f);
            Cargador.value = Progreso;
            valor += 3;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f); // Esperar 2 segundos antes de cargar la siguiente escena

        AsyncOperation Operacion = SceneManager.LoadSceneAsync(2);
        yield return null;
    }
}





