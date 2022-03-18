using FMODUnity;

namespace AudioModule
{
    public class LoopingAudioPlayer : StudioEventEmitter
    {
        /// <summary>
        ///     This function Will be used for setting Up int parameter Value
        /// </summary>
        /// <param name="paramName"> parameter name </param>
        /// <param name="paramValue"> int value </param>
        public void ChangeParameterValue(string paramName, int paramValue) {
            gameObject.GetComponent<StudioEventEmitter>().SetParameter(paramName, paramValue);
        }

        /// <summary>
        ///     This function Will be used for setting Up float parameter Value
        /// </summary>
        /// <param name="paramName"> parameter name </param>
        /// <param name="paramValue"> float value </param>
        public void ChangeParameterValue(string paramName, float paramValue) {
            gameObject.GetComponent<StudioEventEmitter>().SetParameter(paramName, paramValue);
        }
    }
}