using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button : MonoBehaviour
{
   public void OnPlay()
   {
        SceneManager.LoadScene("Game");
   }
}
