using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLook : MonoBehaviour
{
    private GameObject target;
    void Start()
    {
        while(target == null){
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        transform.LookAt(target.transform, Vector3.up);
        transform.Rotate(90, 0, 0);
    }
}
