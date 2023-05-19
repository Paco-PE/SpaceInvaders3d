using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMov : MonoBehaviour
{
    public float speed = 3f;

    private GameObject target;

    public float distanciaUmbral = 5f;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener la dirección hacia el jugador
        Vector3 direction = (target.transform.position - transform.position).normalized;

        // Calculamos la distancia hasta el jugador
        float distance = Vector3.Distance(target.transform.position, transform.position);

        // Orientar el enemigo hacia el jugador
        // transform.LookAt(target.transform);

        if(distance >= distanciaUmbral){
            // Actualizar la posición del enemigo hacia el jugador
            transform.position += direction * speed * Time.deltaTime;
        }else{
            transform.position -= direction * 10 * Time.deltaTime;
        }

    }
}
