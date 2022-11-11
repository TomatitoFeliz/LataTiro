using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrastre : MonoBehaviour
{
    [SerializeField]
    GameObject item;
    private void Update()
    {
        item.SetActive(false);

    }
}
