using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEliseo : MonoBehaviour
{
    float xrot;
    float yrot;
    public float rotspeed;
    Quaternion fromRotation;
    Quaternion toRotation;

    public Quaternion rot;
    public enum EstadosSelector
    {
        EnEspera,
        SeleccionarObjetoMover,
        Mover,
        Escalar,
        Soltar,
        SeleccionarObjetoRotate,
        Rotate,
        SeleccionarObjetoScale,
    }
    [SerializeField]
    EstadosSelector estadoActual = EstadosSelector.EnEspera;


    public GameObject cube;
    Vector3 originalScale;

    private void Update()
    {
        switch (estadoActual)
        {
            case EstadosSelector.SeleccionarObjetoMover:
                SelectCubeMove();
                break;
            case EstadosSelector.SeleccionarObjetoRotate:
                SelectCubeRotate();
                break;
            case EstadosSelector.SeleccionarObjetoScale:
                SelectCubeScale();
                break;
            case EstadosSelector.Mover:
                MoveCube();
                break;
            case EstadosSelector.Soltar:
                ReleaseCube();
                break;
            case EstadosSelector.Rotate:
                RotateCube();
                break;
            case EstadosSelector.Escalar:
                ScaleCube();
                break;
        }

        //if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
        //{
            //if (cube == null)
            //{
                //SelectCube();
            //}
            //else
            //{
                //ReleaseCube();
            //}
        //}

        //if (cube != null)
        //{
            //MoveCube();
        //}

    }
    void ScaleCube()
    {
        Vector3 pos = Input.mousePosition;
        Ray rayo = Camera.main.ScreenPointToRay(pos);
        RaycastHit hitinfo;
        cube.SetActive(false);
        if (Physics.Raycast(rayo, out hitinfo) == true)
        {    
            Vector3 esc = cube.transform.localScale;
            esc.y += Input.mouseScrollDelta.y;
            cube.transform.localScale = esc;
            estadoActual = EstadosSelector.Escalar;
        }
        cube.SetActive(true);
    
        if (Input.GetMouseButtonUp(0))
        {
            estadoActual = EstadosSelector.Soltar;
        }
    }

    void RotateCube()
    {
        rot = Quaternion.Euler (Input.mousePosition);
        Vector3 pos = Input.mousePosition;
        Ray rayo = Camera.main.ScreenPointToRay(pos);
        RaycastHit hitinfo;
        cube.SetActive(false);
        if (Physics.Raycast(rayo, out hitinfo) == true)
        {
            xrot -= Input.GetAxis("Mouse X") * rotspeed;
            yrot += Input.GetAxis("Mouse Y") * rotspeed;
            fromRotation = transform.rotation;
            toRotation = Quaternion.Euler(yrot, xrot, 0);
            cube.transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime);
            estadoActual = EstadosSelector.Rotate;
            Debug.Log("Rotate");
        }
        cube.SetActive(true);

        if (Input.GetMouseButtonUp(0))
        {
            estadoActual = EstadosSelector.Soltar;
        }
    }
    void MoveCube()
    {
        Vector3 pos = Input.mousePosition;
        Ray rayo = Camera.main.ScreenPointToRay(pos);
        RaycastHit hitinfo;
        cube.SetActive(false);
        if (Physics.Raycast(rayo, out hitinfo) == true)
        {
            cube.transform.position = hitinfo.point + Vector3.up * cube.transform.localScale.y/2;
            estadoActual = EstadosSelector.Mover;
        }
        cube.SetActive(true);

        if (Input.GetMouseButtonUp(0))
        {
            estadoActual = EstadosSelector.Soltar;
        }
    }

    void ReleaseCube()
    {        
        LeanTween.cancel(cube);
        LeanTween.scale(cube, originalScale, 0f);
        Debug.Log("Release");
        cube = null;
        estadoActual = EstadosSelector.EnEspera;
    }
    void SelectCubeMove()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Select");
            Vector3 pos = Input.mousePosition;
            if (Application.platform == RuntimePlatform.Android)
            {
                pos = Input.GetTouch(0).position;
            }

            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitinfo;
            if (Physics.Raycast(rayo, out hitinfo) == true)
            {
                if (hitinfo.collider.tag.Equals("Cubo"))
                {
                    cube = hitinfo.collider.gameObject;
                    originalScale = cube.transform.localScale;
                    LeanTween.scale(cube, cube.transform.localScale * 1.2f, 0.75f).setEaseInBounce().setLoopPingPong();
                    estadoActual = EstadosSelector.Mover;
                }
            }
        }
    }
    void SelectCubeScale()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Select");
            Vector3 pos = Input.mousePosition;
            if (Application.platform == RuntimePlatform.Android)
            {
                pos = Input.GetTouch(0).position;
            }

            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitinfo;
            if (Physics.Raycast(rayo, out hitinfo) == true)
            {
                if (hitinfo.collider.tag.Equals("Cubo"))
                {
                    estadoActual = EstadosSelector.Escalar;
                }
            }
        }
    }
    void SelectCubeRotate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Select");
            Vector3 pos = Input.mousePosition;
            if (Application.platform == RuntimePlatform.Android)
            {
                pos = Input.GetTouch(0).position;
            }

            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitinfo;
            if (Physics.Raycast(rayo, out hitinfo) == true)
            {
                if (hitinfo.collider.tag.Equals("Cubo"))
                {
                    estadoActual = EstadosSelector.Rotate;
                }
            }
        }
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
    public void ActivarRotate()
    {
        switch (estadoActual)
        {
            case EstadosSelector.EnEspera:
                estadoActual = EstadosSelector.SeleccionarObjetoRotate;
                break;
        }
    }
    public void ActivarScale()
    {
        switch (estadoActual)
        {
            case EstadosSelector.EnEspera:
                estadoActual = EstadosSelector.SeleccionarObjetoScale;
                break;
        }
    }
}
