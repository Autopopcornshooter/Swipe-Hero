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
    private bool invincibility;
    private Animator playerAnimator;


    private PlayerHPCtrl playerHPCtrl;
    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        playerHPCtrl = GetComponent<PlayerHPCtrl>();
    }
    private void Start()
    {
    }


    private void PlayerGetHItCheck()
    {
        //Debug.Log("PlayerGetHit");
        if (!invincibility)
        {
            playerHPCtrl.PlayerDamaged();
            GameScoreCheck.BonusComboDecrease();
        }
        playerAnimator.SetTrigger("GetHit");
        GameSound.Instance().ShootPlayerSound2(GameSound.Instance().playerHit);
        GameScoreCheck.currentCombo = 0;

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
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            if (!PlayerHPCtrl.isDead)
            {
                sfx.CameraShake();

                PlayerGetHItCheck();
                if (PlayerHPCtrl.s_currentHP <= 0)
                {
                    return;
                }
            }
        }
    }
}
