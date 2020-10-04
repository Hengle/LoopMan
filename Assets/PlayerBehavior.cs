using System.Collections;
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

        if (hInput > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        
        else if(hInput <0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }

        if (vInput > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        
        else if (vInput < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }

        if(Input.GetMouseButtonDown(0))
        {
            IEnumerator attack = attackCoroutine();
            StartCoroutine(attack);
        }



    }

    IEnumerator attackCoroutine()
    {
        attackCone.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attackCone.SetActive(false);

    }
}
