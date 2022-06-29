using FMODUnity;
using UnityEngine;
using FMOD.Studio;
using System.Collections.Generic;

namespace AudioModule {
    [CreateAssetMenu(fileName = "AudioHelperSO", menuName = "AudioManagement/AudioHelper")]
    public class AudioHelper : ScriptableObject {
        /// <summary>
        ///     This function is made to get playbackState of any FMOD Event
        /// </summary>
        /// <param name="instance"> Instance object who's playbact state is required </param>
        /// <returns> Playbact State of required instance </returns>
        public PLAYBACK_STATE GetPlaybackState(EventInstance instance) {
            PLAYBACK_STATE pS;
            instance.getPlaybackState(out pS);
            return pS;
        }

        /// <summary>
        ///     This function is made to set volume of FMOD EventEmitter
        /// </summary>
        /// <param name="emitterObj"> Object with StuioEventEmitter component </param>
        /// <param name="volValue"> Volume value to be updated </param>
        public void SetSoundEmitterVolume(GameObject emitterObj, float volValue) {
            StudioEventEmitter eventEmitterObj = emitterObj.GetComponent<StudioEventEmitter>();
            EventInstance eventInstanceObj;
            eventInstanceObj = eventEmitterObj.EventInstance;
            eventInstanceObj.setVolume(volValue);
        }

        /// <summary>
        ///     This function is made to set volume of FMOD EventInstance object
        /// </summary>
        /// <param name="eventInstanceObj"> FMOD EventInstance object </param>
        /// <param name="volValue"> Volume value to be updated </param>
        public void SetEventInstanceVolume(EventInstance eventInstanceObj, float volValue) {
            eventInstanceObj.setVolume(volValue);
        }

        /// <summary>
        ///     This Function is written so that we can get emitter's event path
        /// </summary>
        /// <param name="soundEmmiterObj"> FMOD Sound Emitter Object</param>
        /// <returns> Event path as string of given Sound Emitter's object</returns>
        public string GetEmitterEventPath(StudioEventEmitter soundEmmiterObj) {
            string eventPath = default;
            EventDescription edState;
            edState = soundEmmiterObj.EventDescription;
            edState.getPath(out eventPath);
            return eventPath;
        }

        /// <summary>
        ///     This function is for getting path to all banks used in FMOD Studio Project
        /// </summary>
        /// <returns> string names of all banks </returns>
        public List<string> GetAllBanks() {
            List<string> allBanks = new List<string>();
            FMOD.Studio.System studioSystem;
            allBanks = FMODUnity.Settings.Instance.Banks;
            
            return allBanks;
        }

        /// <summary>
        ///     Function is used for getting paths of all events in different banks
        /// </summary>
        /// <returns> string paths of events</returns>
        public List<string> GetAllEventsOfBanks() {
            List<string> allBanks = new List<string>(); 
            List<string> allClipEvents = new List<string>();
            FMOD.Studio.System studioSystem;
            allBanks = FMODUnity.Settings.Instance.Banks;
            studioSystem = FMODUnity.RuntimeManager.StudioSystem;
            
            foreach (string obj in allBanks) {
                string objPath = "bank:/" + obj;
                Bank bankObj;
                studioSystem.getBank(objPath, out bankObj);

                EventDescription[] allEvents;
                bankObj.getEventList(out allEvents);
                //Debug.Log("Bank = " + objPath + " || GameplayBank = " + allEvents.Length);

                foreach (EventDescription eventDec in allEvents)
                {
                    string path;
                    eventDec.getPath(out path);
                    allClipEvents.Add(path);
                    //print("Events = " + path);
                }
            }
            return allClipEvents;
        }
    }
}