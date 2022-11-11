using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Material hitMaterial;
    public AudioClip shotSound;
    public AudioClip booSound;
    public AudioClip tinSound;
    public AudioClip welldoneSound;
    public AudioClip perfectSound;
    private AudioSource gunAudioSource;

    //HUD Puntuación:
    [SerializeField]
    TextMeshProUGUI labelpuntuacion;
    [SerializeField]
    GameObject hudpuntuacion;
    int puntuacion = 0;

    void Awake()
    {
        gunAudioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        //HUD Puntuacion:
        labelpuntuacion.text = "Puntuacion: " + puntuacion;

        //En (Input.GetMouseButtonUp(0))) el número indica la parte del botón seleccionada que va de 0-2 y de izquierda a 
        //derecha respectivamente, siendo el 1 el botón central.

        //En (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) primero comprobamos que se toca la pantalla
        //y después calculamos el tipo de toque (mantenido, pulsado al tocar, pulsado al dejar de tocar).
        if ((Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetMouseButtonUp(0)))
        {
            //Calculamos la posición del ratón
            gunAudioSource.PlayOneShot(shotSound);
            Vector3 pos = Input.mousePosition;
            if (Application.platform == RuntimePlatform.Android)
            { 
                pos = Input.GetTouch(0).position;
            }

            //Hacemos que desde la cámara salga un rayo en el lugar en el que se encuentra el ratón
            Ray rayo = Camera.main.ScreenPointToRay(pos);
            RaycastHit hitInfo;
            //out = sacar información
            if (Physics.Raycast(rayo, out hitInfo) == true)
            {
                if (hitInfo.collider.tag.Equals("Lata"))
                {
                    gunAudioSource.PlayOneShot(tinSound);
                    Rigidbody rigidbodyLata = hitInfo.collider.GetComponent<Rigidbody>();
                    rigidbodyLata.AddForce(rayo.direction * 50f, ForceMode.VelocityChange);
                    hitInfo.collider.GetComponent<MeshRenderer>().material = hitMaterial;
                    puntuacion = puntuacion + 10;
                }
                else if (hitInfo.collider.tag.Equals("Untagged"))
                {
                    gunAudioSource.PlayOneShot(booSound);
                    if (puntuacion >= 5)
                    {
                        puntuacion = puntuacion - 5;
                    }
                }
            
            }
            else if (Physics.Raycast(rayo, out hitInfo) == false)
            {
                gunAudioSource.PlayOneShot(booSound);
                if (puntuacion >= 5)
                {
                    puntuacion = puntuacion - 5;
                }
            }

            if (puntuacion == 120)
            {
                gunAudioSource.PlayOneShot(welldoneSound);
            }
            else if (puntuacion == 160)
            {
                gunAudioSource.PlayOneShot(perfectSound);
            }
        
        }
    }
    public void resetear()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
