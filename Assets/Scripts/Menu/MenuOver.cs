using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MenuOver : MonoBehaviour
{
    private PlayerController playerController;
    private int seleccionActual = 0;
    private int totalBotones = 2;
    private float tiempoUltimaPulsacion;
    private float intervaloMinimoPulsacion = 0.2f;
    private Button[] botones;
    void Start(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        botones = transform.Find("MenuPrincipal").GetComponentsInChildren<Button>();

        ActualizarSeleccion(seleccionActual);
    }

    void Update(){
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0){
            botones[seleccionActual].OnDeselect(null);
        }
    }
    public void Inicio(){
        AsyncOperation operacionCarga = SceneManager.LoadSceneAsync("Intro");
    }

    public void Salir(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OnNavigateUp(InputAction.CallbackContext context)
    {
        float tiempoActual = Time.time;
        if(tiempoActual - tiempoUltimaPulsacion >= intervaloMinimoPulsacion && seleccionActual > 0){
            ActualizarSeleccion(seleccionActual-1);
            tiempoUltimaPulsacion = tiempoActual;
            Debug.Log("Arriba");
        }   
    }

    public void OnNavigateDown(InputAction.CallbackContext context)
    {
        float tiempoActual = Time.time;
        if(tiempoActual - tiempoUltimaPulsacion >= intervaloMinimoPulsacion && seleccionActual < totalBotones)
        {
            ActualizarSeleccion(seleccionActual+1);
            tiempoUltimaPulsacion = tiempoActual;
            Debug.Log("Abajo");
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        float tiempoActual = Time.time;
        if(tiempoActual - tiempoUltimaPulsacion >= intervaloMinimoPulsacion && seleccionActual < totalBotones)
        {
            SeleccionarOpcion(seleccionActual);
            tiempoUltimaPulsacion = tiempoActual;
        }
    }

    private void ActualizarSeleccion(int nuevaSeleccion)
    {
        // Limitar la selección dentro del rango válido de botones
        nuevaSeleccion = Mathf.Clamp(nuevaSeleccion, 0, totalBotones - 1);

        // Deseleccionar el botón anterior
        botones[seleccionActual].OnDeselect(null);

        // Seleccionar el nuevo botón
        seleccionActual = nuevaSeleccion;
        botones[seleccionActual].OnSelect(null);
    }

    private void SeleccionarOpcion(int opcionSeleccionada)
    {
        // Realizar la acción correspondiente a la opción seleccionada
        switch (opcionSeleccionada)
        {
            case 0:
                Inicio();
                break;
            case 1:
                Salir();
                break;
            default:
                break;
        }
    }
}
