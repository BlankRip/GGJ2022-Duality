using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class HidingBox : MonoBehaviour, IInteractable
    {
        private bool inHiding;
        private CharacterController playerCC;

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) {
                if(!TheOutSourcer.instance.interationManager.IsOccupied()) {
                    if(playerCC == null)
                        playerCC = other.GetComponent<CharacterController>();
                    TheOutSourcer.instance.interationManager.SetInteraction(this);
                }
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("Player")) {
                TheOutSourcer.instance.interationManager.ClearInteration(this);
                TheOutSourcer.instance.instructions.CloseInstruction();
            }
        }

        private void Hide() {
            inHiding = true;
            playerCC.enabled = false;
            TheOutSourcer.instance.playerMesh.SetActive(false);
            TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Exit Hidding");
        }

        private void ExitHiding() {
            inHiding = false;
            TheOutSourcer.instance.playerMesh.SetActive(true);
            playerCC.enabled = true;
            TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Hide");
        }

        public void Interact() {
            if(!inHiding)
                Hide();
            else
                ExitHiding();
        }

        public void OnEnter() {
            TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Hide");
        }
    }
}