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
    public Transform attackPos;
    public float attackRadius = 0.5f;
    public LayerMask enemies;
    public GameObject recordingPrefab;
    public bool recording = false;
    PlayheadBehavior playHead;
    public RecordingBehavior currentRecording;
    public Animator playerAnim;
    public int directionInteger = 0;
    public GameObject recordingIndicator;

    void Start()
    {
        playHead = GameObject.FindGameObjectWithTag("playHead").GetComponent<PlayheadBehavior>();
        myRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputManager();
        legacyInputSim();
    }

    private void FixedUpdate()
    {
        myRb.velocity = (movementVector * speed);
    }

    void legacyInputSim()
    {
        if(Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerAnim.SetBool("down", false);
            playerAnim.SetBool("up", false);
            playerAnim.SetBool("left", true);
            playerAnim.SetBool("right", true);
            directionInteger = 2;
            playerAnim.Play("Base Layer.hero_idle_side");
            
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerAnim.SetBool("down", false);
            playerAnim.SetBool("up", false);
            playerAnim.SetBool("left", true);
            playerAnim.SetBool("right", false);
            directionInteger = 4;
            playerAnim.Play("Base Layer.hero_idle_left");
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerAnim.SetBool("down", false);
            playerAnim.SetBool("up", true);
            playerAnim.SetBool("left", false);
            playerAnim.SetBool("right", false);
            directionInteger = 1;
            playerAnim.Play("Base Layer.hero_idle_up");
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerAnim.SetBool("down", true);
            playerAnim.SetBool("up", false);
            playerAnim.SetBool("left", false);
            playerAnim.SetBool("right", false);
            directionInteger = 3;
            playerAnim.Play("Base Layer.hero_idle_down");
        }

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
            //playerAnim.SetBool("right", true);
            //playerAnim.SetBool("left", false);
           //transform.localScale = new Vector3(1, 1, 1);
            //gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }

        else if(hInput <0)
        {
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            //playerAnim.SetBool("right", false);
            //playerAnim.SetBool("left", true);
            //transform.localScale = new Vector3(-1, 1, 1);
            //gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (vInput > 0)
        {
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //playerAnim.SetBool("up", true);
            //playerAnim.SetBool("down", false);
            //playerAnim.SetBool("right", false);
            //playerAnim.SetBool("left", false);
        }
        
        else if (vInput < 0)
        {
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            //playerAnim.SetBool("up", false);
            //playerAnim.SetBool("down", true);
            //playerAnim.SetBool("right", false);
            //playerAnim.SetBool("left", false);
        }

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            IEnumerator attack = attackCoroutine();
            StartCoroutine(attack);

            //attacking code - disabling and moving to inside coroutine
            
            RaycastHit2D hitCircle = Physics2D.CircleCast(attackPos.position, attackRadius, Vector2.up, Mathf.Infinity, enemies);
            Collider2D coll = Physics2D.OverlapCircle(attackPos.position, attackRadius, enemies);
            Debug.Log(hitCircle.collider);
            if(coll !=null)
            {
                if (coll.GetComponent<enemyBehavior>() != null)
                {
                    coll.GetComponent<enemyBehavior>().playSound(directionInteger);
                }

                if (coll.GetComponent<FreestyleEnemyBehavior>() != null)
                {
                    coll.GetComponent<FreestyleEnemyBehavior>().playSound(directionInteger);
                }
            }
            
            /*
            if(hitCircle.collider !=null)
            {
                hitCircle.collider.GetComponent<enemyBehavior>().playSound();
            }
            */

        }
        //on right click down, begin a recording
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftShift))
        {
            
            GetComponent<SpriteRenderer>().color =  new Color(1,0,0,1);
            // OLD recording code.
           
            if (!recording)
            {
                recording = true;
               // GameObject newRecordingGO = Instantiate(recordingPrefab, Vector3.zero, Quaternion.identity);
               // currentRecording = newRecordingGO.GetComponent<RecordingBehavior>();
               // currentRecording.createRecording(playHead.ticker, transform,playHead);
            }

            recording = true;
            recordingIndicator.SetActive(true);
        } else
        {
            recording = false;
            recordingIndicator.SetActive(false);
            //   if(currentRecording!=null)
            // {
            //      currentRecording.stopRecording();
            //  }
            GetComponent<SpriteRenderer>().color = Color.white;
            
        }

        



    }

    
    IEnumerator attackCoroutine()
    {
        Vector2 initialPos = transform.position;
        float offset = 0.4f;
        /*
        if(playerAnim.GetBool("right"))
        {
            transform.position = initialPos + Vector2.right * new Vector2(offset, 0);
        } else if (playerAnim.GetBool("left"))
        {
            transform.position = initialPos - Vector2.right * new Vector2(offset, 0);
        }
        else if (playerAnim.GetBool("up"))
        {
            transform.position = initialPos + Vector2.up * new Vector2(0, offset);
        }
        else if (playerAnim.GetBool("down"))
        {
            transform.position = initialPos - Vector2.up * new Vector2(0, offset);
        }
        */
        //attackCone.SetActive(true);
        switch(directionInteger)
        {
            case 1:
            playerAnim.Play("Base Layer.hero_attack_up");
                break;
            case 2:
                playerAnim.Play("Base Layer.hero_attack_right");
                break;
            case 3:
                playerAnim.Play("Base Layer.hero_attack_down");
                break;
            case 4:
                playerAnim.Play("Base Layer.hero_attack_left");
                break;

        }
        
        playerAnim.SetBool("attacking", true);
        //GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        /*
        RaycastHit2D hitCircle = Physics2D.CircleCast(attackPos.position, attackRadius, Vector2.up, Mathf.Infinity, enemies);
        Collider2D coll = Physics2D.OverlapCircle(attackPos.position, attackRadius, enemies);
        Debug.Log(hitCircle.collider);
        if (coll != null)
        {
            if (coll.GetComponent<enemyBehavior>() != null)
            {
                coll.GetComponent<enemyBehavior>().playSound(directionInteger);
            }

            if (coll.GetComponent<FreestyleEnemyBehavior>() != null)
            {
                coll.GetComponent<FreestyleEnemyBehavior>().playSound(directionInteger);
            }
        }
        */
        playerAnim.SetBool("attacking", false);
        GetComponent<BoxCollider2D>().enabled = true;
        
        //attackCone.SetActive(false);

    }
}
