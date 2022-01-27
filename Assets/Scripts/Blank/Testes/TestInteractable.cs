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
        private bool canInteract, playerIn;

        private void Start() {
            renderer = GetComponent<Renderer>();
            startMat = renderer.material;
            canInteract = true;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) {
                TheOutSourcer.instance.interationManager.SetInteraction(OnInteract);
                if(canInteract)
                    TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Interact");
                playerIn = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("Player")) {
                TheOutSourcer.instance.interationManager.ClearInteration(OnInteract);
                TheOutSourcer.instance.instructions.CloseInstruction();
                playerIn = false;
            }
        }

        private void OnInteract() {
            if(canInteract) {
                TheOutSourcer.instance.instructions.CloseInstruction();
                canInteract = false;
                renderer.material = toSwapTo;
                Invoke("BackToNormal", 2.5f);
            }
        }

        private void BackToNormal() {
            renderer.material = startMat;
            if(playerIn)
                TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Interact");
            canInteract = true;
        }
    }
}