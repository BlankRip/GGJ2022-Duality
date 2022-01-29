using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Blank {
    public class Pause : MonoBehaviour
    {
        [SerializeField] GameObject pausePanel, warningPanel;
        [SerializeField] Button resume, restart, backToMenu, warningYes, warningNo;
        private bool paused;

        private void Start() {
            resume.onClick.AddListener(ResumeGame);
            restart.onClick.AddListener(() => {Time.timeScale = 1; SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);});
            backToMenu.onClick.AddListener(() => {warningPanel.SetActive(true);});
            warningNo.onClick.AddListener(() => {warningPanel.SetActive(false);});
            warningYes.onClick.AddListener(() => {Time.timeScale = 1; SceneManager.LoadScene(0);});
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                if(!paused)
                    PauseGame();
                else
                    ResumeGame();
            }
        }

        private void PauseGame() {
            warningPanel.SetActive(false);
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            paused = true;
        } 

        private void ResumeGame() {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pausePanel.SetActive(false);
            paused = false;
        }
    }
}