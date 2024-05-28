

using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameInputCtrl : PanelCtrl
{
    [Header("IndicatorCtrl")]
    [SerializeField]
    private GameObject combo_Indicator;
    [SerializeField]
    private Ease ease;
    [Header("Reference")]
    [SerializeField]
    private GameObject playerPhysics;
    [SerializeField]
    private SFX SFX;
    [SerializeField]
    private HitPoint HitPoint_Up;
    [SerializeField]
    private HitPoint HitPoint_Down;
    [SerializeField]
    private HitPoint HitPoint_Left;
    [SerializeField]
    private HitPoint HitPoint_Right;



    // Start is called before the first frame update
    private void Awake()
    {
    }
    private void Start()
    {

        SFX.ChangeColor_BG();
    }
    private void IndicatorSFX()
    {
        combo_Indicator.GetComponent<RectTransform>().DOShakeScale(0.1f);
        combo_Indicator.GetComponent<RectTransform>().DOJump(new Vector2(0, 0), 0.2f, 1, 0.5f);
    }

    protected override void SwipeDTU()
    {
        UpperAttack();
        RandomAttackSound();
        IndicatorSFX();
    }
    protected override void SwipeUTD()
    {
        LowerAttack();
        RandomAttackSound();
        IndicatorSFX();
    }
    protected override void SwipeRTL()
    {
        LeftAttack();
        RandomAttackSound();
        IndicatorSFX();
    }
    protected override void SwipeLTR()
    {
        RightAttack();
        RandomAttackSound();
        IndicatorSFX();
    }
    private void RandomAttackSound()
    {
        int randNum = Random.Range(0, 2);
        if (randNum == 0)
        {
            GameSound.Instance().ShootPlayerSound1(GameSound.Instance().playerAttack1);
        }
        else if (randNum == 1)
        {
            GameSound.Instance().ShootPlayerSound1(GameSound.Instance().playerAttack2);
        }
    }
    private void RightAttack()
    {
        playerPhysics.GetComponent<Animator>().SetTrigger("Horizontal Attack");
        playerPhysics.GetComponent<SpriteRenderer>().flipX = false;
        SFX.SwipeEffect(Vector2.right);
        SFX.ChangeColor_BG();
        StartCoroutine(SFX.InstantiateSFX(Vector2.right));
        StartCoroutine(HitPoint_Right.ActivateHitPoint());
    }
    private void LeftAttack()
    {
        playerPhysics.GetComponent<Animator>().SetTrigger("Horizontal Attack");
        playerPhysics.GetComponent<SpriteRenderer>().flipX = true;
        SFX.SwipeEffect(Vector2.left);
        SFX.ChangeColor_BG();
        StartCoroutine(SFX.InstantiateSFX(Vector2.left));
        StartCoroutine(HitPoint_Left.ActivateHitPoint());
    }
    private void UpperAttack()
    {
        playerPhysics.GetComponent<Animator>().SetTrigger("Upper Attack");
        SFX.SwipeEffect(Vector2.up);
        SFX.ChangeColor_BG();
        StartCoroutine(SFX.InstantiateSFX(Vector2.up));
        StartCoroutine(HitPoint_Up.ActivateHitPoint());
    }
    private void LowerAttack()
    {
        playerPhysics.GetComponent<Animator>().SetTrigger("Lower Attack");
        SFX.SwipeEffect(Vector2.down);
        SFX.ChangeColor_BG();
        StartCoroutine(SFX.InstantiateSFX(Vector2.down));
        StartCoroutine(HitPoint_Down.ActivateHitPoint());
    }



    private void Update()
    {
        if (!PlayerHPCtrl.isDead && GameManager.Instance().isGameRunning)
        {
            ReceiveInput();
        }
    }
}
