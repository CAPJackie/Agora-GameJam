﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour {
	static Animator anim; 
	
	public float speed = 20.0F;
	public float rotationSpeed = 100.0F;
    private bool isDead;
    public Transform respawn;
    private bool hasFlash;
	
	
	//Jump Variables
	
	public float jumpForce = 2.0F;
	public Vector3 jumpVector;
	public bool isGrounded;
	Rigidbody rb;
	
	// Use this for initialization
	void Start () 
	{
        hasFlash = true;
        isDead = false;
		anim= GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		jumpVector = new Vector3(0.0F,2.0F,0.0F);
	}
	
	void OnCollisionStay(){
		isGrounded = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		float translation = Input.GetAxis("Vertical") * speed;
		float straffe = Input.GetAxis("Horizontal") * speed;
		translation*=Time.deltaTime;
		straffe*=Time.deltaTime;
		
		transform.Translate(straffe,0,translation);
        if (Input.GetButtonDown("Fire3") && hasFlash) {
            transform.position += new Vector3(0, 0, 10);
            //hasFlash = false;
        }
        
		if(Input.GetButtonDown("Jump") && isGrounded){
			rb.AddForce(jumpVector * jumpForce, ForceMode.Impulse);
			anim.SetTrigger("isJumping");
			isGrounded=false;
		}
		if(translation!=0 || straffe!=0){
			anim.SetBool("isRunning",true);
			anim.SetBool("isIdle",false);
		}
		else{
			anim.SetBool("isRunning",false);
			anim.SetBool("isIdle",true);
		}

		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Killzone" && !isDead)
        {
            this.transform.position = respawn.position;
        }
    
    }
}
