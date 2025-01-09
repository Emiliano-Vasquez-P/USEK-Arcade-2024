using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairBossfight : MonoBehaviour
{
    public float speed, damage;
    public Transform crosshair;
    IDamageable target;
    [SerializeField] private float verticalOffset, horizontalOffset;
    public void Movement(Vector2 input)
    {
        Debug.Log(input);
        input.Normalize();
        crosshair.position += speed * (Vector3)input * Time.deltaTime;
        
        Vector2 screenRelativePosition = Camera.main.WorldToScreenPoint(crosshair.position);
        screenRelativePosition = new Vector2 (Mathf.Clamp(screenRelativePosition.x, 0+horizontalOffset, Screen.width-horizontalOffset), Mathf.Clamp(screenRelativePosition.y, 0+verticalOffset, Screen.height-verticalOffset));
        Vector3 desiredPosition = Camera.main.ScreenToWorldPoint(screenRelativePosition);
        desiredPosition.z = 0;
        crosshair.position = desiredPosition;
    }
    public void Shoot(bool value)
    {
        if(target != null && value)
        {
            target.TakeDamage(damage);
        }
    }

    public void SpecialShoot(bool value)
    {
        if (target != null && value)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            target = damageable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            if(target == damageable)
            {
                target = null;
            }
        }
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) && target != null)
        {
            Shoot(true);
        }*/
    }
}
