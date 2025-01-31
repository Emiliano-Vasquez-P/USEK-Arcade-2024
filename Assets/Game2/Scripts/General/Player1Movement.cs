
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float speed, scaleSpeed, shipRotationSpeed, playerRotationSpeed;
    public Transform player, spaceship, background;

    SpriteRenderer sr;
    Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void Movement(Vector2 input)
    {
        float x = input.x;
        Vector3 dir = new(x, 0);

        dir.Normalize();
        player.position += speed * Time.deltaTime * dir;

        BackgroundScale(input.y);
        SpaceshipRotate(input.x);
    }

    void BackgroundScale(float verticalAxis)
    {
        Vector3 scaleChange = new(scaleSpeed, scaleSpeed, scaleSpeed);
        Vector3 maxScale = new(2.5f, 2.5f, 2.5f);
        Vector3 minScale = Vector3.one;

        if (verticalAxis > 0)
        {
            background.localScale += scaleChange;
            background.localScale = new Vector3(Mathf.Min(background.localScale.x, maxScale.x), Mathf.Min(background.localScale.y, maxScale.y), Mathf.Min(background.localScale.z, maxScale.z));
        }
        else if (verticalAxis < 0)
        {
            background.localScale -= scaleChange;
            background.localScale = new Vector3(Mathf.Max(background.localScale.x, minScale.x), Mathf.Max(background.localScale.y, minScale.y), Mathf.Max(background.localScale.z, minScale.z));
        }
    }

    void SpaceshipRotate(float horizontalAxis)
    {
        if (horizontalAxis < 0)
        {
            player.Rotate(Vector3.forward * playerRotationSpeed * Time.deltaTime);
            spaceship.Rotate(Vector3.forward * shipRotationSpeed * Time.deltaTime);
        }
        else if (horizontalAxis > 0)
        {
            player.Rotate(-Vector3.forward * playerRotationSpeed * Time.deltaTime);
            spaceship.Rotate(-Vector3.forward * shipRotationSpeed * Time.deltaTime);
        }
    }
}