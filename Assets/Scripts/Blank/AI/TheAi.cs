using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Blank {
    public enum MindState {Normal, Psych};
    public class TheAi : MonoBehaviour
    {
        protected AiState currentState;
        public float currentSpeed;
        public bool playerInSight;
        public NavMeshAgent na;
        public MindState currentMindState;

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