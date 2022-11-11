using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject lata;
    float timer = 1;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }     
        if (timer < 1)
        {
            var position = new Vector3(Random.Range(-11f, 11f), 10.36f, 2.1f);
            Instantiate(lata, position, Quaternion.identity);
            timer++;
        }

    }
}
