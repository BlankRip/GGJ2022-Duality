using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] bool lastLevel;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            int buldIndex = 0;
            if(!lastLevel)
                buldIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(buldIndex);
        }
    }
}
