using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Blank {
    public class SFX : MonoBehaviour
    {
        public enum Pitch{Low, Normal, High};
        public static SFX instance;
        private AudioSource source;
        [SerializeField] AudioMixerGroup lowPitch;
        [SerializeField] AudioMixerGroup normalPitch;
        [SerializeField] AudioMixerGroup highPitch;
        [Header("Effects Data")]
        [SerializeField] SE hit;
        [SerializeField] SE click, jump, startChase, petting, cat;
        
        private void Start() {
            source = GetComponent<AudioSource>();
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.L))
                PlayHit();
        }

        public void PlayHit() {
            PlaySFX(hit);
        }

        public void PlayClick() {
            PlaySFX(click);
        }

        public void PlayJump() {
            PlayOneShotSFX(jump);
        }

        public void PlayStartChase() {
            PlaySFX(startChase);
        }

        public void PlayPetting() {
            PlaySFX(petting);
        }

        public void PlayCat() {
            PlayOneShotSFX(cat);
        }

        private void Awake() {
            if(instance == null) {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            } else
                Destroy(this.gameObject);
        }

        private void PlaySFX(SE se) {
            source.Stop();
            source.outputAudioMixerGroup = GetMixerGroup(se.pitch);
            source.clip = se.clip;
            source.Play();
        }

        public void StopSFX() {
            source.Stop();
        }

        private void PlayOneShotSFX(SE se) {
            source.outputAudioMixerGroup = GetMixerGroup(se.pitch);
            source.PlayOneShot(se.clip);
        }

        private AudioMixerGroup GetMixerGroup(Pitch pitch) {
            switch (pitch) {
                case Pitch.Low:
                    return lowPitch;
                case Pitch.Normal:
                    return normalPitch;
                case Pitch.High:
                    return highPitch;
            }
            return normalPitch;
        }

        [System.Serializable]
        private class SE {
            public AudioClip clip;
            public Pitch pitch;
        }
    }
}