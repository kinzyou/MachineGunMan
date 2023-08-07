using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGFall : MonoBehaviour
{
    public AudioClip Sound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            audioSource.volume = 0.4f;
            audioSource.PlayOneShot(Sound);
        }
    }
}
