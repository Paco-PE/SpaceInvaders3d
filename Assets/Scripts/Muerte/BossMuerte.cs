using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMuerte : MonoBehaviour
{
    public GameObject particulasMuerte;
    private ScoreManager scoreManager;
    private AudioManager audioManager;
    private int puntuacion;
    private int disparosRecibidos = 0;
    public int vida = 10;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void RecibirDisparo()
    {
        disparosRecibidos++;

        if (disparosRecibidos == vida)
        {
            if(scoreManager != null) scoreManager.AddScore(10);
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

