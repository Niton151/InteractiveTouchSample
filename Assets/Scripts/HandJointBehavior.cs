using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HandJointBehavior : MonoBehaviour
{
    [SerializeField] private Transform _trackedHand;
    private bool _onWall;
    private Rigidbody _rb;
    private FixedJoint _joint;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _joint = _trackedHand.GetComponent<FixedJoint>();
    }

    private void Update()
    {
        transform.rotation = _trackedHand.rotation;
        if (!_onWall)
        {
            transform.position = _trackedHand.position;
        }

        // _rb.MoveRotation(_trackedHand.rotation);
        // _rb.MovePosition(_trackedHand.position);
    }

    private void OnCollisionStay(Collision other)
    {
        _onWall = true;
        _joint.connectedBody = _rb;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    
    private void OnCollisionExit(Collision other)
    {
        _onWall = false;
        _joint.connectedBody = null;
        _rb.constraints = RigidbodyConstraints.None;
    }
}
