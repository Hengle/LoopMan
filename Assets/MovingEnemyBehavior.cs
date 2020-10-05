using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayheadBehavior playHead;
    int count = 0;
    List<Vector3> possiblePositions;
    public Vector3[] directions;
    public float attackRadius = 0.2f;
    public LayerMask wallsnenemies;
    Vector3 targetDirection;
    float speed = 5f;
    bool running = true;

    private void OnEnable()
    {
        MyEventSystem.track2Hit += stopEnemy;
    }

    private void OnDisable()
    {
        MyEventSystem.track2Hit -= stopEnemy;
    }
    void Start()
    {
        possiblePositions = new List<Vector3>();
        playHead = GameObject.FindGameObjectWithTag("playHead").GetComponent<PlayheadBehavior>();
        targetDirection = transform.position;
    }

    void stopEnemy(int i)
    {
        Destroy(GetComponent<MovingEnemyBehavior>());
    }
    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            if (playHead.ticker == 1)
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
      
    }

    void moveInRandomDirection()
    {

        foreach( Vector3 dir in directions)
        {
            Collider2D coll = Physics2D.OverlapCircle(transform.position + dir, attackRadius, wallsnenemies);
            if(coll ==null)
            {
                possiblePositions.Add(transform.position + dir);
                
            }
        }

        int randomIndex = Random.Range(0, possiblePositions.Count - 1);
        targetDirection = possiblePositions[randomIndex];
        possiblePositions = new List<Vector3>();



    } 

    void moveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetDirection, speed * Time.deltaTime);
    }
}
