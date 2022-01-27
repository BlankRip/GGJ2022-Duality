using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Blank {
    public class PlayerHealthSystem : MonoBehaviour
    {
        [SerializeField] List<Image> hearts;
        [SerializeField] Sprite emptyHeart, fullHeart;
        private int currentIndex;

        private void Start() {
            TheOutSourcer.instance.healthSystem = this;
            currentIndex = hearts.Count - 1;
        }

        public void TakeDamage(int amount) {
            if(currentIndex > (amount - 1)) {
                for (int i = 0; i < amount; i++) {
                    hearts[currentIndex].sprite = emptyHeart;
                    currentIndex--;
                }
            } else {
                foreach (Image img in hearts)
                    img.sprite = emptyHeart;
                currentIndex = -1;
                Debug.Log("<color=red>Game-Over Here</color>");
                //TODO
            }
        }

        public void Heal(int amount) {
            if(currentIndex < (hearts.Count - 1)) {
                currentIndex++;
                for (int i = 0; i < amount; i++) {
                    hearts[currentIndex].sprite = fullHeart;
                    currentIndex++;
                    if(currentIndex == hearts.Count)
                        break;
                }
                currentIndex--;
            }
        }
    }
}