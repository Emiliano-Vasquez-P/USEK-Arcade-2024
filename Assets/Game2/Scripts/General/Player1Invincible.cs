using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Invincible : MonoBehaviour
{
    [SerializeField] float invincibilityDuration = 3f;
    bool isInvincible = false;

    IEnumerator BecomeTemporarilyInvincible()
    {
        if (!isInvincible)
        {
            Debug.Log("Player turned invicible");
            isInvincible = true;

            float time = 0f;
            while (time < invincibilityDuration)
            {
                yield return new WaitForSeconds(0.1f);
                time += Time.deltaTime;
            }

            yield return new WaitForSeconds(invincibilityDuration - time);
            isInvincible = false;
            Debug.Log("Player no longer invicible");
        }
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public void BecomeInvincible(bool value)
    {
        StartCoroutine(BecomeTemporarilyInvincible());
    }
}
