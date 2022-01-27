using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Blank {
    public class InterationManager : MonoBehaviour
    {
        private UnityEvent interactEvent;
        private IInteractable currentInteractable;
        private bool occupied;

        private void Start() {
            interactEvent = new UnityEvent();
            TheOutSourcer.instance.interationManager = this;
        }

        public void SetInteraction(IInteractable interactable) {
            if(currentInteractable == null && currentInteractable != interactable) {
                currentInteractable = interactable;
                currentInteractable.OnEnter();
            }
        }

        public void ClearInteration(IInteractable interactable) {
            if(currentInteractable == interactable)
                currentInteractable = null;
        }

        public void Interact() {
            if(currentInteractable != null)
                currentInteractable.Interact();
        }

        public bool IsOccupied() {
            return occupied;
        }
    }
}