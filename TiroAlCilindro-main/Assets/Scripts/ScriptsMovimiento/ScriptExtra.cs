using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptExtra : MonoBehaviour
{
    //Textos:
    [SerializeField]
    GameObject UICrear;
    [SerializeField]
    GameObject UI;

    private void Start()
    {
        UICrear.SetActive(false);
        UI.SetActive(true);
    }
    public enum EstadosSelector
    {
        EnEspera,
        SeleccionarObjetoMover,
        Mover,
        EsperaMover,
        Soltar,
        SeleccionarObjetoRotate,
        Rotate,
        SeleccionarObjetoScale,
        Escalar,
        SelecionarCrear,
        Crear,
    }

    [SerializeField]
    EstadosSelector estadoActual = EstadosSelector.EnEspera;
    [SerializeField]
    GameObject objetoSeleccionado;
    Vector3 originalScale;
    [SerializeField]
    GameObject prefab;
    
    private void Update()
    {
        switch (estadoActual)
        {
            case EstadosSelector.SeleccionarObjetoMover:
                SelectCubeMove();
                break;
            case EstadosSelector.Mover:
                MoveCube();
                break;
            case EstadosSelector.Soltar:
                ReleaseCube();
                break;
            case EstadosSelector.SelecionarCrear:
                SelecionarCrearCubo();
                break;
            case EstadosSelector.EsperaMover:
                estadoActual = EstadosSelector.Mover;
                break;
                
        }

    }
    
    //------------------------------------MOVIMIENTO----------------------------------------------------------------------------
    void SelectCubeMove()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Select");
            Vector3 pos = Input.mousePosition;
            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitinfo;
            if (Physics.Raycast(rayo, out hitinfo) == true)
            {
                if (hitinfo.collider.tag.Equals("Cubo"))
                {
                    objetoSeleccionado = hitinfo.collider.gameObject;
                    originalScale = objetoSeleccionado.transform.localScale;
                    LeanTween.scale(objetoSeleccionado, objetoSeleccionado.transform.localScale * 1.2f, 0.75f).setEaseInBounce().setLoopPingPong();
                    estadoActual = EstadosSelector.Mover;
                }
            }
        }
    }
    void MoveCube()
    {
        Vector3 pos = Input.mousePosition;
        Ray rayo = Camera.main.ScreenPointToRay(pos);
        RaycastHit hitinfo;
        objetoSeleccionado.SetActive(false);
        if (Physics.Raycast(rayo, out hitinfo) == true)
        {
            objetoSeleccionado.transform.position = hitinfo.point + Vector3.up * objetoSeleccionado.transform.localScale.y / 2;
            estadoActual = EstadosSelector.Mover;
        }
        objetoSeleccionado.SetActive(true);

        if (Input.GetMouseButtonUp(0))
        {
            estadoActual = EstadosSelector.Soltar;
        }
    }
    void ReleaseCube()
    {
        LeanTween.cancel(objetoSeleccionado);
        LeanTween.scale(objetoSeleccionado, originalScale, 0f);
        Debug.Log("Release");
        objetoSeleccionado = null;
        estadoActual = EstadosSelector.EnEspera;
    }
    public void ActivarMover()
    {
        switch (estadoActual)
        {
            case EstadosSelector.EnEspera:
                estadoActual = EstadosSelector.SeleccionarObjetoMover;
                break;
        }
    }

    //------------------------------------ROTACIÓN----------------------------------------------------------------------------


    //-------------------------------------ESCALADO----------------------------------------------------------------------------


    //-------------------------------------CREACIÓN----------------------------------------------------------------------------
    public void SeleccionarCrear()
    {
        UICrear.SetActive(true);
        UI.SetActive(false);
    }
    public void SeleccionarCancelar()
    {
        UICrear.SetActive(false);
        UI.SetActive(true);
    }

    void SelecionarCrearCubo()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("CrearCubo");
            Vector3 pos = Input.mousePosition;
            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitinfo;
            if (Physics.Raycast(rayo, out hitinfo) == true)
            {
                if (hitinfo.collider.tag.Equals("Suelo"))
                {
                    estadoActual = EstadosSelector.Crear;
                }
            }
        }
    }

    public void Crear(GameObject objetoACrear)
    {   
        objetoSeleccionado = Instantiate(objetoACrear, Vector3.zero, Quaternion.identity);
        originalScale = objetoSeleccionado.transform.localScale;
        estadoActual = EstadosSelector.EsperaMover;
    }

    public void ActivarCrear()
    {
        switch (estadoActual)
        {
            case EstadosSelector.EnEspera:
                estadoActual = EstadosSelector.SelecionarCrear;
                break;
        }
    }
}
