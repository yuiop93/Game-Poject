using UnityEngine;
using Cinemachine;

public class 滑鼠滾輪視野 : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSpeed = 1f;
    public float minDistance = 1f;
    public float maxDistance = 5f;

    private void Start()
    {
        virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = maxDistance;
    }
    void OnEnable()
    {
        virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = maxDistance;
    }
    void Update()
    {
        if (互動選擇.isSelecting)
        {
            return;
        }
        if (坐下.isSitting)
        {
            virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = maxDistance;
            return;
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            var thirdPersonFollow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            float currentDistance = thirdPersonFollow.CameraDistance;
            currentDistance -= scrollInput * zoomSpeed;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
            thirdPersonFollow.CameraDistance = currentDistance;
        }
    }
}