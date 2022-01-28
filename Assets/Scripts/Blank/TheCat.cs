using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class TheCat : MonoBehaviour, IInteractable
    {
        [SerializeField] float yeetForce = 3;
        private Rigidbody rb;
        private bool picked, justDropped;

        private void Start() {
            rb = GetComponent<Rigidbody>();
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
            picked = true;
            TheOutSourcer.instance.instructions.ShowInstruction("Press LMB to Drop");
            rb.useGravity = false;
            rb.isKinematic = true;
            transform.parent = TheOutSourcer.instance.pickUpPos;
            transform.localPosition = Vector3.zero;
        }

        private void Drop() {
            picked = false;
            transform.localRotation = Quaternion.identity;
            transform.parent = null;
            Vector3 forceToAdd = transform.forward * yeetForce;
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(forceToAdd, ForceMode.Impulse);
            justDropped = true;
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