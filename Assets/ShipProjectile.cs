using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipProjectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float lifetime;

    public virtual void ForwardMovement()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
