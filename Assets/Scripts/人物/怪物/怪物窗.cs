using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 怪物窗 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main != null)
            transform.rotation = Camera.main.transform.rotation;
    }
}
