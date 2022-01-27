using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class Player : MonoBehaviour
    {
        private PlayerMovement movement;
        private CharacterController cc;
        private InterationManager interationManager;
        private float horizontalInput, verticalInput;
        private bool grounded, sprint, walk, jump;
        private bool ctrlRegestered, shiftRegerered;

        private void Start() {
            movement = GetComponent<PlayerMovement>();
            cc = GetComponent<CharacterController>();
            interationManager = GetComponent<InterationManager>();
        }

        private void Update() {
            grounded = cc.isGrounded;
            MovementInputHandeling();
        }

        private void MovementInputHandeling() {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            if(grounded) {
                if(shiftRegerered) {
                    sprint = true;
                    shiftRegerered = false;
                }
                if(ctrlRegestered) {
                    walk = true;
                    ctrlRegestered = false;
                }
            }

            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                interationManager.Interact();
            }

            if(Input.GetKeyDown(KeyCode.LeftShift)) {
                if(grounded)
                    sprint = true;
                else
                    shiftRegerered = true;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift)) {
                sprint = false;
                shiftRegerered = false;
            }
            
            if(Input.GetKeyDown(KeyCode.LeftControl)) {
                if(grounded)
                    walk = true;
                else
                    ctrlRegestered = true;
            }
            if(Input.GetKeyUp(KeyCode.LeftControl)) {
                walk = false;
                ctrlRegestered = false;
            }
            
            if(grounded && Input.GetKeyDown(KeyCode.Space))
                jump = true;
        }

        private void FixedUpdate() {
            movement.Move(horizontalInput, verticalInput, ref jump, sprint, walk);
        }
    }
}