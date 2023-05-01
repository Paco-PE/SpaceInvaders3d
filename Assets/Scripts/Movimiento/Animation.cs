using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public Transform modelo1;
    public Transform modelo2;
    public float tiempoDeCambio = 0.6f;

    void Start()
    {
        StartCoroutine(Alternar());
    }

    IEnumerator Alternar()
    {
        while (true)
        {
            modelo1.gameObject.SetActive(true);
            modelo2.gameObject.SetActive(false);
            yield return new WaitForSeconds(tiempoDeCambio);
            modelo1.gameObject.SetActive(false);
            modelo2.gameObject.SetActive(true);
            yield return new WaitForSeconds(tiempoDeCambio);
        }
    }
}
