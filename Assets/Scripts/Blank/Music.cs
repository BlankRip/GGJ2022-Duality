using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Blank.Audio {
    public class Music : MonoBehaviour
    {
        static Music Instance;
        [SerializeField] List<BackTrack> album;
        private AudioSource audioSource;
        private BackTrack currentTrack;
        private bool volumeUp;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            audioSource = GetComponent<AudioSource>();
            SelectClip();
            SwithClip();
        }

        private void Update() {
            if(volumeUp) {
                audioSource.volume += (Time.deltaTime * 0.5f);
                if(audioSource.volume >= 1)
                    volumeUp = false;
            }

            if(!audioSource.isPlaying) {
                SelectClip();
                SwithClip();
            }
        }

        private void SelectClip() {
            bool currentExist = true;
            if(currentTrack == null)
                currentExist = false;

            int rand = Random.Range(0, album.Count);
            currentTrack = album[rand];
            album.RemoveAt(rand);
            if(currentExist)
                album.Add(currentTrack);
        }

        private void SwithClip() {
            audioSource.Stop();
            audioSource.clip = currentTrack.clip;
            audioSource.outputAudioMixerGroup = currentTrack.mixerGroup;
            audioSource.volume = 0;
            volumeUp = true;
            audioSource.Play();
        }

        [System.Serializable]
        private class BackTrack {
            public AudioClip clip;
            public AudioMixerGroup mixerGroup;
        }
    }

}