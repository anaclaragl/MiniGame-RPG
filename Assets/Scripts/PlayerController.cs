using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<Notes> playerSequence = new List<Notes>();
    public List<Notes> systemSequence = new List<Notes>();
    public MusicPuzzle musicPuzzle;

    PlayerInput playerInput;

    public bool canTalk = false;
    public bool canGate = false;
    public bool canMusic = false;

    public Notes _re, _la, _sol, _mi;

    public DialogueManager dialogueManager;

    public Gate gate;

    public bool dialogueStart = false;


    void Awake(){
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Dialogue.started += Dialogue;

        playerInput.PlayerControls.Re.started += Re;
        playerInput.PlayerControls.La.started += La;
        playerInput.PlayerControls.Sol.started += Sol;
        playerInput.PlayerControls.Mi.started += Mi;
        playerInput.PlayerControls.Repeat.started += RepeatMusicSequence;
    }

    public void Dialogue(InputAction.CallbackContext context)
    {
        if(canTalk){
            if(context.started){
                if(dialogueManager.sentences.Count == 3){
                    dialogueManager.StartDialogue();
                }else{
                    dialogueManager.DisplayNextSentence();  
                }
            }
        }
        if(canGate){
            if(context.started){
                if (gate.checkKeysRequired()){
                    gate.OpenDoor();
                }else{
                    gate.ShowLockedMessage();
                }
            }
        }
    }

    void OnEnable()
    {
        playerInput.PlayerControls.Enable();
   
    }

    void OnDisable()
    {
        playerInput.PlayerControls.Disable();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Genio")){
            canTalk = true;
        }
        if (other.CompareTag("Portao")){
            canGate = true;
        }
        if (other.CompareTag("Music")){
            canMusic = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Genio")){
            canTalk = false;
            dialogueManager.sentences.Clear();
            dialogueManager.EndDialogue();
        }
        if (other.CompareTag("Portao")){
            canGate = false;
        }
        if (other.CompareTag("Music")){
            canMusic = false;
        }
    }

    public void Re(InputAction.CallbackContext context){
        if(canMusic){
            if(context.started){
                Notes note = _re.GetComponent<Notes>();
                note.PlayNote();
                playerSequence.Add(note);
                CheckPlayerSequence();
            }
        }
    }

    public void La(InputAction.CallbackContext context){
        if(canMusic){
            if(context.started){
                Notes note = _la.GetComponent<Notes>();
                note.PlayNote();
                playerSequence.Add(note);
                CheckPlayerSequence();
            }
        }
    }

    public void Sol(InputAction.CallbackContext context){
        if(canMusic){
            if(context.started){
                Notes note = _sol.GetComponent<Notes>();
                note.PlayNote();
                playerSequence.Add(note);
                CheckPlayerSequence();
            }
        }
    }

    public void Mi(InputAction.CallbackContext context){
        if(canMusic){
            if(context.started){
                Notes note = _mi.GetComponent<Notes>();
                note.PlayNote();
                playerSequence.Add(note);
                CheckPlayerSequence();
            }
        }
    }

    public void RepeatMusicSequence(InputAction.CallbackContext context){
        if(context.started){
            musicPuzzle.RepeatSequence();
        }
    }

    /*void InteractWithNote(){
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
    }*/

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
