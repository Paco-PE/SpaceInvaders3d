using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaMov : MonoBehaviour
{
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve el proyectil hacia adelante en su propia dirección
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("1. Enemigo tocado");
        // Comprueba si el objeto colisionado es un enemigo
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("2. Enemigo tocado");
            // Destruye el proyectil y el enemigo
            Destroy(gameObject);
            Destroy(collider.gameObject);
        }
    }


}
