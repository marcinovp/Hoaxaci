using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform targetToFollow;

    private Vector3 localPosition;
    private Quaternion localRotation;

    private void Awake()
    {
        localPosition = targetToFollow.localPosition;
        localRotation = targetToFollow.localRotation;
    }

    void Update()
    {
        Transform originalParent = transform.parent;
        transform.parent = targetToFollow;
        transform.localPosition = localPosition;
        transform.localRotation = localRotation;

        transform.parent = originalParent;
    }
}
