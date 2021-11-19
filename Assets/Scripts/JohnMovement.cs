using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public Joystick joystick;
    public GameObject BulletPrefab;
    public float Speed;
    public float Jumpforce;
    private bool Grounded;
    bool voltearPlayer = true;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
     
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        Horizontal = joystick.Horizontal;

        if (Horizontal < 0.2f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.2f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);



        Animator.SetBool("Running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 3.0f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 3.0f))
        {
            Grounded = true;
        }
        else Grounded = false;


     
    }

  

     

    public void Jump()
    {
        //if (Input.GetButtonDown ("Jump")&& Grounded)

        {
            Rigidbody2D.AddForce(Vector2.up * Jumpforce);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal*Speed, Rigidbody2D.velocity.y);
        
    }

    public void Shoot()
    {
        //if (Input.GetButtonDown ("shoot"))
      
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;


        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }
}
