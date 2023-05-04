using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMuerte : MonoBehaviour
{
    public GameObject particulasMuerte;
    // Start is called before the first frame update
    void OnDestroy() {
            Vector3 posactual = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            // Comprobamos que la escena esté cargada para no instanciar particulas cuando se destruyen los objetos que quedaban en la escena al cerrarla
            if(this.gameObject.scene.isLoaded) Instantiate(particulasMuerte, posactual, particulasMuerte.transform.rotation);
    }
}
