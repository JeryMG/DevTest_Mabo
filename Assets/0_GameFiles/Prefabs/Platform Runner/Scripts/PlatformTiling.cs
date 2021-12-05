using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTiling : MonoBehaviour
{
    public float tilingValue = 5f;
    
    void Update()
    {
        GetComponent<MeshRenderer>().materials[1].mainTextureScale = new Vector2(transform.localScale.x * tilingValue, 0f);
    }
}
