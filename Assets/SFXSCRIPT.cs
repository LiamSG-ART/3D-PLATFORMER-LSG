using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSCRIPT : MonoBehaviour
{
    public AudioClip soundEfect;
    public AudioSource test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundEffect()
    {
        test.PlayOneShot(soundEfect);
    }
}
