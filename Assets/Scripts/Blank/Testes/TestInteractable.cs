using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank.Test {
    public class TestInteractable : MonoBehaviour
    {
        [SerializeField] Material toSwapTo;
        [SerializeField] float normalAfter = 3;
        private Material startMat;
        private Renderer renderer;
        private bool canInteract;

        private void Start() {
            renderer = GetComponent<Renderer>();
            startMat = renderer.material;
            canInteract = true;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) {
                TheOutSourcer.instance.interationManager.SetInteraction(OnInteract);
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("Player")) {
                TheOutSourcer.instance.interationManager.ClearInteration(OnInteract);
            }
        }

        private void OnInteract() {
            if(canInteract) {
                canInteract = false;
                renderer.material = toSwapTo;
                Invoke("BackToNormal", 2.5f);
            }
        }

        private void BackToNormal() {
            renderer.material = startMat;
            canInteract = true;
        }
    }
}