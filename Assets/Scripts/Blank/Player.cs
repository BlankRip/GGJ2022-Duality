using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class Player : MonoBehaviour
    {
        private PlayerMovement movement;
        private CharacterController cc;
        private float horizontalInput, verticalInput;
        private bool grounded, sprint, walk, jump;

        private void Start() {
            movement = GetComponent<PlayerMovement>();
            cc = GetComponent<CharacterController>();
        }

        private void Update() {
            grounded = cc.isGrounded;
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            if(grounded && Input.GetKeyDown(KeyCode.LeftShift))
                sprint = true;
            if(Input.GetKeyUp(KeyCode.LeftShift))
                sprint = false;
            
            if(grounded && Input.GetKeyDown(KeyCode.LeftControl))
                walk = true;
            if(Input.GetKeyUp(KeyCode.LeftControl))
                walk = false;
            
            if(grounded && Input.GetKeyDown(KeyCode.Space))
                jump = true;
        }

        private void FixedUpdate() {
            movement.Move(horizontalInput, verticalInput, ref jump, sprint, walk);
        }
    }
}