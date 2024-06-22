using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public AudioClip noteSound;
    private AudioSource audioSource;
    public Material playMaterial;
    public Material originalMaterial;
    private Renderer renderer;

    void Start(){
        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.material;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = noteSound;
    }

    public void PlayNote(){
        audioSource.Play();
        StartCoroutine("ActiveNote");
    }

    IEnumerator ActiveNote(){
        renderer.material = playMaterial;
        yield return new WaitForSeconds(0.5f);
        renderer.material = originalMaterial;
    }
}
