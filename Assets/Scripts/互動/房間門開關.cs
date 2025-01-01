using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 房間門開關 : MonoBehaviour
{
    public float 前後距離 = 0.01f;
    public float 左右距離 = 1.0f;
    public float 移動速度 = 2.0f;
    public void 開門()
    {
        StartCoroutine(切換門狀態());
    }

    private IEnumerator 切換門狀態()
    {
        if(materialIndex >= 0)
        {
            ChangeMaterialColor(targetObject, materialIndex, newColor);
        }
        Vector3 後移目標 = transform.position + transform.TransformDirection(Vector3.back * 前後距離);
        Vector3 左移目標 = 後移目標 + transform.TransformDirection(Vector3.left * 左右距離);
        while (Vector3.Distance(transform.position, 後移目標) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, 後移目標, 移動速度 * Time.deltaTime);
            yield return null;
        }
        transform.position = 後移目標;
        while (Vector3.Distance(transform.position, 左移目標) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, 左移目標, 移動速度 * Time.deltaTime);
            yield return null;
        }
        transform.position = 左移目標;
        
    }
    public GameObject targetObject;
    public int materialIndex = 3;
    public Color newColor;
    private void ChangeMaterialColor(GameObject obj, int index, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer != null && index >= 0 && index < renderer.materials.Length)
        {
            Material[] materials = renderer.materials;
            materials[index].color = color;
            renderer.materials = materials;
        }
        else
        {
            Debug.LogError("无效的材质索引或缺少 Renderer 组件。");
            return;
        }
    }
}
