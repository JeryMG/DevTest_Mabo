using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{
    private Transform Target;
    
    void Start()
    {
        Target = Camera.main.transform;
    }
    
    void Update()
    {
        if (Target != null) transform.rotation = Quaternion.LookRotation(transform.position - Target.position);
    }
}
