using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank.Test {
    public class TestHeldPickup : MonoBehaviour, IInteractable
    {
        private bool picked;
        private Vector3 spawnPos;

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) {
                spawnPos = transform.position;
                TheOutSourcer.instance.interationManager.SetInteraction(this);
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("Player")) {
                Debug.Log("was Here");
                TheOutSourcer.instance.interationManager.ClearInteration(this);
                TheOutSourcer.instance.instructions.CloseInstruction();
            }
        }

        private void PickUp() {
            picked = true;
            TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Drop");
            transform.parent = TheOutSourcer.instance.pickUpPos;
            transform.localPosition = Vector3.zero;
        }

        private void Drop() {
            picked = false;
            transform.parent = null;
            transform.position = spawnPos;
        }

        public void Interact() {
            if(picked)
                Drop();
            else
                PickUp();
        }

        public void OnEnter() {
            TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Pick Up");
        }
    }
}