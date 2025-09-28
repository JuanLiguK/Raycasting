using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastSight : MonoBehaviour
{

    [SerializeField] Transform originTR;
    [SerializeField] float rayLength;

    // Start is called before the first frame update
    void Start()
    {
        originTR = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //lo primero que necesitamos es crear una variable de raycasthit que por defecto su nombre es hit --> almacena los valores que me devuelve el raycast cuando apuntoa a un collider

        RaycastHit hitInfo;
        if (Physics.Raycast(originTR.position, originTR.forward, out hitInfo, rayLength))
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                Debug.Log("Jugador detectado");
            }
        }
    }

    void OnDrawGizmos() // ESPECIE DE UPDATE que esta todo el tiempo ejecutandose, gizmos son ayudas visuales que tiene unity
    {
        Color color = Color.red;
        Gizmos.color = color;
        Gizmos.DrawLine(originTR.position, originTR.position + originTR.forward * rayLength);
    }

}