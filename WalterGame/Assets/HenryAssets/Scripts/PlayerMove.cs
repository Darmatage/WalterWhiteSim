using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update

    public float walkVel = 2;
    
    
    private Rigidbody2D rb;
    private Vector2 movement;

    public Animator animator;
    private float prevY;
    private bool prevUp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (prevY > 0) {
            prevUp = true;
        }
        if (prevY < 0) {
            prevUp = false;
        }
        float speed = walkVel;
        
        movement = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        if (movement.magnitude > speed) {
            movement = movement.normalized * speed;
        }
        rb.velocity = movement;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("idle", (movement.x == 0 && movement.y == 0));
        animator.SetBool("prevUp", prevUp);
        animator.SetBool("isHor", movement.x != 0);
        prevY = movement.y;
        
    }
}
