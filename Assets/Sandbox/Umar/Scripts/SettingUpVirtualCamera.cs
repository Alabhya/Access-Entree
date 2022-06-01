using Cinemachine;
using UnityEngine;

public class SettingUpVirtualCamera : CinemachineCameras<CinemachineVirtualCameraBase> {
    [SerializeField] private GameEventSO eventWithObj;

    private void OnEnable() {
        eventWithObj.eventWithGameObject += LookAtGameObject;
    }

    private void LookAtGameObject(GameObject objToLookAt) {
        cinemachinCamera.Follow = objToLookAt.transform;
        cinemachinCamera.LookAt = objToLookAt.transform;
    }

    private void OnDisable() {
        eventWithObj.eventWithGameObject -= LookAtGameObject;
    }
}

public class CinemachineCameras<T> : MonoBehaviour where T : CinemachineVirtualCameraBase {
    protected T cinemachinCamera;

    protected void Awake() {
        cinemachinCamera = GetComponent<T>();
    }
}