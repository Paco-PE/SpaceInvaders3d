using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaFondo : MonoBehaviour
{
    public static MusicaFondo _instancia;
    
    // Start is called before the first frame update
    void Awake()
    {
        if(MusicaFondo._instancia == null){
            MusicaFondo._instancia = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
}