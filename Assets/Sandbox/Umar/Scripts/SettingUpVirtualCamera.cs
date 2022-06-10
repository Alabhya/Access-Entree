using Cinemachine;
using UnityEngine;

/// <summary>
///     This component will be present on differetn virtuals cameras and will hold
///     reference to CineMachineCamera component and will help to assing gameobject 
///     to Follow and Aim property with the help of GameEventSO.cs event.
/// </summary>
public class SettingUpVirtualCamera : CinemachineCameras<CinemachineVirtualCameraBase> {
    [Tooltip("Event that hold reference to gameobject, whom to follow or aim")]
    [SerializeField] private GameEventSO eventWithObj;
    [Tooltip("Will be true if you want to look at on specific gameobject")]
    [SerializeField] private bool lookAt = default;
    [Tooltip("Will be true if you want to follow specific object")]
    [SerializeField] private bool follow = default;

    private void OnEnable() {
        // Subscribing function to event
        eventWithObj.eventWithGameObject += LookAtAndFollowGameObject;
    }

    // Function that will be called when required GameEventSO is Raised
    private void LookAtAndFollowGameObject(GameObject objToFollowAndLookAt) {
        if (lookAt)
            LookAtGameObject(objToFollowAndLookAt);
        if (follow)
            FollowGameObject(objToFollowAndLookAt);
    }

    // Function to assing transform to Cinemachine LookAt property
    private void LookAtGameObject(GameObject objToLookAt) {
        cinemachineCamera.LookAt = objToLookAt.transform;
    }

    // Function to assing transform to Cinemachine Follow property
    private void FollowGameObject(GameObject objToFollow) {
        cinemachineCamera.Follow = objToFollow.transform;
    }

    private void OnDisable() {
        // Unsubscribing function to event
        eventWithObj.eventWithGameObject -= LookAtAndFollowGameObject;
    }
}

/// <summary>
///     This is a Generic class used to get different type of Cinemachine Cameras
/// </summary>
/// <typeparam name="T"> Cinemachine Cameras that inherited from CinemachineVirtualCameraBase class </typeparam>
public class CinemachineCameras<T> : MonoBehaviour where T : CinemachineVirtualCameraBase {
    // This variable will hold reference to the CinemachineCamere type
    protected T cinemachineCamera;

    protected void Awake() {
        // This will get the Cinemachine Camera component reference when gameobject will be loaded
        cinemachineCamera = GetComponent<T>();
    }
}