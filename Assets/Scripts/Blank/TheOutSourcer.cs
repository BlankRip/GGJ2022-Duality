using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class TheOutSourcer : MonoBehaviour
    {
        public static TheOutSourcer instance;
        public InterationManager interationManager;
        public InstructionText instructions;

        private void Awake() {
            if(instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }
    }
}