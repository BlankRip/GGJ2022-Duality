using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            TutorialTip();
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }    
    }

    private void TutorialTip()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        tutorialPanel.SetActive(true);
    }
}
