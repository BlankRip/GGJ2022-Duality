using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class DmgCollider : MonoBehaviour
    {
        [SerializeField] int dmageDelt = 1;

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) {
                TheOutSourcer.instance.healthSystem.TakeDamage(dmageDelt);
            }
        }
    }
}