using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class IdleState : AiState
    {
        private Vector2 idelingRange;
        private float idelingFor;
        private float timer;

        public IdleState(Vector2 timeRange) {
            idelingRange = timeRange;
        }

        public override void OnEnter(TheAi ai) {
            timer = 0;
            idelingFor = Random.Range(idelingRange.x, idelingRange.y);
        }

        public override void Exicute(TheAi ai) {
            Debug.Log("<color=green>Ideling</color>");
            if(ai.playerInSight && ai.currentMindState == MindState.Psych) {
                ai.SwithState(myConnections[1]);
                return;
            }
            
            timer += Time.deltaTime;
            if(timer >= idelingFor)
                ai.SwithState(myConnections[0]);
        }
        
    }
}