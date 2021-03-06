using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
        Physics.autoSimulation = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (simulating)
        {
            Debug.Log("Simulating Colided with "+collision.gameObject.name  +" at " + ticksPassed);
        }
        else
        {
            Debug.Log("Colided at " + collision.gameObject.name + " at " + ticksPassed);

        }
    }
    public GameObject bat;
    int ticksPassed;
    public void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(0,0,2f);
   //     bat.transform.position = bat.transform.position + new Vector3(2f, 0, 0f);
        ticksPassed++;
        Physics.Simulate(Time.fixedDeltaTime);
        if (ticksPassed == 70) {
            simulateWithBatInput(ticksPassed);
        }
    }
    bool simulating;
    void simulateWithBatInput(int ticks)
    {
        simulating = true;
        transform.position = startPos;
        int i = 0;
        while (i < ticks)
        {
            i++;
            ticksPassed = i;
            bat.transform.position = bat.transform.position + new Vector3(2f, 0, 0f);
            transform.position = transform.position + new Vector3(0, 0, 2f);
            Physics.Simulate(Time.fixedDeltaTime);
        }
        ticksPassed = ticks;
        simulating = false;
    }
}
