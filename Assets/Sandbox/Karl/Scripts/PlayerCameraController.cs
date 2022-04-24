using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Coroutine _zoomCoroutine;
    private Transform _camTransform;

    [SerializeField] private float _camX;
    [SerializeField] private float _camZ;
    [SerializeField] private float _camSpeed = 5f;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _camTransform = Camera.main.transform;
    }

    private void Start()
    {
        _playerInput.Player_Test.SecondaryTouchContact.started += _ => CameraZoomStart();
        _playerInput.Player_Test.SecondaryTouchContact.canceled += _ => CameraZoomStop();
    }

    private void OrbitCamera()
    { 
    
    }

    private void CameraZoomStart()
    {
        _zoomCoroutine = StartCoroutine(ZoomDetect());
    }

    private void CameraZoomStop()
    {
        StopCoroutine(_zoomCoroutine);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private IEnumerator MoveDetect()
    {
        while (true)
        {


            yield return null;
        }
    }

    private IEnumerator ZoomDetect()
    {
        float prevDistance = 0f;
        float currDistance = 0f;

        while (true)
        {
            Vector2 a = _playerInput.Player_Test.PrimaryTouchPos.ReadValue<Vector2>();
            Vector2 b = _playerInput.Player_Test.SecondaryTouchPos.ReadValue<Vector2>();
            currDistance = Vector2.Distance(a, b);

            TouchDetected(prevDistance, currDistance);
            prevDistance = currDistance;

            yield return null;
        }
    }

    private void TouchDetected(float prevDistance, float currDistance)
    {
        Vector3 targetPos = _camTransform.position;
        targetPos.z = (currDistance > prevDistance) ? targetPos.z - 1 :
                      (currDistance < prevDistance) ? targetPos.z + 1 :
                      targetPos.z;

        _camTransform.position = Vector3.Slerp(_camTransform.position, targetPos, Time.deltaTime * _camSpeed);
    }
}
