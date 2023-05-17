using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sonido {
    public string nombre;
    public AudioClip clipDeAudio;
}

public class AudioManager : MonoBehaviour {
    public float volumen = 0.5f;
    // Lista de Sonidos para cargar los clips de audio en el Inspector de Unity
    public List<Sonido> sonidos;

    // Diccionario para almacenar los clips de audio
    private Dictionary<string, AudioClip> clipsDeAudio = new Dictionary<string, AudioClip>();

    // Crea un método para cargar los clips de audio en el diccionario
    void Awake() {
        foreach (Sonido sonido in sonidos) {
            clipsDeAudio.Add(sonido.nombre, sonido.clipDeAudio);
        }
    }

    // Crea un método para reproducir un clip de audio por su nombre
    public void ReproducirSonido(string nombreDelSonido) {
        if (clipsDeAudio.ContainsKey(nombreDelSonido)) {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = clipsDeAudio[nombreDelSonido];
            audioSource.volume = volumen;
            audioSource.Play();
        } else {
            Debug.LogWarning("El clip de audio '" + nombreDelSonido + "' no se encuentra en el diccionario de clips de audio.");
        }
    }
}