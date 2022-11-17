using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testeo : MonoBehaviour
{
    //Fuente:
    //https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/#:~:text=In%20Unity%2C%20getting%20the%20mouse,bottom%20left%20of%20the%20screen.

    int contador = 0;
    [SerializeField]
    GameObject cubo;
    public Vector3 screenPosition;
    public Vector3 worldPosition;

    private void Update()
    {
        screenPosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            worldPosition = hitData.point;
        }
        
        transform.position = worldPosition;

        if (Input.GetMouseButtonUp(0))
        {
            if (hitData.collider.tag.Equals("Cubo"))
            {
                while (contador <= 10)
                {
                    cubo.transform.position = worldPosition;
                    contador++;
                }

                if (contador == 9)
                {
                    contador = 0;
                }
            }

        }

    }
}
