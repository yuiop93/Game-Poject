using UnityEngine;

public class ShowcaseCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    public float distance = 5.0f; // 初始距離
    public float minDistance = 2.0f; // 最小距離
    public float maxDistance = 10.0f; // 最大距離

    [Header("Rotation Settings")]
    public float rotationSpeed = 100.0f; // 旋轉速度

    [Header("Zoom Settings")]
    public float zoomSpeed = 2.0f; // 縮放速度

    private float _targetYaw;
    private float _targetPitch;

    private void OnEnable()
    {
        ResetCamera();
    }

    private void LateUpdate()
    {
        HandleRotation();
        HandleZoom();
        UpdateCameraPosition();
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(0)) // 按住左鍵旋轉
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            _targetYaw += mouseX;
            _targetPitch -= mouseY;

            // 限制俯仰角度
            _targetPitch = Mathf.Clamp(_targetPitch, -80f, 80f);
        }
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance -= scroll;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    private void UpdateCameraPosition()
    {
        if (transform.parent == null)
        {
            Debug.LogError("The camera must be a child of a target object!");
            return;
        }

        Transform target = transform.parent;
        Quaternion rotation = Quaternion.Euler(_targetPitch, _targetYaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    private void ResetCamera()
    {
        if (transform.parent == null)
        {
            Debug.LogError("The camera must be a child of a target object!");
            return;
        }

        // 重設俯仰和偏航角
        _targetYaw = transform.parent.eulerAngles.y;
        _targetPitch = transform.parent.eulerAngles.x;

        // 重設距離
        distance = 5.0f;

        // 更新相機位置
        UpdateCameraPosition();
    }
}
