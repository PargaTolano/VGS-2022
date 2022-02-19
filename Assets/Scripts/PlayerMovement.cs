using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 15f;
    // Start is called before the first frame update
    public float boxCastOffset = -0.1f;
    public LayerMask whatIsGround;
    private Rigidbody2D rb;
    private Animator animator;
    public BoxCollider2D boxCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        rb.velocity =  new Vector2( x * speed, rb.velocity.y);

        animator.SetFloat("Move", Mathf.Abs(x));

        // Voltear personaje
        Vector2 s = transform.localScale;
        if( x < 0 ){
            s.x = -Mathf.Abs(s.x);
        }else if(x > 0 ){
            s.x = Mathf.Abs(s.x);
        }
        transform.localScale = s;

        //Checar aterrizaje y animar
        bool grounded = isGrounded();
        animator.SetBool("Grounded", grounded);
        if( Input.GetKeyDown(KeyCode.Space) && grounded) {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position + Vector3.down * boxCastOffset, boxCollider.bounds.size);
    }

    public bool isGrounded() {
        var hit = Physics2D.BoxCast(
            transform.position,         //origen
            boxCollider.bounds.size,    //tamanio
            0,                          //rotacion
            Vector2.down,               //direccion de desplazamiento
            boxCastOffset,              //magnitud de desplazamiento
            whatIsGround.value          //layer de tierra
        );
        return hit.collider != null;
    }
}
