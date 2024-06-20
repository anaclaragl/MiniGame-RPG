using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    //som
    public Material playMaterial;
    public Material originalMaterial;
    private Renderer renderer;

    void Start(){
        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.material;
    }

    public void PlayNote(){
        StartCoroutine("ActiveNote");
    }

    IEnumerator ActiveNote(){
        renderer.material = playMaterial;
        yield return new WaitForSeconds(0.5f);
        renderer.material = originalMaterial;
    }
}
