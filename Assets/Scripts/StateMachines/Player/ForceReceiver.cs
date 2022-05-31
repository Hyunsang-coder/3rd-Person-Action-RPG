using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float drag = 0.3f;
    
    Vector3 dampingVelocity;
    Vector3 impact;
    float verticalVelocity;


    // 프로퍼티를 짧게 바꾼 형태로 아래와 같음. 
    // public Vector3 Movement {get => Vector3.up*verticalVelocity} 
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;
        
    void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y* Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
