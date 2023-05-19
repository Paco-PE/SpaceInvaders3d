using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMuerte : MonoBehaviour
{
    public GameObject particulasMuerte;
    private ScoreManager scoreManager;

    private AudioManager audioManager;
    public int puntuacion;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void OnDestroy()
    {
        if (this.gameObject.scene.isLoaded)
        {
            scoreManager.AddScore(10);
            Vector3 posactual = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(particulasMuerte, posactual, particulasMuerte.transform.rotation);
            audioManager.ReproducirSonido("invaderkilled");

            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemigos.Length == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
