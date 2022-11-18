using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEliseo02 : MonoBehaviour
{
    public enum EstadosSelector
    {
        EnEspera,
        SelecciónObjetoMover,
        SelecciónObjetoRotar,
        SelecciónObjetoEscalar,
        Mover,
        Rotar,
        Escalar,
        Soltar,
    }
    [SerializeField]
    EstadosSelector estadoActual = EstadosSelector.EnEspera;
    [SerializeField]
    GameObject objetoSeleccionado;

    void ObjetosSeleccionadoCambiarEstado()
    {
        switch (estadoActual)
        {
            case EstadosSelector.SelecciónObjetoMover:
                estadoActual = EstadosSelector.Mover;
                break;
            case EstadosSelector.SelecciónObjetoRotar:
                //objetoSeleccionado.GetComonent<Rigidbody>().isKinematic = true;
                //mousePos = Input.mousePosition;
                estadoActual = EstadosSelector.Rotar;
                break;
            case EstadosSelector.SelecciónObjetoEscalar:
                estadoActual = EstadosSelector.Escalar;
                break;
        }
    }

    void RotarObjeto()
    {
        //Vector2 mousedelta = mousePos - (Vector2)Input.mousePosition;

            //objetoSeleccionado.transform.Rotate(mousedelta.y, mousedelta.x, 0f);

        //mousePos = Input.mousePosition;
        if (Input.GetMouseButtonUp(0))
        {
            //objetoSeleccionado.Getcomponent<Rigidbody>().iskinematic = false;
            estadoActual = EstadosSelector.EnEspera;
        }
    }
    void EscalarObjeto()
    {
        objetoSeleccionado.transform.localScale += Vector3.one * Input.mouseScrollDelta.y;

        if (Input.GetMouseButtonUp(0))
        {
            estadoActual = EstadosSelector.EnEspera;
        }
    }

}
