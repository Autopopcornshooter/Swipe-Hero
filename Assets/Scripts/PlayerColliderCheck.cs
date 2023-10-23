using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderCheck : MonoBehaviour
{
    [Header("Value")]
    [SerializeField]
    private float blinkTerm = 0.5f;
    [Header("Reference")]
    [SerializeField]
    private GameObject playerCollider;
    [SerializeField]
    private SFX sfx;
    [Header("Developer Mode")]
    [SerializeField]
    private bool uinvincibility;
    private Animator playerAnimator;

    private int playerHP = 3;

    static public bool isDead = false;
    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        PlayerInitiate();
    }
    private void PlayerInitiate()
    {
        playerHP = 3;
        isDead = false;
    }

    private void PlayerGetHIt()
    {
        //Debug.Log("PlayerGetHit");
        playerAnimator.SetTrigger("GetHit");
        GameSound.Instance().ShootPlayerSound2(GameSound.Instance().playerHit);
        GameManager.Instance().currentCombo = 0;
        if (playerHP == 2)
        {
            StartCoroutine(CollisionBoxBlink());
        }
        if (playerHP == 1)
        {
            StopAllCoroutines();
            playerCollider.GetComponent<SpriteRenderer>().color = Color.clear;
        }
    }
    IEnumerator CollisionBoxBlink()
    {

        SpriteRenderer playerSprite = playerCollider.GetComponent<SpriteRenderer>();
        Color coll_color = playerSprite.color;
        while (true)
        {
            if (playerSprite.color == coll_color)
            {
                playerSprite.color = Color.clear;
            }
            else if (playerSprite.color == Color.clear)
            {
                playerSprite.color = coll_color;
            }
            yield return new WaitForSeconds(blinkTerm);
        }

    }
    private void PlayerDead()
    {
        playerAnimator.SetTrigger("Dead");
        GameSound.Instance().ShootPlayerSound2(GameSound.Instance().playerDead);
        isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            if (!isDead)
            {
                if (!uinvincibility)
                {
                    playerHP--;
                }
                sfx.CameraShake();
                if (playerHP <= 0)
                {
                    PlayerDead();
                    return;
                }
                PlayerGetHIt();
            }
        }
    }
}
