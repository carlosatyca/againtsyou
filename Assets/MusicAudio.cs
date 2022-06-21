using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudio : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
        DontDestroyOnLoad(audio);
    }
}
