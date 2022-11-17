using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunteriLaser02 : MonoBehaviour
{
    [SerializeField]
    GameObject cubo;
    //bool arrastrando = false;
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 pos = Input.mousePosition;
            Vector3 loc = Camera.main.ScreenToWorldPoint(pos);
            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayo, out hitInfo) == true)
            {
                if (hitInfo.collider.tag.Equals("Cubo"))
                {
                    cubo.transform.Translate(loc);
                }
            }
        }
    }
}
