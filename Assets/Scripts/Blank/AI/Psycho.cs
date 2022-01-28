using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Blank {
    public class Psycho : TheAi
    {
        [SerializeField] Fov myFOV;
        [Header("Patrol stuff")]
        [SerializeField] List<Transform> patrolWaypoints;
        [SerializeField] bool loopPatrol;
        [Range(0, 25)] [SerializeField] int idleChance;

        [Header("Chase Stuff")]
        [SerializeField] float atkRange = 0.5f;

        [Header("Attack Stuff")]
        [SerializeField] ParticleSystem atkParticles;
        [SerializeField] Collider dmgCollider;
        [SerializeField] float attackDuration;

        private void Start() {
            StandardStart();
            PatrolState patrolState = new PatrolState(patrolWaypoints, loopPatrol, idleChance);
            ChaseState chaseState = new ChaseState(TheOutSourcer.instance.interationManager.transform, atkRange, new Vector2(2, 6));
            IdleState idleState = new IdleState(new Vector2(3.5f, 6));
            AttackState atkState = new AttackState(dmgCollider, atkParticles, attackDuration);

            patrolState.myConnections.Add(idleState);
            patrolState.myConnections.Add(chaseState);

            idleState.myConnections.Add(patrolState);
            idleState.myConnections.Add(chaseState);

            chaseState.myConnections.Add(patrolState);
            chaseState.myConnections.Add(atkState);

            atkState.myConnections.Add(chaseState);

            SwithState(patrolState);
        }

        private void Update() {
            playerInSight = myFOV.inTheView;
            currentState.Exicute(this);
        }
    }
}