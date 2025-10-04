using System;
using Unity.VisualScripting;
using UnityEngine;


public class HitBox : MonoBehaviour
{

    private Attackable _target;
    public Attackable target
    {
        get { return _target; }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Attackable attackable = other.GetComponent<Attackable>();

        if (attackable != null)
        {
            _target = attackable;
        }
 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _target = null;
    }
}