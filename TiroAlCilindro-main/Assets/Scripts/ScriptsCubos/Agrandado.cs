using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agrandado : MonoBehaviour
{
    [SerializeField]
    GameObject cubo;
    public void agrandarcubo()
    {
        //Debug.Log("funciona" + gameObject.name);
        LeanTween.scale(cubo, new Vector3(4f, 4f, 4f), 1.0f).setEaseInBounce();
    }
    public void achicacubo()
    {
        LeanTween.scale(cubo, Vector3.one, 1.0f).setEaseInBounce();
    }
}
