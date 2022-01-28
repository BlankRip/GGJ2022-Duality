using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Blank {
    public class Psycho : TheAi
    {
        [System.Serializable]
        private class MindStateData {
            public MindState state;
            public Material skin;
            public GameObject face;
        }

        [Header("FOV")]
        [SerializeField] Fov myFOV;

        [Header("Flipping Stuff")]
        [SerializeField] MindStateData normalState;
        [SerializeField] MindStateData psychState;
        [SerializeField] Renderer skinRenderer;
        private MindState currentMindState;

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
            currentMindState = MindState.Psych;
            FilpMindState();

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

        private void FilpMindState() {
            List<Material> mats = new List<Material>();
            skinRenderer.GetMaterials(mats);
            if(currentMindState == MindState.Normal) {
                currentMindState = psychState.state;
                mats[0] = psychState.skin;
                psychState.face.SetActive(true);
                normalState.face.SetActive(false);
            } else {
                currentMindState = normalState.state;
                mats[0] = normalState.skin;
                normalState.face.SetActive(true);
                psychState.face.SetActive(false);
            }
                skinRenderer.materials = mats.ToArray();
        }

        private void Update() {
            playerInSight = myFOV.inTheView;
            currentState.Exicute(this);
        }
    }
}