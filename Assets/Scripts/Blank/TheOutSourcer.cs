using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class TheOutSourcer : MonoBehaviour
    {
        public static TheOutSourcer instance;
        public InterationManager interationManager;
        public InstructionText instructions;
        public Transform pickUpPos;
        public GameObject playerMesh;
        public PlayerHealthSystem healthSystem;

        private void Awake() {
            if(instance == null) {
                instance = this;
                pickUpPos = GameObject.FindGameObjectWithTag("PickUpPos").transform;
                playerMesh = GameObject.FindGameObjectWithTag("PlayerMesh");
            }
            else
                Destroy(this.gameObject);
        }
    }
}