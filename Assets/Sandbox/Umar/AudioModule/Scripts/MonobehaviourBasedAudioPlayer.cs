using UnityEngine;

namespace AudioModule {
    public class MonobehaviourBasedAudioPlayer : AudioPlayerBase {
        [SerializeField] private MonobehaviourBasedEventTypes eventType;

        #region Monobehavior Based Function Defination
        private void Awake() {
            if (eventType == MonobehaviourBasedEventTypes.Awake) {
                RaiseEvent();
            }
        }

        private void OnEnable() {
            if (eventType == MonobehaviourBasedEventTypes.OnEnable) {
                RaiseEvent();
            }
        }

        private void Start() {
            if (eventType == MonobehaviourBasedEventTypes.Start) {
                RaiseEvent();
            }
        }

        private void OnDisable() {
            if (eventType == MonobehaviourBasedEventTypes.OnDisable) {
                RaiseEvent();
            }
        }
        #endregion

        public override void RaiseEvent() {
            audioChannelSO.audioChannelEvent.Invoke(eventPath.Guid, transform.position);
        }
    }

    /// <summary>
    /// Dont change the pattern of these Enum and add new Enum at the end 
    /// </summary>
    public enum MonobehaviourBasedEventTypes
    {
        Awake, OnEnable, Start, OnDisable
    }
}

