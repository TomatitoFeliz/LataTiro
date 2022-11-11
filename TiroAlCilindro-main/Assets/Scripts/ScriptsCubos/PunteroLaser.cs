using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunteroLaser : MonoBehaviour
{
    //[SerializeField]
    //GameObject Cubo;
    //int Datos = 0;
    bool grande = false;
    void Update()
    {
        //suponemos que dejamos de pulsar el boton izquierdo del ratón
        if (Input.GetMouseButtonUp(0))
        {
            //Creamos un vector3 como "pos" para el lugar en el que se encuentra el raton
            Vector3 pos = Input.mousePosition;
            //Creamos una variable ray como "rayo" y se la adherimos a la cámara?¿
            Ray rayo = Camera.main.ScreenPointToRay(pos);
            //obtenemos la información del rayo anteriormente creado y la denominamos "hitinfo"?¿
            RaycastHit hitInfo;

            //si usando fisicas de rayos?¿ el rayo que denominamos anteriormente (sale de la cámara), no da (out) la 
            //la informacion del dicho rayo la cual denominamos hitinfo, significa que hemos dado con el rayo?¿
            if (Physics.Raycast(rayo, out hitInfo) == true)
            {
                //Si la hitinfo colisiona con algun GameObject con el tag cubo
                if (hitInfo.collider.tag.Equals("Cubo"))
                {
                    //Datos++;
                    //Debug.Log(Datos);
                    if (grande == false)
                    {
                        hitInfo.collider.GetComponent<Agrandado>().agrandarcubo();
                        grande = true;
                    }
                    else if (grande == true)
                    {
                        hitInfo.collider.GetComponent<Agrandado>().achicacubo();
                        grande = false;
                    }
                }
            }
        }
    }
}
