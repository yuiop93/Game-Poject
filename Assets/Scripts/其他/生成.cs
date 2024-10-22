using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 生成 : MonoBehaviour
{
    public GameObject 場景;
    public GameObject 玩家;
    public GameObject 基礎UI;
    void Awake()
    {
        GameObject gameobject= Instantiate(基礎UI);
        gameobject.name = "UI控制";
        GameObject gameobject1= Instantiate(場景);
        gameobject1.name = "場景";
        GameObject gameobject2= Instantiate(玩家);
        gameobject2.name = "Player";
    }
}
