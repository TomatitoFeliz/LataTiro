using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAnim : MonoBehaviour
{
    public enum EstadosSelector
    {
        EnEspera,
        ObjetoSeleccionado,
        Mover,
        Escalar,
        Rotar,
    }
    [SerializeField]
    EstadosSelector estadoActual = EstadosSelector.EnEspera;


    public GameObject cube;
    Vector3 originalScale;

    private void Update()
    {
        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
        {
            if (cube == null)
            {
                SelectCube();
            }
            else
            {
                ReleaseCube();
            }
        }

        if (cube != null)
        {
            MoveCube();
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
            cube.transform.position = hitinfo.point + Vector3.up * cube.transform.localScale.y / 2;
            estadoActual = EstadosSelector.Mover;
        }
        cube.SetActive(true);
    }

    void ReleaseCube()
    {
        LeanTween.cancel(cube);
        LeanTween.scale(cube, originalScale, 0f);
        Debug.Log("Release");
        cube = null;
        estadoActual = EstadosSelector.EnEspera;
    }
    void SelectCube()
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
