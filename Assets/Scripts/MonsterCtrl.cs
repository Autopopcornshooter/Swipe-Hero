
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    
    public float monsterSpeed = 3.0f;
    private Collider2D monsterCollider;
    private SpriteRenderer monsterRenderer;
    private Rigidbody2D monsterRigidbody;
    private Vector2 movedir;
    private void Awake()
    {
        monsterCollider = GetComponentInChildren<Collider2D>();
        monsterRenderer = GetComponentInChildren<SpriteRenderer>();
        monsterRigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        SetMonsterMovement();
    }
    private void SetMonsterMovement()
    {
        movedir = -(transform.position);
        if (movedir.x > 0)
        {
            monsterRenderer.flipX = true;
        }
        else
        {
            monsterRenderer.flipX = false;
        }
    }
    private void Update()
    {
        if (!GameManager.Instance().isGameRunning)
        {
            monsterRigidbody.velocity = Vector2.zero;
            monsterCollider.enabled = false;
        }
        else
        {
            monsterRigidbody.velocity = movedir.normalized * monsterSpeed;
            monsterCollider.enabled = true;
        }
    }
    public void HitPlayer()
    {
        monsterCollider.enabled = false;
        Destroy(gameObject);
    }
    public void MonsterKilled()
    {
        monsterCollider.enabled = false;
        GameSound.Instance().ShootMonsterSound(GameSound.Instance().monsterDead);
        GameScoreCheck.killScore++;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HitPlayer();
        }
        else if (collision.gameObject.tag == "HitPoint")
        {
            PlayerHPCtrl.HP_Increase();
            MonsterKilled();
        }
    }
}
