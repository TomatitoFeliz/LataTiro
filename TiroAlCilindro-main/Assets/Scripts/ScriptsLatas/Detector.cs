using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Detector : MonoBehaviour
{
    [SerializeField]
    GameObject puntuacion;
    [SerializeField]
    GameObject reset;
    [SerializeField]
    GameObject fin;
    int cuenta = 0;

    private void Start()
    {
        fin.gameObject.SetActive(false);
        puntuacion.gameObject.SetActive(true);
        reset.gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lata")
        {
            cuenta++;
        }
    }

    private void Update()
    {
        if (cuenta == 16)
        {
            fin.gameObject.SetActive(true);
            puntuacion.gameObject.SetActive(false);
            reset.gameObject.SetActive(false);
        }
    }
}
