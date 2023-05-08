using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Joystick joystick;

    
    void FixedUpdate()
    {
        Vector2 direction = Vector2.one * joystick.Direction;
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }
}
