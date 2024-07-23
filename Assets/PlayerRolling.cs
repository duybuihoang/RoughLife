using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRolling : MonoBehaviour
{
    [SerializeField] protected bool isRolling = false;

    private void FixedUpdate()
    {
        this.Rolling();
    }

    protected virtual void Rolling()
    {
        if (!this.isRolling) return;
        Debug.Log("Rolling");
    }   
}
