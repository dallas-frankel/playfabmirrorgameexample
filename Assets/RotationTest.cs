using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    private void Start()
    {
        Vector3 direction = Vector3.right;
        Rigidbody rb = GetComponent<Rigidbody>();
        
        Debug.Log("Mad");

        Vector3 cubePreviousVel = new Vector3(-1,-1,1).normalized;

        Vector3 torque = Vector3.Cross(cubePreviousVel, direction * -1);
                

       // rb.AddTorque(torque * 500);


        Vector3 batNormal = new Vector3(0,0,1);
        Vector3 batVel = new Vector3(-1,-1,0).normalized;

        Vector3 newTorque = Vector3.Cross(batVel,batNormal);

        rb.AddTorque(newTorque * 500);
    }
}
