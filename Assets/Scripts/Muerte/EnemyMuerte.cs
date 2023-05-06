using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMuerte : MonoBehaviour
{
    public GameObject particulasMuerte;
    
    private AudioManager audioManager;

    void Start(){
        audioManager = FindObjectOfType<AudioManager>();
    }
    void OnDestroy() {
            // Comprobamos que la escena esté cargada para no instanciar particulas cuando se destruyen los objetos que quedaban en la escena al cerrarla
            if(this.gameObject.scene.isLoaded){
                Vector3 posactual = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                Instantiate(particulasMuerte, posactual, particulasMuerte.transform.rotation);
                audioManager.ReproducirSonido("invaderkilled");
            }
    }
}
