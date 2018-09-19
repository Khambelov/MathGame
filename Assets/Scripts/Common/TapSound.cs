using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapSound : MonoBehaviour
{

    public AudioSource tap;
    public AudioClip tapSound;

    private void Start()
    {
        tap.clip = tapSound;
        tap.playOnAwake = false;
    }

    public void Tap()
    {
        if (tap.isPlaying) tap.Stop();
        tap.PlayOneShot(tapSound);
    }
}
