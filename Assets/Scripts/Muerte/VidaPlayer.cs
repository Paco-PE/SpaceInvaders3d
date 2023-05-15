using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class VidaPlayer : MonoBehaviour {
    public GameObject[] vidas; // un array con las imágenes de las vidas
    public int vidaMaxima = 3; // número máximo de vidas

    public GameObject particulasMuerte;

    private GameObject player;
    
    private AudioManager audioManager;

    private int vidaActual; // número actual de vidas restantes

    // Este método se llama al inicio del juego
    void Start() {
        vidaActual = vidaMaxima; // Inicializamos las vidas
        audioManager = FindObjectOfType<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Este método se llama cada vez que el jugador pierde una vida
    public void PierdeVida() {
        vidaActual--; // Reducimos la vida actual en 1

        // Si la vida actual es menor o igual que cero, se acaba el juego
        if (vidaActual <= 0) {
            AsyncOperation operacionCarga = SceneManager.LoadSceneAsync("SampleScene"); //Esto debería ser lo que pasa cuando nos quedamos sin vidas
        }
        else {
            // Si no, desactivamos el objeto de imagen correspondiente a la vida perdida
            vidas[vidaActual].SetActive(false);

            player.SetActive(false);

            Vector3 posactual = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(particulasMuerte, posactual, particulasMuerte.transform.rotation);
            audioManager.ReproducirSonido("explosion");

            Invoke("ReactivarJugador", 1f);
        }
    }

    public void ReactivarJugador()
    {
        // Activamos el objeto jugador
        player.SetActive(true);
    }
}
