using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    float timeMin, timeMax;
    [SerializeField]
    GameObject prefablata;
    float timer;

    private void Start()
    {
        timer = Random.Range(timeMin, timeMax);
        //InvokeRepeating("CreateTarget", timeMin, Random.Range(timeMin, timeMax);

    }

    void CreateTarget()
    {
        Instantiate(prefablata);
    }

}
