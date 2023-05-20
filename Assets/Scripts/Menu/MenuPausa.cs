using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MenuPausa : MonoBehaviour
{
    private PlayerController playerController;
    private bool isPaused;
    public GameObject menuPausa;
    void Start(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuPausa.SetActive(false);
        isPaused = false;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("Pausado");
        TogglePause();
    }

    private void TogglePause(){
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        menuPausa.SetActive(isPaused);
    }
}
