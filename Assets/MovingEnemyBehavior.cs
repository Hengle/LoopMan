using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayheadBehavior playHead;
    public bool keepActiveForMovingEnemy = true;
    int count = 0;
    List<Vector3> possiblePositions;
    public Vector3[] directions;
    public float attackRadius = 0.2f;
    public LayerMask wallsnenemies;
    Vector3 targetDirection;
    float speed = 5f;
    public bool running = true;
    private enemyBehavior myEnemy;
    public bool escaping = true;

    private void OnEnable()
    {
        MyEventSystem.recFail += startEnemy;
    }

    private void OnDisable()
    {
        MyEventSystem.recFail -= startEnemy;
    }
    void Start()
    {
        possiblePositions = new List<Vector3>();
        playHead = GameObject.FindGameObjectWithTag("playHead").GetComponent<PlayheadBehavior>();
        targetDirection = transform.position;
    }


    public void startEnemy(int i)
    {
        if (keepActiveForMovingEnemy)
        {
            myEnemy = GetComponent<enemyBehavior>();
            if (i == myEnemy.trackNumber)
            {
                moveInRandomDirection();
                escapeToTarget();
                escaping = true;
            }
        }
        
    }
    public void stopEnemy(int i)
    {
        if (keepActiveForMovingEnemy)
        {
            if (!escaping)
            {
                running = false;
            }
        }
        //Destroy(GetComponent<MovingEnemyBehavior>());
    }
    // Update is called once per frame
    void Update()
    {
        if (keepActiveForMovingEnemy)
        {
            if (running)
            {
                if (playHead.ticker == 2)
                {
                    count++;
                }

                if (count == 1)
                {
                    moveInRandomDirection();
                    count = 0;
                }

                moveToTarget();
            }

            if (escaping)
            {
                escapeToTarget();
            }
        }
      
    }

    void moveInRandomDirection()
    {
        if (keepActiveForMovingEnemy)
        {
            foreach (Vector3 dir in directions)
            {
                Collider2D coll = Physics2D.OverlapCircle(transform.position + dir, attackRadius, wallsnenemies);
                if (coll == null)
                {
                    possiblePositions.Add(transform.position + dir);

                }
            }

            int randomIndex = Random.Range(0, possiblePositions.Count - 1);
            targetDirection = possiblePositions[randomIndex];
            possiblePositions = new List<Vector3>();

        }

    } 

    void moveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetDirection, speed * Time.deltaTime);

    }

    void escapeToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetDirection, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetDirection) < 0.2f)
        {
            escaping = false;
            running = true;
        }
    }
}

