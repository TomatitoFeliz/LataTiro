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
            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayo, out hitInfo) == true)
            {
                if (hitInfo.collider.tag.Equals("Cubo"))
                {

                    //Debug.Log("Funciona");
                    //if (arrastrando == false)
                    //{
                    //hitInfo.collider.GetComponent<Arrastre>().arrastre();
                    //arrastrando = true;
                    //}
                    //else if (arrastrando == true)
                    //{
                    //hitInfo.collider.GetComponent<Arrastre>().noarrastre();
                    //arrastrando = false;
                    //}

                    if (cubo == true)
                    {
                        cubo.SetActive(false);
                        Debug.Log("true");
                    }
                    
                }
            }
        }
        
        if (cubo == false)
        {
            cubo.SetActive(true);
            Debug.Log("false");
        }
    }
}
