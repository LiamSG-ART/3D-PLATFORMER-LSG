using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSCRIPT : MonoBehaviour
{
    public AudioClip soundEfect;
    public AudioSource test;
    public GameObject jack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlaySoundEffect();
            jack.SetActive(false);
        }
    }

    public void PlaySoundEffect()
    {
        test.PlayOneShot(soundEfect);
    }
}
