using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class AttackState : AiState
    {
        private ParticleSystem attackParticles;
        private float attackTime;
        private Collider attackCollider;
        private float timer;

        public AttackState(Collider atkCollider, ParticleSystem atkEffect, float atkDuration) {
            attackCollider = atkCollider;
            attackTime = atkDuration;
            attackParticles = atkEffect;
        }

        public override void OnEnter(TheAi ai) {
            timer = 0;
            attackParticles.Play();
            attackCollider.enabled = true;
        }

        public override void Exicute(TheAi ai) {
            timer += Time.deltaTime;
            if(timer >= attackTime) {
                attackCollider.enabled = false;
                ai.SwithState(myConnections[0]);
            }
        }
    }
}