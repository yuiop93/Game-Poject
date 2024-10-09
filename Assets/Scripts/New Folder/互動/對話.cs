using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class 對話 : MonoBehaviour
{
    private 劇情 劇情1;
    [SerializeField]
    private 劇情_SO 劇情SO;
    [SerializeField]
    private GameObject 預置按鈕;
    private GameObject 按鈕;
    private Transform 互動;
    public GameObject[] 攝影機;

    // Start is called before the first frame update
    void Start()
    {
        劇情1 = GameObject.Find("劇情").GetComponent<劇情>();
        互動 = GameObject.Find("互動").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            按鈕 = Instantiate(預置按鈕);
            按鈕.transform.SetParent(互動, false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(按鈕.gameObject);
            按鈕 = null;
        }
    }
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && 按鈕 != null&&按鈕.activeSelf)
        {
            播放劇情();
        }
    }
    void 播放劇情()
    {
        劇情1.劇情SO = 劇情SO;
        劇情1.攝影機 = 攝影機;
        劇情1.顯示劇情();
    }
}
