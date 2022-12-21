using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public XRIDefaultInputActions Actions => _actions;
    private XRIDefaultInputActions _actions = new XRIDefaultInputActions();

    public static GameManager Instance => _instance;
    private static GameManager _instance;

    private void Start()
    {
        _instance = this;
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
