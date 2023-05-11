using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDisparo : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala que va a disparar
    public float bulletSpeed = 10f; // Velocidad de la bala
    public float minfireRate = 5f;
    public float maxfireRate = 10f;

    private float nextFireTime; // Tiempo en el que podrá disparar la siguiente bala
    private Transform gunTransform;

    void Start()
    {
        gunTransform = transform.GetChild(0);
        nextFireTime = Time.time + Random.Range(minfireRate, maxfireRate); // Agrega un pequeño retraso aleatorio en el tiempo de espera
    }

    void Update()
    {
        // Comprueba si ha pasado suficiente tiempo para disparar la siguiente bala
        if (Time.time > nextFireTime)
        {
            // Instancia la bala y la dispara en la dirección del enemigo
            Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);

            // Calcula el tiempo de espera para disparar la siguiente bala
            float t = Time.timeSinceLevelLoad / 10f; // Usa el tiempo de juego para aumentar la frecuencia de disparo a medida que pasa el tiempo
            float rate = Mathf.Lerp(maxfireRate, minfireRate, t); // Calcula la frecuencia de disparo utilizando una función matemática
            nextFireTime = Time.time + rate; // Actualiza el tiempo en el que podrá disparar la siguiente bala
        }
    }
}

