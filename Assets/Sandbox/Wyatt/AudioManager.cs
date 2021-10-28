using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;
        public Sound[] music;

        public static AudioManager instance;
        public Slider volumeSlider;
        public Slider musicSlider;
        public Slider sfxSlider;
        public float initialMasterVolume;
        public float initialMusicVolume;
        public float initialSfxVolume;
        [HideInInspector] public float masterVolume;
        //public float audioVolume;
        [HideInInspector] public float musicVolume;
        [HideInInspector] public float sfxVolume;
        public AudioMixerGroup audioMixer;
        public AudioMixerGroup musicMixer;
        public AudioMixerGroup sfxMixer;


        void Awake()
        {
			instance = this;

             foreach (Sound m in music)
            {
                m.source = gameObject.AddComponent<AudioSource>();
                m.source.clip = m.clip;
                m.source.volume = musicSlider.value;
                m.source.loop = m.loop;
                m.source.outputAudioMixerGroup = musicMixer;
            }
             /*
            foreach (Sound f in sfx)
            {
                f.source = gameObject.AddComponent<AudioSource>();
                f.source.clip = f.clip;
                f.source.volume = sfxSlider.value;
                f.source.loop = f.loop;
                f.source.outputAudioMixerGroup = musicMixer;
            }
            */

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                //s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = audioMixer;
            }
        }

        private void OnSceneChanged(Scene previousScene, Scene changedScene)
        {
            if (changedScene.name == "Main Menu")
            {
                Play("Menu_Music");
                Cursor.visible = true;
            }
            else
            {
                //Play("Folk");
                //Stop("AmbientForest");
                //Cursor.visible = true;
            }
        }

        private void Start()
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.name == "Main Menu")
            {
                Play("Menu_Music");
            }
            if (scene.name == "Level 1")
            {
                Play("Idle_BGM");
                Stop("Menu_Music");
            }
            if (scene.name == "Level 2")
            {
                Play("Level2_Music");
                Stop("Idle_BGM");
            }
            if (scene.name == "Level 3")
            {
                Play("Level3_Music");
                Stop("Level2_Music");
            }

            masterVolume = PlayerPrefsManager.GetMasterVolume();
            musicVolume = PlayerPrefsManager.GetMusicVolume();
            sfxVolume = PlayerPrefsManager.GetSfxVolume();

            // MASTER VOLUME
            if (masterVolume == 0) // What if you want the master volume at 0?
            {
                masterVolume = initialMasterVolume;
                PlayerPrefsManager.SetMasterVolume(masterVolume);
            }
            else if(masterVolume != initialMasterVolume)
            {
                masterVolume = PlayerPrefsManager.GetMasterVolume();
            }
            else
            {
                masterVolume = initialMasterVolume;
            }
            volumeSlider.value = masterVolume;
            ChangeVolume();


            // MUSIC VOLUME
            if (musicVolume == 0)
            {
                musicVolume = initialMusicVolume;
                PlayerPrefsManager.SetMusicVolume(musicVolume);
            }
            else if (musicVolume != initialMusicVolume)
            {
                musicVolume = PlayerPrefsManager.GetMusicVolume();
            }
            else
            {
                musicVolume = initialMusicVolume;
            }
            musicSlider.value = musicVolume;
            ChangeMusicVolume();


            // SFX VOLUME
            if (sfxVolume == 0)
            {
                sfxVolume = initialSfxVolume;
                PlayerPrefsManager.SetSfxVolume(sfxVolume);
            }
            else if (sfxVolume != initialSfxVolume)
            {
                sfxVolume = PlayerPrefsManager.GetSfxVolume();
            }
            else
            {
                sfxVolume = initialSfxVolume;
            }
            sfxSlider.value = sfxVolume;
            ChangeSFXVolume();
        }

        private void Update()
        {
            //volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
            //PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
     
            //musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
            //PlayerPrefsManager.SetMusicVolume(musicSlider.value);

            //sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
            //PlayerPrefsManager.SetSfxVolume(sfxSlider.value);
        }

        // Change Master volume
        public void ChangeVolume()
        {
			PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
			float newVolume = volumeSlider.value;
            AudioListener.volume = newVolume;
            //Debug.Log("Master Volume Value: " + volumeSlider.value);
        }

        // Change Music Volume
        public void ChangeMusicVolume()
        {
			PlayerPrefsManager.SetMusicVolume(musicSlider.value);
			foreach (Sound m in music)
            {
                m.source.volume = musicSlider.value;
            }
            //Debug.Log("Music Volume Value: " + volumeSlider.value);
        }

        // Change SFX Volume
        public void ChangeSFXVolume()
        {
			PlayerPrefsManager.SetSfxVolume(sfxSlider.value);
			foreach (Sound s in sounds)
            {
                s.source.volume = sfxSlider.value;
            }
            //Debug.Log("SFX Volume Value: " + volumeSlider.value);
        }

        // Play sound
        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            Sound m = Array.Find(music, sound => sound.name == name);

            if (s != null)
            { 
                s.source.Play();
            }
            else if(m != null)
            {
                m.source.Play();
            }
            else
            {
                //Debug.LogWarning("Sound: " + name + " not found!");
            }
            return;
        }

        // Stop sound
        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            Sound m = Array.Find(music, sound => sound.name == name);

            if (s != null)
            {
                s.source.Stop();
            }
            else if (m != null)
            {
                m.source.Stop();
            }
            else
            {
                Debug.LogWarning("Sound: " + name + " not found!");
            }
            return;
        }
    }
}