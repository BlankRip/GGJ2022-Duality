using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank {
    public class HpPickUp : MonoBehaviour
    {
        [SerializeField] int healAmount;
        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) {
                if(TheOutSourcer.instance.healthSystem.Heal(healAmount))
                    Destroy(this.gameObject);
            }
        }
    }
}