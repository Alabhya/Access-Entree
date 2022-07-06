using UnityEngine;

namespace AudioModule {

    /// <summary>
    ///     This class is made abstract so that different type of 
    ///     Oneshots can be entertained
    /// </summary>
    public abstract class AudioPlayerBase : MonoBehaviour
    {
        [SerializeField] protected AudioChannelSO audioChannelSO;
        [SerializeField] protected FMODUnity.EventReference eventPath;

        public abstract void RaiseEvent();
    }

}