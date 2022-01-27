using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController cc;
        private float currentSpeed;
        private Vector3 gravityVector; 
        [SerializeField] Transform cameraTransform;
        [SerializeField] float gravity = -9.81f;
        [SerializeField] float jumpHight = 3;
        [SerializeField] float normalSpeed = 10;
        [SerializeField] float sprintSpeed = 20;
        [SerializeField] float walkSpeed = 5;
        [SerializeField] float rotationSpeed = 10;
        private bool grounded;
        private Quaternion turnAngle;



        private void Start() {
            cc = GetComponent<CharacterController>();
            currentSpeed = normalSpeed;
            if(gravity > 0)
                gravity *= -1;
        }

        public void Move(float horizontal, float vertical, ref bool jump, bool sprint, bool walk) {
            grounded = cc.isGrounded;
            if(sprint)
                currentSpeed = sprintSpeed;
            else if(walk)
                currentSpeed = walkSpeed;
            else
                currentSpeed = normalSpeed;

            Vector3 move = ((transform.forward * vertical) + (transform.right * horizontal)) * currentSpeed * Time.deltaTime;
            cc.Move(move);

            if(horizontal != 0 || vertical != 0) {
                turnAngle = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, rotationSpeed * Time.deltaTime);
            }
            
            if(grounded && gravityVector.y < 0)
                gravityVector.y = -2;
            if(jump) {
                jump = false;
                gravityVector.y = Mathf.Sqrt(-2 * jumpHight * gravity);
            }
            gravityVector.y += gravity * Time.deltaTime;
            cc.Move(gravityVector * Time.deltaTime);
        }
    }
}