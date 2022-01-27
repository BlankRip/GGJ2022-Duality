using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class HidingBox : MonoBehaviour
    {
        private bool inHiding;
        private CharacterController playerCC;

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) {
                if(playerCC == null)
                    playerCC = other.GetComponent<CharacterController>();
                TheOutSourcer.instance.interationManager.SetInteraction(Hide);
                TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Hide");
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("Player") && !inHiding) {
                TheOutSourcer.instance.interationManager.ClearInteration(Hide);
                TheOutSourcer.instance.instructions.CloseInstruction();
            }
        }

        private void Hide() {
            inHiding = true;
            playerCC.enabled = false;
            TheOutSourcer.instance.playerMesh.SetActive(false);
            TheOutSourcer.instance.interationManager.ClearInteration(Hide);
            TheOutSourcer.instance.interationManager.SetInteraction(ExitHiding);
            TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Exit Hidding");
        }

        private void ExitHiding() {
            inHiding = false;
            TheOutSourcer.instance.interationManager.ClearInteration(ExitHiding);
            TheOutSourcer.instance.playerMesh.SetActive(true);
            playerCC.enabled = true;
        }
    }
}