using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public static FollowPlayer instance;
    private GameObject playerTransform;
    private Vector3 initialOffset;

    void Awake ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player");

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // Store the initial offset of the canvas relative to the player object
        initialOffset = transform.position - playerTransform.transform.position;
    }

    private void LateUpdate()
    {
        // Update the position of the canvas to be offset from the player object's position
        transform.position = playerTransform.transform.position + initialOffset;
    }
}
