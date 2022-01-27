using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Blank {
    public class InterationManager : MonoBehaviour
    {
        private UnityEvent interactEvent;
        private void Start() {
            interactEvent = new UnityEvent();
            TheOutSourcer.instance.interationManager = this;
        }

        public void SetInteraction(UnityAction call) {
            interactEvent.AddListener(call);
        }

        public void ClearInteration(UnityAction call) {
            interactEvent.RemoveListener(call);
        }

        public void Interact() {
            interactEvent.Invoke();
        }
    }
}