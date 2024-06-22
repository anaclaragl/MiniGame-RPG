using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button : MonoBehaviour
{
   public string Scene;
   public void OnClickButton()
   {
        SceneManager.LoadScene("Scene");
   }
}
