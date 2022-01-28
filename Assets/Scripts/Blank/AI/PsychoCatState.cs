using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class PsychoCatState : AiState
    {
        private Psycho myPsycho;
        private ParticleSystem petingParticle, killParticle;
        private float stoppingDistance, distance, timer, petTime;
        private bool petting;
        private TheCat cat;

        public PsychoCatState(ParticleSystem petting, ParticleSystem killing, Psycho psycho, float rangeDistance, float pettingTime) {
            myPsycho = psycho;
            petingParticle = petting;
            killParticle = killing;
            stoppingDistance = rangeDistance * rangeDistance;
            petTime = pettingTime;
        }

        public override void OnEnter(TheAi ai) {
            Collider[] count = Physics.OverlapSphere(ai.transform.position, 20);
            cat = null;
            foreach (Collider col in count) {
                if(col.gameObject.CompareTag("Cat"))
                    cat = col.GetComponent<TheCat>();
            }
            if(cat == null)
                ai.SwithState(myConnections[0]);
            
            try {
                ai.na.SetDestination(cat.transform.position);
            } catch {}
        }

        public override void Exicute(TheAi ai) {
            if(petting) {
                timer += Time.deltaTime;
                if(timer >= petTime) {
                    petingParticle.Stop();
                    cat.InteractStatus(true);
                    petting = false;
                    ai.SwithState(myConnections[0]);
                    return;
                }
            }

            distance = (ai.transform.position - cat.transform.position).sqrMagnitude;
            if(distance <= stoppingDistance) {
                ai.na.SetDestination(ai.transform.position);
                if(ai.currentMindState == MindState.Normal) {
                    if(!petingParticle.isPlaying) {
                        petingParticle.Play();
                        cat.InteractStatus(false);
                    }
                    myPsycho.timer = 0;
                    petting = true;
                } else {
                    killParticle.Play();
                    cat.SelfDestruction();
                    myPsycho.FilpMindState();
                    myPsycho.timer = 0;
                    myPsycho.timer -= 1;
                    ai.SwithState(myConnections[0]);
                    return;
                }
            }
        }
    }
}