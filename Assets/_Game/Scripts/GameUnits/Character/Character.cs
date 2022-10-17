using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Character : MonoBehaviour, IHit
{
    [SerializeField]
    protected Rigidbody2D myRigidbody2D;
    [SerializeField]
    protected Collider2D myCollider2D;
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    protected AnimController animController;
    [SerializeField]
    protected Transform myTransform, myFoot;
    [SerializeField]
    protected Weapon weapon;
    [SerializeField]
    protected ForceBar forceBar;
    [SerializeField]
    protected float jumpForce;
    [SerializeField]
    TextMeshProUGUI characterName;

    protected Side side;
    protected CharacterData data;
    protected bool isGrounded, isDead;
    protected float holdTime, forcePercent, currentHP;

    protected const float MAX_HOLD_TIME = 1f;
    protected readonly Vector3 MINUS_HP_OFFSET = new Vector3(0f, 1.2f, 0f);

    public float CurrentHPPercent { get { return currentHP / data.HP; } private set { } }
    public CharacterData Data { get { return data; } private set { } }

    protected void OnEnable()
    {
        GameManager.Instance.OnStateChange += OnGameStateChange;
    }

    public void InitData(CharacterData data, Side side)
    {
        this.data = data;
        this.side = side;
    }

    public virtual void OnInit()
    {
        isDead = false;
        weapon.SetActive(true);
        currentHP = data.HP;
    }

    public void ChangeName(string name, Side side, Color color)
    {
        characterName.text = name;
        characterName.color = color;
        Vector3 temp = characterName.transform.localScale;
        characterName.transform.localScale = new Vector3(Mathf.Abs(temp.x) * (int)side, temp.y, temp.z);
    }

    public virtual void OnHit(float damage)
    {
        currentHP = currentHP < damage ? 0f : currentHP - damage;

        if (Mathf.Approximately(currentHP, 0f))
        {
            OnDead();
            LevelManager.Instance.CheckEndLevel();
        }

        LevelManager.Instance.UpdateHP();
        SoundManager.Instance.PlayAudio(AudioType.hurt, 0.5f);
        SimplePool.Spawn<MinusHP>(PrefabManager.Instance.MinusHPPrefab, myTransform.position + MINUS_HP_OFFSET, myTransform.rotation).OnInit(damage);
    }

    protected void OnDead()
    {
        isDead = true;
        weapon.SetActive(false);
        animController.ChangeAnim(CharacterAnim.idle);
        forceBar.gameObject.SetActive(false);

        if (LevelManager.Instance.IsMode(GameMode.mode2v2))
        {
            OnDespawn();
        }
    }

    protected void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    protected void Jump()
    {
        isGrounded = false;
        myRigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        animController.ChangeAnim(CharacterAnim.jump);
        SoundManager.Instance.PlayAudio(AudioType.jump, 0.5f);
    }

    // On... such, use raycast instead
    protected void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(myFoot.position, Vector2.down, Mathf.Infinity, DataManager.Instance.BGLayer);

        if (hit.collider != null)
        {
            if (Mathf.Abs(hit.point.y - myFoot.position.y) < 0.1f && myRigidbody2D.velocity.y <= 0f)
            {
                isGrounded = true;
                animController.ChangeAnim(CharacterAnim.idle);
            }
        }
    }

    protected virtual void Attack(float forcePercent)
    {
        weapon.Attack(forcePercent, (int)side);
    }

    protected virtual void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                characterName.gameObject.SetActive(false);
                spriteRenderer.enabled = false;
                break;

            case GameState.Gameplay:
                characterName.gameObject.SetActive(true);
                spriteRenderer.enabled = true;
                break;

            case GameState.End:
                forceBar.gameObject.SetActive(false);
                animController.ChangeAnim(CharacterAnim.idle);
                break;

            default:
                break;
        }
    }

    protected void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChange -= OnGameStateChange;
        }
    }
}
