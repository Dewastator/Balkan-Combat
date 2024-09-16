using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public bool isGrounded { get; private set; }

    [SerializeField]
    private Transform groundCheck;

    public LayerMask groundMask;
   
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.1f, groundMask);
    }
}
