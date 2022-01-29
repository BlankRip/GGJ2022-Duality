using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] Button theButton;

    void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        theButton.onClick.AddListener(() => {SceneManager.LoadScene(0);});
    }
}
