using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordAnimation : MonoBehaviour
{
    [SerializeField] Vector2 tama�oNuevo;
    [SerializeField] float aparicion;
    void Start()
    {
        transform.LeanScale(tama�oNuevo, aparicion).setEaseOutQuart().setLoopPingPong();
    }

}
