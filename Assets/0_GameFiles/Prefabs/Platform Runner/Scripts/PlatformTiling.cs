using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTiling : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().materials[1].mainTextureScale = new Vector2(transform.localScale.x * 5f, 0f);
    }
}
