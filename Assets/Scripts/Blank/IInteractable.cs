using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public interface IInteractable
    {
        void Interact();
        void OnEnter();
    }
}