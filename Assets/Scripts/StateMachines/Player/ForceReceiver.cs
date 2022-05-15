using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    float verticalVelocity;

    // 프로퍼티를 짧게 바꾼 형태로 아래와 같음. 
    // public Vector3 Movement {get => Vector3.up*verticalVelocity} 
    public Vector3 Movement => Vector3.up * verticalVelocity;
        
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
    }
}
