using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnemyMuerte : MonoBehaviour
{
    public GameObject particulasMuerte;
    
    private AudioManager audioManager;

    private int puntuacion;

    void Start(){
        audioManager = FindObjectOfType<AudioManager>();
    }
    void OnDestroy() {
        // Comprobamos que la escena esté cargada para no instanciar particulas cuando se destruyen los objetos que quedaban en la escena al cerrarla
        if(this.gameObject.scene.isLoaded){
            GameObject puntuacionTextObj = GameObject.Find("ScorePuntuacion");
            Text puntuacionText = puntuacionTextObj.GetComponent<Text>();
            puntuacion = int.Parse(puntuacionText.text) + 10;
            if(puntuacionTextObj != null){
                puntuacionText.text = puntuacion.ToString();
            }
            Vector3 posactual = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(particulasMuerte, posactual, particulasMuerte.transform.rotation);
            audioManager.ReproducirSonido("invaderkilled");
        }
    }
}
