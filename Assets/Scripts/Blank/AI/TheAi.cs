using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Blank {
    public class TheAi : MonoBehaviour
    {
        protected AiState currentState;
        public float currentSpeed;
        public NavMeshAgent na;

        protected void StandardStart() {
            na = GetComponent<NavMeshAgent>();
        }

        public void SwithState(AiState state) {
            if(currentState != null)
                currentState.OnExit(this);
            currentState = state;
            currentState.OnEnter(this);
        }
    }
}