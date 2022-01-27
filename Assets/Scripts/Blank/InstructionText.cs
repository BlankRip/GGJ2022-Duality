using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Blank {
    public class InstructionText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI theText;

        private void Start() {
            TheOutSourcer.instance.instructions = this;
        }

        public void ShowInstruction(string instruction) {
            theText.text = instruction;
            theText.gameObject.SetActive(true);
        }

        public void CloseInstruction() {
            theText.text = "";
            theText.gameObject.SetActive(false);
        }
    }
}