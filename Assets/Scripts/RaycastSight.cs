using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayCastSight : MonoBehaviour
{


    [SerializeField] Transform originTR;
    [SerializeField] float rayLength;


   
    void Start()
    {
        originTR = GetComponent<Transform>();
    }


    
    void Update()
    {
       


        RaycastHit hitInfo;
        if (Physics.Raycast(originTR.position, originTR.forward, out hitInfo, rayLength))
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                Debug.Log("Jugador detectado");
            }
        }
    }


    void OnDrawGizmos()
    {
        Color color = Color.red;
        Gizmos.color = color;
        Gizmos.DrawLine(originTR.position, originTR.position + originTR.forward * rayLength);
    }


}



