using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSourcePrefab;

    private AudioSource audioSource;

    public void GetInstance()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Awake()
    {
        GetInstance();
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip, Vector3 position)
    {
        audioSource.transform.position = position;
        
        audioSource.PlayOneShot(clip);
    }
}
