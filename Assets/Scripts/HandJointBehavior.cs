using UnityEngine;

public class HandJointBehavior : MonoBehaviour
{
    [SerializeField] private Transform _controller;
    private bool _onWall;
    private Rigidbody _rb;
    private FixedJoint _joint;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _joint = _controller.GetComponent<FixedJoint>();
    }

    private void Update()
    {
        transform.rotation = _controller.rotation;
        if (!_onWall)
        {
            transform.position = _controller.position;
        }
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
