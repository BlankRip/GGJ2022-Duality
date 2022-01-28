using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class PatrolState : AiState
    {
        private List<Transform> waypoints;
        private bool loopPath;
        private int current;
        private float distance;
        private float switchDistance = 0.15f;
        private int indexChanger;
        private List<int> goToIdel;

        public PatrolState(List<Transform> patrolPath, bool loopPath, int idleChance) {
            waypoints = patrolPath;
            this.loopPath = loopPath;
            current = 0;
            switchDistance *= switchDistance;
            indexChanger = 1;
            goToIdel = new List<int>();
            for (int i = 0; i < (idleChance + 1); ) {
                int toAdd = Random.Range(0, 101);
                if(!goToIdel.Contains(toAdd)) {
                    goToIdel.Add(toAdd);
                    i++;
                }
            }
        }

        public override void Exicute(TheAi ai) {
            //if player spoted go to chase

            distance = (ai.transform.position - new Vector3(waypoints[current].position.x, ai.transform.position.y,waypoints[current].position.z)).sqrMagnitude;
            if(distance <= switchDistance) {
                int sholudIdle = Random.Range(0, 101);
                if(goToIdel.Contains(sholudIdle)) {
                    ai.SwithState(myConnections[0]);
                    return;
                }

                if(current < waypoints.Count) {
                    if(current == waypoints.Count - 1) {
                        if(loopPath)
                            current = 0;
                        else
                            indexChanger = -1;
                    } else if(current == 0) {
                        indexChanger = 1;
                    }
                }
                ai.na.SetDestination(new Vector3(waypoints[current].position.x, ai.transform.position.y,waypoints[current].position.z));
                current += indexChanger;
            }
        }
    }
}