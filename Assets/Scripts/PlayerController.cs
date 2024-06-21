using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<Notes> playerSequence = new List<Notes>();
    public List<Notes> systemSequence = new List<Notes>();
    public MusicPuzzle musicPuzzle;

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            InteractWithNote();
        }
    }

    void InteractWithNote(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)){
            Notes note = hit.collider.GetComponent<Notes>();
            if (note != null){
                note.PlayNote();
                playerSequence.Add(note);
                CheckPlayerSequence();
            }
        }

        //checkKeys

        if (hit.collider.gameObject == musicPuzzle.repeatButton){
            musicPuzzle.RepeatSequence();
        }
    }

    public void SetSystemSequence(List<Notes> sequence){
        systemSequence = sequence;
        playerSequence.Clear();
    }

    private void CheckPlayerSequence(){
        if (playerSequence.Count <= systemSequence.Count){
            for (int i = 0; i < playerSequence.Count; i++){
                if (playerSequence[i] != systemSequence[i]){
                    Debug.Log("errado");
                    //tocar som de errado
                    playerSequence.Clear();
                    return;
                }
            }

            if (playerSequence.Count == systemSequence.Count){
                Debug.Log("correta");
                //ativa o item
                musicPuzzle.flauta.SetActive(true);
            }
        }
    }
}
