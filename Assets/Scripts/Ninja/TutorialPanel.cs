using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] Button closeButton;

    private void Start() 
    {
        closeButton.onClick.AddListener(TutorialClose);  
    }

   private void TutorialClose()
   {
       Time.timeScale = 1;
       Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;
       this.gameObject.SetActive(false);
   }
}
