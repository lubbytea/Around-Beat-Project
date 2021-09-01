using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    public float speed;
    Animator animator;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";


    // Physics Movement
    [SerializeField] public GameObject followObject;
    [SerializeField] public float followSpeed = 30f;
    [SerializeField] public float rotateSpeed = 100f;
    public Transform followTarget;
    public Rigidbody body;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Physics Movement
        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;

        //Teleport hands
        body.position = followTarget.position;
        body.rotation = followTarget.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
        PhysicsMove();
    }

    public void PhysicsMove()
    {
        //Position

        var distance = Vector3.Distance(followTarget.position, transform.position);
        body.velocity = (followTarget.position - transform.position).normalized * (followSpeed * distance);

        //Rotation
        Quaternion q = followTarget.rotation * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime *speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime *speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }
}
