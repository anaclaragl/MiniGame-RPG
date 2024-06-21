using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Item key1;
    public Item key2;
    public GameObject lockedMessage;

    public bool checkKeysRequired(){
        return InventoryManager.instance.HasRequiredKeys(key1, key2);
    }

    public void OpenDoor(){
        // Coloque aqui a lógica para abrir a porta
        Debug.Log("Porta Aberta!");
    }

    public void ShowLockedMessage(){
        // Mostre uma mensagem indicando que a porta está trancada
        if (lockedMessage != null){
            lockedMessage.SetActive(true);
            StartCoroutine(HideLockedMessage());
        }
        Debug.Log("A porta está trancada. Você precisa das duas chaves para abrir.");
    }

    IEnumerator HideLockedMessage(){
        yield return new WaitForSeconds(2);
        lockedMessage.SetActive(false);
    }
}
