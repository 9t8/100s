using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public int contacting { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        contacting = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        ++contacting;
    }

    void OnTriggerExit(Collider other)
    {
        --contacting;
    }
}
