using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Blank.Audio {
    public class SFX : MonoBehaviour
    {
        public enum Pitch{Low, Normal, High};
        public static SFX instance;
        private AudioSource source;
        [SerializeField] AudioMixerGroup lowPitch;
        [SerializeField] AudioMixerGroup normalPitch;
        [SerializeField] AudioMixerGroup highPitch;

        private void Awake() {
            if(instance == null) {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            } else
                Destroy(this.gameObject);
        }

        public void PlaySFX(AudioClip clip, Pitch pitch) {
            source.Stop();
            source.outputAudioMixerGroup = GetMixerGroup(pitch);
            source.clip = clip;
            source.Play();
        }

        public void PlayOneShotSFX(AudioClip clip, Pitch pitch) {
            source.outputAudioMixerGroup = GetMixerGroup(pitch);
            source.PlayOneShot(clip);
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
    }
}