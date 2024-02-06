using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public Collider coll;

    private void Awake()
    {
        coll = GetComponent<Collider>();
    }

    public void EnableWeapon()
    {
        coll.enabled = true;
    }

    public void DisableWeapon()
    {
        coll.enabled = false;
    }
}
