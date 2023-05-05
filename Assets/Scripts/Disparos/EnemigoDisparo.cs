using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoDisparo : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala que va a disparar
    public float bulletSpeed = 10f; // Velocidad de la bala
    public float minfireRate = 5f;
    public float maxfireRate = 10f;
    private float fireRate = 0f;

    private float nextFireTime; // Tiempo en el que podrá disparar la siguiente bala
    private Transform gunTransform;

    void Start()
    {
        gunTransform = transform.GetChild(0);
        nextFireTime = Random.Range(minfireRate, maxfireRate);
    }

    void Update()
    {
        // Comprueba si ha pasado suficiente tiempo para disparar la siguiente bala
        if (Time.time > nextFireTime)
        {
            // Instancia la bala y la dispara en la dirección del enemigo
            Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);

            fireRate = Time.time + Random.Range(minfireRate, maxfireRate);

            // Actualiza el tiempo en el que podrá disparar la siguiente bala
            nextFireTime = Time.time + fireRate;
        }
    }
}
