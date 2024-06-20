using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPuzzle : MonoBehaviour
{
    public List<Notes> notes;
    public PlayerController playerController;
    public GameObject repeatButton, flauta;

    void Start(){ //chamar quando apertar algum botao ou chegar perto
        GenerateSequence();
    }

    private void GenerateSequence(){
        List<Notes> sequence = new List<Notes>();
        for (int i = 0; i < 6; i++){ //6 notas a serem tocadas
            sequence.Add(notes[Random.Range(0, notes.Count)]);
        }
        playerController.SetSystemSequence(sequence);
    }

    private IEnumerator PlaySequence(){
        foreach (Notes note in playerController.systemSequence)
        {
            note.PlayNote();
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void RepeatSequence(){
        StartCoroutine(PlaySequence());
    }
}
