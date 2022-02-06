namespace AudioModule {
    public class AudioPlayer : AudioPlayerBase {
        public override void RaiseEvent() {
            audioChannelSO.audioChannelEvent.Invoke(eventPath.Guid, transform.position);
        }
    }
}
