﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D myRb;
    public float speed;
    Vector3 movementVector;
    Vector3 targetPos = Vector3.zero;
    public GameObject attackCone;
    public Transform attackPos;
    public float attackRadius = 0.5f;
    public LayerMask enemies;

    public Animator playerAnim;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputManager();
    }

    private void FixedUpdate()
    {
        myRb.velocity = (movementVector * speed);
    }

    void inputManager()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        movementVector = new Vector3(hInput, vInput,0);
        Vector3 targetPos = transform.position + movementVector * speed * Time.deltaTime;
        playerAnim.SetFloat("haxis", hInput);
        playerAnim.SetFloat("vaxis", vInput);

        if (hInput > 0)
        {
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            playerAnim.SetBool("right", true);
            playerAnim.SetBool("left", false);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }

        else if(hInput <0)
        {
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            playerAnim.SetBool("right", false);
            playerAnim.SetBool("left", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (vInput > 0)
        {
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            playerAnim.SetBool("up", true);
            playerAnim.SetBool("down", false);
            playerAnim.SetBool("right", false);
            playerAnim.SetBool("left", false);
        }
        
        else if (vInput < 0)
        {
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            playerAnim.SetBool("up", false);
            playerAnim.SetBool("down", true);
            playerAnim.SetBool("right", false);
            playerAnim.SetBool("left", false);
        }

        if(Input.GetMouseButtonDown(0))
        {
            IEnumerator attack = attackCoroutine();
            StartCoroutine(attack);

            RaycastHit2D hitCircle = Physics2D.CircleCast(attackPos.position, attackRadius, Vector2.up, Mathf.Infinity, enemies);
            Debug.Log(hitCircle.collider);
            if(hitCircle!=null)
            {
                hitCircle.collider.GetComponent<enemyBehavior>().playSound();
            }
        }



    }

    IEnumerator attackCoroutine()
    {
        attackCone.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attackCone.SetActive(false);

    }
}
