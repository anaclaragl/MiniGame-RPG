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

    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    public Gate gate;

    public bool dialogueStart = false;


    void Awake(){
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Dialogue.started += Dialogue;
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            InteractWithNote();
        }
    }

    public void Dialogue(InputAction.CallbackContext context)
    {
        if(canTalk){
            if(context.started){
                if(dialogueManager.sentences.Count == 3){
                    dialogueManager.StartDialogue(dialogue);
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
