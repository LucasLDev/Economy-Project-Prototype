using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 initialOffset;

    private void Start()
    {
        // Store the initial offset of the canvas relative to the player object
        initialOffset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        // Update the position of the canvas to be offset from the player object's position
        transform.position = playerTransform.position + initialOffset;
    }
}
