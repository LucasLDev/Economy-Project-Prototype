using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public FollowPlayer instance;
    private GameObject playerTransform;
    private Vector3 initialOffset;

    void Awake ()
    {
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
        playerTransform = GameObject.FindGameObjectWithTag("Player");
        // Store the initial offset of the canvas relative to the player object
        initialOffset = transform.position - playerTransform.transform.position;
    }

    private void LateUpdate()
    {
        // Update the position of the canvas to be offset from the player object's position
        transform.position = playerTransform.transform.position + initialOffset;
    }
}
