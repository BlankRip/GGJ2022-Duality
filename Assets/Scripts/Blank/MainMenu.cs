using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Blank {
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] GameObject mainMenuPanel, creditsPanel;
        [SerializeField] Button play, credits, quit, closeCredits;

        private void Start() {
            Time.timeScale = 1;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            play.onClick.AddListener(() => {SceneManager.LoadScene(1);});
            credits.onClick.AddListener(() => {mainMenuPanel.SetActive(false); creditsPanel.SetActive(true);});
            play.onClick.AddListener(() => {Application.Quit();});
            closeCredits.onClick.AddListener(() => {mainMenuPanel.SetActive(true); creditsPanel.SetActive(false);});
        }
    }
}