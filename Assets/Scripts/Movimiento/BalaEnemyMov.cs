using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemyMov : MonoBehaviour
{
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve el proyectil hacia adelante en su propia dirección
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        // Comprueba si el objeto colisionado es un enemigo
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador Tocado");
        }
    }
}
