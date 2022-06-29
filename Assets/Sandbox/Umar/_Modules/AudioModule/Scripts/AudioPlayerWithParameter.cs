using UnityEngine;

namespace AudioModule {
    public class AudioPlayerWithParameter : AudioPlayerBase {
        [SerializeField] private string paramName;
        [SerializeField] private int paramValue;

        public override void RaiseEvent() {
            audioChannelSO.RaiseEvent(eventPath.Guid, transform.position, (paramName, paramValue));
        }
    }
}