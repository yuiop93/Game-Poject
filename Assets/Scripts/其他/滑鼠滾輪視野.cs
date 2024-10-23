using UnityEngine;
using Cinemachine;

public class 滑鼠滾輪視野 : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSpeed = 10f;  // 调整FOV的速度
    public float minFOV = 20f;     // 最小FOV
    public float maxFOV = 40f;     // 最大FOV

    void Update()
    {
        if (互動選擇.isSelecting)
        {
            return;
        }
        if (坐下.isSitting)
        {
            virtualCamera.m_Lens.FieldOfView = maxFOV;
            return;
        }
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            float currentFOV = virtualCamera.m_Lens.FieldOfView;
            currentFOV -= scrollInput * zoomSpeed;
            currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
            virtualCamera.m_Lens.FieldOfView = currentFOV;
        }
    }
}