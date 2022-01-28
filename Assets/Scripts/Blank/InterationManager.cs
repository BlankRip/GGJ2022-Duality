using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Blank {
    public class InterationManager : MonoBehaviour
    {
        private IInteractable currentInteractable;
        private IInteractable quiedInteractable;
        private bool occupied;

        private void Awake() {
            TheOutSourcer.instance.interationManager = this;
        }

        public void SetInteraction(IInteractable interactable) {
            if(currentInteractable == null) {
                currentInteractable = interactable;
                currentInteractable.OnEnter();
            } else {
                if(currentInteractable != interactable && quiedInteractable == null) {
                    quiedInteractable = interactable;
                }
            }
        }

        public void ClearInteration(IInteractable interactable) {
            if(quiedInteractable == interactable)
                quiedInteractable = null;
            if(currentInteractable == interactable) {
                currentInteractable = null;
                if(quiedInteractable != null) {
                    currentInteractable = quiedInteractable;
                    StopAllCoroutines();
                    StartCoroutine(EnterEndOffFrame());
                    quiedInteractable = null;
                }
            }
        }

        private IEnumerator EnterEndOffFrame() {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            currentInteractable.OnEnter();
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