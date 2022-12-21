using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandBehavior : MonoBehaviour
{
    private XRIDefaultInputActions _actions;

    private void Start()
    {
        _actions = GameManager.Instance.Actions;
        
    }
}