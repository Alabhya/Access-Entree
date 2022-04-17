using UnityEngine;
using UnityEngine.Events;

namespace AudioModule {
    [CreateAssetMenu(fileName = "AudioEventChannel", menuName = "AudioManagement/AudioEventChannelSO")]
    public class AudioChannelSO : ScriptableObject {
        public UnityAction<FMOD.GUID, Vector3> audioChannelEvent;
        public UnityAction<FMOD.GUID, Vector3, (string name, float value)[]> audioChannelEventWithParameters;

        public void RaiseEvent(FMOD.GUID eventPath, Vector3 objPos) {
            audioChannelEvent.Invoke(eventPath, objPos);
        }

        public void RaiseEvent(string eventPath, Vector3 objPos) {
            FMOD.GUID guidPath = FMODUnity.RuntimeManager.PathToGUID(eventPath);
            RaiseEvent(guidPath, objPos);
        }

        public void RaiseEvent(FMOD.GUID eventPath, Vector3 objPos, params (string name, float value)[] parameters) {
            audioChannelEventWithParameters.Invoke(eventPath, objPos, parameters);
        }

        public void RaiseEvent(string eventPath, Vector3 objPos, params (string name, float value)[] parameters) {
            FMOD.GUID guidPath = FMODUnity.RuntimeManager.PathToGUID(eventPath);
            RaiseEvent(guidPath, objPos, parameters);
        }
    }
}