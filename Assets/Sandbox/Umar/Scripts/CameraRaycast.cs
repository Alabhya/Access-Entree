using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRaycast : MonoBehaviour
{
    [SerializeField] LayerMask layerToMask;
    Ray RayOrigin;
    RaycastHit HitInfo;
    Transform cameraTransform;

    private void Start()
    {
        cameraTransform = gameObject.GetComponent<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 100.0f, layerToMask))
        {
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.yellow);

            if (HitInfo.collider.gameObject.tag.Equals("Construction")) {
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.green);
            }
            Debug.Log("Obj name = " + HitInfo.collider.gameObject.name);
        }
    }
}
