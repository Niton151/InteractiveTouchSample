using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class HandBehavior : MonoBehaviour
{
    [SerializeField] private Hand _hand = Hand.Left;
    [SerializeField] private GameObject _trackedHand;
    [SerializeField] private Transform _direction;
    [SerializeField] private float _boxSize = 1.0f;
    [SerializeField] private float _rayDistance = 2.0f;
    [SerializeField] private float _torelance = 0.1f;
    [SerializeField] private float _smooth = 0.01f;
    private XRIDefaultInputActions _actions;
    private UnityAction _updateAction;

    //test
    private RaycastHit _hit;
    private bool _isHit;
    private bool _onWall;
    private bool _inWall;

    public enum Hand
    {
        Left,
        Right
    }

    private void Start()
    {
        _actions = GameManager.Instance.Actions;

        _updateAction = UpdatePosition;
    }

    private void Update()
    {
        _updateAction?.Invoke();
    }

    private void UpdatePosition()
    {
        transform.position = _trackedHand.transform.position;
        transform.rotation = _trackedHand.transform.rotation;


        _isHit = Physics.Raycast(_direction.position, _direction.forward, out _hit);
        if (_isHit && _hit.distance <= _torelance)
        {
            _updateAction = OnWallUpdate;
            _preTrackedPos = _trackedHand.transform.position;
            Debug.Log("OnWallUpdate");
        }
    }

    private Vector3 _preTrackedPos;
    private Vector3 _normal;

    private void OnWallUpdate()
    {
        var trackedDelta = _trackedHand.transform.position - _preTrackedPos;

        transform.position += trackedDelta;

        if (_inWall)
        {
            transform.position += _hit.normal * _smooth;
        }
        else
        {
            if (Vector3.Distance(transform.position, _trackedHand.transform.position) == 0f)
            {
                _updateAction = UpdatePosition;
                Debug.Log("UpdatePosition");
                return;
            }
        }

        _isHit = Physics.Raycast(_direction.position, _direction.forward, out _hit);
        if (_hit.normal != Vector3.zero) _normal = _hit.normal;
        if (_isHit)
        {
            if ((_hit.point - transform.position).magnitude > _torelance)
            {
                transform.position -= _hit.normal * _smooth;
            }
        }

        _preTrackedPos = _trackedHand.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        _inWall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _inWall = false;
    }

    void OnDrawGizmos()
    {
        RaycastHit hit;
        var scale = _boxSize;

        if (_isHit)
        {
            Gizmos.DrawRay(_direction.position, _direction.forward * _hit.distance);
        }
        else
        {
            Gizmos.DrawRay(_direction.position, _direction.forward * 100);
        }
    }
}