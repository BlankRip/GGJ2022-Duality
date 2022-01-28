using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class ChaseState : AiState
    {
        private Transform playerTransform;
        private float attackRange;
        private float distance;
        private Vector2 seachTimeRange;
        private float serachTime;
        private float timer;
        private bool searchTimerOn;


        public ChaseState(Transform chaseTarget, float attackRange, Vector2 seachTimeRange) {
            playerTransform = chaseTarget;
            this.attackRange = attackRange * attackRange;
            this.seachTimeRange = seachTimeRange;
        }

        public override void OnEnter(TheAi ai) {
            searchTimerOn = false;
        }

        public override void Exicute(TheAi ai) {
            if(!searchTimerOn) {
                if(!ai.playerInSight) {
                    searchTimerOn = true;
                    serachTime = Random.Range(seachTimeRange.x, seachTimeRange.y);
                    timer = 0;
                }
            } else {
                if(ai.playerInSight)
                    searchTimerOn = false;
                timer += Time.deltaTime;
                if(timer >= serachTime) {
                    ai.SwithState(myConnections[0]);
                    return;
                }
            }

            ai.na.SetDestination(playerTransform.position);
            distance = (playerTransform.position - ai.transform.position).sqrMagnitude;
            if(distance <= attackRange)
                ai.SwithState(myConnections[1]);
        }
    }
}