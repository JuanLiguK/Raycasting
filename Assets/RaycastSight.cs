using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSight : MonoBehaviour
{
    [SerializeField] Transform originTR;
    [SerializeField] float rayLenght;
    // Start is called before the first frame update
    void Start()
    {
        originTR = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(originTR.position, originTR.forward, out hitInfo, rayLenght))
        {
            Debug.Log(hitInfo.collider.name);
        }
    }


    private void OnDrawGizmos()
    {
        Color color = Color.red;
        Gizmos.color = color;
        Gizmos.DrawLine(originTR.position, originTR.position + originTR.forward * rayLenght);
    }
}


