using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPCtrl : MonoBehaviour
{
    [Header("Value")]
    [SerializeField]
    private float MaxHP;
    [SerializeField]
    private float hitDamage;
    [SerializeField]
    private float HP_Decrease_rate;
    [SerializeField]
    private float HP_Increase_rate;
    [Header("Referenced")]
    [SerializeField]
    private ParticleSystem healedParticle;

    static public float s_MaxHP;
    static public float s_currentHP;
    static public bool isDead = false;

    static private float s_HP_Decrease_rate;
    static private float s_HP_Increase_rate;
    static private ParticleSystem s_healedParticle;
    static private Animator playerAnimator;
    // Start is called before the first frame update
    void Awake()
    {
        s_MaxHP = MaxHP;
        s_HP_Decrease_rate = HP_Decrease_rate;
        s_HP_Increase_rate = HP_Increase_rate;
        s_healedParticle = healedParticle;
        playerAnimator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        Initiate();
    }
    private void Initiate()
    {
        s_currentHP = MaxHP;
        isDead = false;
    }
    static public void HP_Decrease()
    {
        if (GameProcess.s_BTime_Remain <= 0)
        {
            s_currentHP -= s_HP_Decrease_rate;
            if (s_currentHP <= 0)
            {
                PlayerDead();
            }
        }
    }
    static public void HP_Increase()
    {
        if (GameProcess.s_BTime_Remain <= 0)
        {
            s_healedParticle.Play();
            s_currentHP += s_HP_Increase_rate;
        }
    }
    public void PlayerDamaged()
    {
        if (GameProcess.s_BTime_Remain <= 0)
        {
            s_currentHP -= hitDamage;
            if (s_currentHP <= 0)
            {
                PlayerDead();
            }
        }
    }
    static public void PlayerDead()
    {
        isDead = true;
        GameSound.Instance().ShootPlayerSound2(GameSound.Instance().playerDead);
        playerAnimator.SetTrigger("Dead");
        if (GameManager.Instance().isGameRunning)
        {
            GameManager.Instance().GameEnd();
        }
    }
}
