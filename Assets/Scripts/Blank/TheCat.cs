using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class TheCat : MonoBehaviour, IInteractable
    {
        [SerializeField] float yeetForce = 3;
        private Rigidbody rb;
        private bool picked, justDropped, canInteract;

        private void Start() {
            rb = GetComponent<Rigidbody>();
            canInteract = true;
        }

        private void OnCollisionEnter(Collision other) {
            if(other.gameObject.CompareTag("Ground")) {
                justDropped = false;
                rb.velocity = Vector3.zero;
            }
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) {
                TheOutSourcer.instance.interationManager.SetInteraction(this);
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("Player")) {
                TheOutSourcer.instance.interationManager.ClearInteration(this);
                if(!justDropped)
                    TheOutSourcer.instance.instructions.CloseInstruction();
                    
            }
        }

        private void PickUp() {
            gameObject.tag = "Untagged";
            picked = true;
            TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Drop");
            rb.useGravity = false;
            rb.isKinematic = true;
            transform.parent = TheOutSourcer.instance.pickUpPos;
            transform.localPosition = Vector3.zero;
        }

        private void Drop() {
            gameObject.tag = "Cat";
            picked = false;
            transform.localRotation = Quaternion.identity;
            transform.parent = null;
            Vector3 forceToAdd = transform.forward * yeetForce;
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(forceToAdd, ForceMode.Impulse);
            justDropped = true;
        }

        public void InteractStatus(bool state) {
            canInteract = state;
        }

        public void SelfDestruction() {
            Destroy(this.gameObject);
        }

        private void OnDestroy() {
            TheOutSourcer.instance.interationManager.ClearInteration(this);
            if(!justDropped)
                TheOutSourcer.instance.instructions.CloseInstruction();
        }

        public void Interact() {
            if(canInteract) {
                if(picked)
                    Drop();
                else
                    PickUp();
            }
        }

        public void OnEnter() {
            if(canInteract && !picked)
                TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Pick Up");
        }
    }
}