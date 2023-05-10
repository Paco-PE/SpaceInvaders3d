using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMuerte : MonoBehaviour
{
    public GameObject particulasMuerte;
    private AudioManager audioManager;
    private int puntuacion;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnDestroy()
    {
        if (this.gameObject.scene.isLoaded)
        {
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

            if (puntuacion >= 180)
            {
                SceneManager.LoadScene("Nivel2");
            }
        }
    }
}
