using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Blank {
    public class InterationManager : MonoBehaviour
    {
        private UnityEvent interactEvent;
        private bool occupied;

        private void Start() {
            interactEvent = new UnityEvent();
            TheOutSourcer.instance.interationManager = this;
        }

        public void SetInteraction(UnityAction call) {
            if(!occupied) {
                interactEvent.AddListener(call);
                occupied = true;
            }
        }

        public void ClearInteration(UnityAction call) {
            interactEvent.RemoveListener(call);
            occupied = false;
        }

        public void Interact() {
            interactEvent.Invoke();
        }

        public bool IsOccupied() {
            return occupied;
        }


    }
}