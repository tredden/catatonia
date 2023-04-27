using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
 
    public float speed;
    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer sprite;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        sprite = GetComponent<SpriteRenderer> ();
    }
 
    void Update()
    {
        
        float moveHorizontal;
        if(Input.GetButtonDown("Fire1")){
            anim.SetTrigger("doAttack");
        }
        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
            moveHorizontal = 0;
        } else {
            moveHorizontal = Input.GetAxis ("Horizontal");
            if (moveHorizontal > 0){
                moveHorizontal = 1;
            } else if(moveHorizontal < 0){
                moveHorizontal = -1;
            } else {
                moveHorizontal = 0;
            }
        }

        float newx = rb2d.velocity.x;
        float newy = rb2d.velocity.y;

        //Debug.Log(moveHorizontal);
        if (moveHorizontal==0){
            newx = 0;
        } else {
            newx += moveHorizontal*speed;
        }
        
        if(-0.001 > rb2d.velocity.x || rb2d.velocity.x > 0.001){
            anim.SetBool("isRunning",true);
        } else {
            anim.SetBool("isRunning",false);
        }

        
        rb2d.velocity = new Vector2(Mathf.Clamp(newx,-2,2),0);
        if (rb2d.velocity.x > 0) {
            sprite.flipX = false;
            FlipBox(false);
        } else if (rb2d.velocity.x < 0) {
            sprite.flipX = true;
            FlipBox(true);
        }

        float moveVertical = Input.GetAxis ("Vertical");
 
        if (moveVertical > 0)
        {
            rb2d.velocity += new Vector2(0,5);
        }    

        
        //Debug.Log(moveVertical);
        // Try out this delta time method??
        //rb2d.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
    }

    void FlipBox(bool flip){
        int flipper = (flip ? -1 : 1);
        foreach (Transform child in transform){
            if (child.transform.tag == "Hitbox"){
                child.transform.localPosition = new Vector2(flipper * Mathf.Abs(child.transform.localPosition.x),child.transform.localPosition.y);
            }
        }
    }
 
    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
    }
}
