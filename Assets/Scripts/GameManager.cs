using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public XRIDefaultInputActions Actions => _actions;
    private XRIDefaultInputActions _actions;

    public static GameManager Instance => _instance;
    private static GameManager _instance;

    private void Awake()
    {
        _instance = this;
        _actions =  new XRIDefaultInputActions();
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
