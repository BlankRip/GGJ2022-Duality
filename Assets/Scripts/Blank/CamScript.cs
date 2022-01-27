using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class CamScript : MonoBehaviour
    {
        [Header("Things needed to move camera around")]
        [SerializeField] Transform target;
        [SerializeField] float mouseSensitivity = 2;
        [SerializeField] float verticalClampMin = -15.0f;
        [SerializeField] float verticalClampMax = 60.0f;
        [SerializeField] float smoothCamRotation = 10.0f;

        float mouseX;                                            //The current horizontal input value for horizontal rotaion
        float mouseY;                                            //The current vertical input value for the vertical rotation
        
        [Header("Things needed to do wall clipping for the camera")]
        [SerializeField] float minDistance = 1.0f;
        [SerializeField] float maxDistance = 8.0f;
        [SerializeField] float smoothCamMovement = 10.0f;
        [SerializeField] LayerMask WallClipLayerMask;

        float distance;
        Vector3 camDirection;
        Vector3 desiredCameraDir;
        RaycastHit hit;

        void Awake()
        {
            Cursor.visible = false;                                     //Setting cursor to not be visible when playing the game
            Cursor.lockState = CursorLockMode.Locked;                   //Locking the cursor to the center of the screen so that it does not move out of the window
            //For wall clipping
            camDirection = transform.position-target.position;          //Getting the local unit direction vector
            distance = camDirection.magnitude;                          //Getting the local magnitude to the postion with is basically distance from parent to camera
        }

        void FixedUpdate()
        {
            mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
            mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            mouseY = Mathf.Clamp(mouseY, verticalClampMin, verticalClampMax);

            desiredCameraDir = Quaternion.Euler(mouseY, mouseX, 0) * Vector3.back;

            // Check if there is a wall or object between the camera and move the camera close to the target if so else set the camera to be at the normal distance from the target
            if (Physics.Raycast(target.transform.position, desiredCameraDir, out hit, maxDistance, WallClipLayerMask)) {
                distance = Mathf.Clamp((hit.distance), minDistance, maxDistance);
            } else {
                distance = maxDistance;
            }
            transform.position = Vector3.Lerp(transform.position, desiredCameraDir * distance + target.position, Time.deltaTime * smoothCamMovement);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(-desiredCameraDir), Time.deltaTime * smoothCamRotation);
        }
    }
}