using FMODUnity;
using UnityEngine;
using System.Collections.Generic;

namespace AudioModule {
    [CreateAssetMenu(fileName = "AudioManagerSO", menuName = "AudioManagement/AudioManager")]
    public class AudioManagerSO : ScriptableObject {
        [SerializeField] private AudioChannelSO oneshotChannelSO, oneshotWithParametersSO, playInstanceBasedSound, stopInstanceBassedSound;
        private List<FMOD.Studio.EventInstance> eventInstances = new List<FMOD.Studio.EventInstance>();

        private void OnEnable() {
            eventInstances?.Clear();
            oneshotChannelSO.audioChannelEvent += PlayOneShot;
            oneshotWithParametersSO.audioChannelEventWithParameters += PlayOneShotWithParameters;
            playInstanceBasedSound.audioChannelEvent += PlayEventBasedAudios;
            stopInstanceBassedSound.audioChannelEvent += StopEventBasedAudios;
        }

        #region OneShots Functionality
        /// <summary>
        ///     This function is Playing oneShot for events without parameter
        /// </summary>
        /// <param name="eventPath"> FMOD GUID Event Path </param>
        /// <param name="positionObj"> Position mostly given for 3D audios </param>
        void PlayOneShot(FMOD.GUID eventPath, Vector3 positionObj = new Vector3()) {
            RuntimeManager.PlayOneShot(eventPath, positionObj);
        }

        /// <summary>
        ///     * This function is Playing oneShot for events having parameters. 
        ///     * So this functions is also settingUp parameters for oneShot audios.
        /// </summary>
        /// <param name="eventPath"> FMOD Event Path </param>
        /// <param name="positionObj"> Position mostly given for 3D audios </param>
        /// <param name="parameters"> Parameter whose valus you want to change </param>
        void PlayOneShotWithParameters(FMOD.GUID eventPath, Vector3 positionObj = new Vector3(), params (string name, float value)[] parameters) {
            FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(eventPath);

            foreach (var (name, value) in parameters) {
                instance.setParameterByName(name, value);
            }

            instance.set3DAttributes(positionObj.To3DAttributes());
            instance.start();
            instance.release();
        }
        #endregion

        #region Instance Based Sounds Functionality 

        /// <summary>
        ///     This function is Playing instanceBased Sounds for events
        /// </summary>
        /// <param name="eventPath"> FMOD Event Path </param>
        /// <param name="positionObj"> Position mostly given for 3D audios </param>
        private void PlayEventBasedAudios(FMOD.GUID eventPath, Vector3 positionObj = new Vector3())
        {
            FMOD.Studio.EventInstance tempAudioInstance;
            tempAudioInstance = RuntimeManager.CreateInstance(eventPath);
            tempAudioInstance.set3DAttributes(positionObj.To3DAttributes());
            eventInstances.Add(tempAudioInstance);

            FMOD.Studio.PLAYBACK_STATE pbState;
            tempAudioInstance.getPlaybackState(out pbState);
            if (pbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                tempAudioInstance.start();
            }
        }

        /// <summary>
        ///     This function is Stoping instanceBased Sounds for events
        /// </summary>
        /// <param name="eventPath"> FMOD Event Path </param>
        /// <param name="positionObj"> Position mostly given for 3D audios </param>
        private void StopEventBasedAudios(FMOD.GUID eventPath, Vector3 positionObj = new Vector3())
        {
            foreach (FMOD.Studio.EventInstance instance in eventInstances)
            {
                FMOD.GUID path;
                FMOD.Studio.EventDescription edState;
                instance.getDescription(out edState);
                edState.getID(out path);
                
                if (eventPath == path) {
                    FMOD.Studio.PLAYBACK_STATE pbState;
                    instance.getPlaybackState(out pbState);

                    if (pbState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                        eventInstances.Remove(instance);
                        instance.release();
                        break;
                    }
                }
            }
        }
        #endregion

        private void OnDisable() {
            oneshotChannelSO.audioChannelEvent -= PlayOneShot;
            oneshotWithParametersSO.audioChannelEventWithParameters -= PlayOneShotWithParameters;
            playInstanceBasedSound.audioChannelEvent -= PlayEventBasedAudios;
            stopInstanceBassedSound.audioChannelEvent -= StopEventBasedAudios;
        }
    }
}