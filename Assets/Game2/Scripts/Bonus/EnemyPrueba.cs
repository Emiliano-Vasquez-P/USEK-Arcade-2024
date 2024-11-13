using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrueba : MonoBehaviour
{
    public Transform positionA, positionB, tPlayer;

    public float speed = 10;

    Transform enemyTransformPosition;

    public Vector2 direction;

    public float radio;

    public bool isRight = true;

    public bool isEnemyInZone = false;

    void Start()
    {
        enemyTransformPosition = transform;
    }

    void Update()
    {
        if (radio >= (tPlayer.position - enemyTransformPosition.position).magnitude)
        {
            isEnemyInZone = true;
        }
        else
        {
            isEnemyInZone = false;
        }
        if (isEnemyInZone)
        {
            direction = tPlayer.position - enemyTransformPosition.position;

        }
        else if (isRight)
        {
            direction = positionB.position - enemyTransformPosition.position;
        }
        else
        {
            direction = positionA.position - enemyTransformPosition.position;
        }
        if (direction.magnitude <= radio)
        {
            isRight = !isRight;
        }

        enemyTransformPosition.position += (Vector3)(direction.normalized * speed * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
