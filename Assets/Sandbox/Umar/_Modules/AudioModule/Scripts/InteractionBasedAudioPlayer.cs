using UnityEngine;

namespace AudioModule {
    public class InteractionBasedAudioPlayer : AudioPlayerBase {
        [SerializeField] private InteractionBasedEvents eventType;
        [SerializeField] private string triggerTag;

        #region Interaction Based Function Definations
        private void OnCollisionEnter(Collision collision) {
            if (eventType == InteractionBasedEvents.OnColliderEnter) {
                if (collision.gameObject.CompareTag(triggerTag)) {
                    RaiseEvent();
                }
            }
        }

        private void OnCollisionStay(Collision collision) {
            if (eventType == InteractionBasedEvents.OnColliderStay) {
                if (collision.gameObject.CompareTag(triggerTag)) {
                    RaiseEvent();
                }
            }
        }

        private void OnCollisionExit(Collision collision) {
            if (eventType == InteractionBasedEvents.OnColliderExit) {
                if (collision.gameObject.CompareTag(triggerTag)) {
                    RaiseEvent();
                }
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (eventType == InteractionBasedEvents.OnTriggerEnter) {
                if (other.gameObject.CompareTag(triggerTag)) {
                    RaiseEvent();
                }
            }
        }

        private void OnTriggerStay(Collider other) {
            if (eventType == InteractionBasedEvents.OnTriggerStay) {
                if (other.gameObject.CompareTag(triggerTag)) {
                    RaiseEvent();
                }
            }
        }

        private void OnTriggerExit(Collider other) {
            if (eventType == InteractionBasedEvents.OnTriggerExit) {
                if (other.gameObject.CompareTag(triggerTag)) {
                    RaiseEvent();
                }
            }
        }
        #endregion

        public override void RaiseEvent() {
            audioChannelSO.audioChannelEvent.Invoke(eventPath.Guid, transform.position);
        }
    }

    /// <summary>
    ///     Don't change the pattern of these Enum and add new Enum at the end
    /// </summary>
    public enum InteractionBasedEvents {
        None, OnTriggerEnter, OnTriggerStay, OnTriggerExit, OnColliderEnter, OnColliderExit, OnColliderStay
    }
}