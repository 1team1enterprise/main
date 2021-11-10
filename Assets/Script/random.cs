using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random : MonoBehaviour
{
    void Awake()
    {
        instance = this;
    }
    public static random instance;


    public int Xnum;
    public int Ynum;
    void Start()
    {
        Xnum = Random.Range(1,12);
        Ynum = Random.Range(1,12);
    }
}
