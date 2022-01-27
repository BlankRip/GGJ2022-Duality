using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blank.Test {
    public class HelathSystemTest : MonoBehaviour
    {
        [SerializeField] int healthToRemove = 1;
        [SerializeField] int healthToAdd = 2;

        private void Update() {
            if(Input.GetKeyDown(KeyCode.J))
                TheOutSourcer.instance.healthSystem.TakeDamage(healthToRemove);
            if(Input.GetKeyDown(KeyCode.K))
                TheOutSourcer.instance.healthSystem.Heal(healthToAdd);
        }
    }
}