using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zain360
{
    public enum eSoundList
    {
        SOUND_NONE,
        SOUND_CLICK,
        SOUND_BOMB,
        SOUND_ARROW,
        SOUND_CUBE_POP,
        SOUND_DISCO_BOMB,
        SOUND_SEARCHING_FOR_PLAYERS,
        SOUND_SWAP_BOX,
        SOUND_BGM
    }


    [System.Serializable]
    public class SoundMap
    {
        public eSoundList soundType = eSoundList.SOUND_NONE;
        public AudioClip[] audioClip;
    }

    [System.Serializable]
    public class SoundMaps
    {
        public eSoundList soundType = eSoundList.SOUND_NONE;
        //public AudioClip[] audioClip;
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance = null;

        private List<AudioSource> soundAudioSources = new List<AudioSource>();
        private List<AudioSource> musicAudioSources = new List<AudioSource>();

        public SoundMap[] soundMaps;
        public SoundMaps[] clickVariations;

        public float delay = 0f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void FillAudioSources()
        {
            AudioSource[] audioSources = gameObject.GetComponentsInChildren<AudioSource>();

            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource.transform.CompareTag("Sound"))
                {
                    soundAudioSources.Add(audioSource);
                }

                if (audioSource.transform.CompareTag("Music"))
                {
                    musicAudioSources.Add(audioSource);
                }
            }
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        public AudioSource GetFreeSoundSource()
        {
            foreach (AudioSource audioSource in soundAudioSources)
            {
                if (!audioSource.isPlaying)
                {
                    return audioSource;
                }
            }

            return null;
        }

        public AudioSource GetFreeMusicSource()
        {
            foreach (AudioSource audioSource in musicAudioSources)
            {
                if (!audioSource.isPlaying)
                {
                    return audioSource;
                }
                else
                {
                    audioSource.Stop();
                    return audioSource;
                }
            }

            return null;
        }

        public SoundMap GetSoundMap(eSoundList sound)
        {
            foreach (SoundMap soundMap in soundMaps)
            {
                if (soundMap.soundType == sound)
                {
                    return soundMap;
                }
            }

            return null;
        }

        public void PlaySound(eSoundList sound)
        {
            SoundMap soundMap = GetSoundMap(sound);

            if (soundMap != null && soundMap.audioClip != null)
            {
                AudioSource targetSoundSource = GetFreeSoundSource();

                if (targetSoundSource != null)
                {
                    targetSoundSource.clip = soundMap.audioClip[0];
                    targetSoundSource.Play();
                }
            }
        }

        public void PlaySound(eSoundList sound, int idx)
        {
            SoundMap soundMap = GetSoundMap(sound);

            if (soundMap != null && soundMap.audioClip != null)
            {
                AudioSource targetSoundSource = GetFreeSoundSource();

                if (targetSoundSource != null)
                {
                    if (idx > soundMap.audioClip.Length - 1)
                    {
                        idx = soundMap.audioClip.Length - 1;
                    }
                    targetSoundSource.clip = soundMap.audioClip[idx];
                    targetSoundSource.Play();
                }
            }
        }

        public void PlayMusic(eSoundList sound)
        {
            SoundMap soundMap = GetSoundMap(sound);

            if (soundMap != null && soundMap.audioClip != null)
            {
                AudioSource targetMusicSource = GetFreeMusicSource();

                if (targetMusicSource != null)
                {
                    targetMusicSource.clip = soundMap.audioClip[0];
                    targetMusicSource.PlayDelayed(delay);
                }
            }
        }

        public bool AlreadyPlayingTheSame(eSoundList sound)
        {
            if (musicAudioSources[0].clip != null)
                return musicAudioSources[0].clip.name == GetSoundMap(sound).audioClip[0].name;

            return false;
        }

        public void SetSoundSourceState(bool state)
        {
            //Inverse the state to Set Mute Value
            for (int i = 0; i < soundAudioSources.Count; i++)
            {
                soundAudioSources[i].mute = !state;
            }
        }

        public void SetMusicSourceState(bool state)
        {
            //Inverse the state to Set Mute Value
            for (int i = 0; i < musicAudioSources.Count; i++)
            {
                musicAudioSources[i].mute = !state;
            }
        }

        void Start()
        {
            FillAudioSources();
        }
    }
}