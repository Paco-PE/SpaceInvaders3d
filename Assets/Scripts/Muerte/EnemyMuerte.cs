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
            if(scoreManager != null) scoreManager.AddScore(10);
            Vector3 posactual = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(particulasMuerte, posactual, particulasMuerte.transform.rotation);
            audioManager.ReproducirSonido("invaderkilled");

            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            if(boss != null){
                // Crear un nuevo vector con espacio para el objeto "Boss" y los enemigos existentes
                GameObject[] enemigosConBoss = new GameObject[enemigos.Length + 1];
                
                // Copiar los objetos "Enemy" al nuevo vector
                enemigos.CopyTo(enemigosConBoss, 0);
                
                // Añadir el objeto "Boss" al final del vector
                enemigosConBoss[enemigos.Length] = boss;
                
                // Reemplazar el vector de enemigos original por el nuevo vector que incluye al "Boss"
                enemigos = enemigosConBoss;
            }
            
            if (enemigos.Length == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
