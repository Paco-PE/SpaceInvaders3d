using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMuerte : MonoBehaviour
{
    public GameObject particulasMuerte;
    private AudioManager audioManager;
    private int puntuacion;
    private int disparosRecibidos = 0;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void RecibirDisparo()
    {
        disparosRecibidos++;

        if (disparosRecibidos == 5)
        {
            // Código para destruir el jefe
            GameObject puntuacionTextObj = GameObject.Find("ScorePuntuacion");
            Text puntuacionText = puntuacionTextObj.GetComponent<Text>();
            puntuacion = int.Parse(puntuacionText.text) + 10;
            if (puntuacionTextObj != null)
            {
                puntuacionText.text = puntuacion.ToString();
            }
            Vector3 posactual = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(particulasMuerte, posactual, particulasMuerte.transform.rotation);
            audioManager.ReproducirSonido("invaderkilled");

            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemigos.Length == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            Destroy(this.gameObject);
        }
    }
}

