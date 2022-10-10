using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IHit
{
    [SerializeField]
    protected Rigidbody2D myRigidbody2D;
    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    protected AnimController animController;
    [SerializeField]
    protected Weapon weapon;
    [SerializeField]
    protected ForceBar forceBar;
    [SerializeField]
    protected float jumpForce;

    protected float currentHP;
    protected CharacterData data;
    protected bool isGrounded;
    protected float holdTime, forcePercent;
    protected const float MAX_HOLD_TIME = 1f;

    public CharacterData Data { get { return data; } set { data = value; } }

    protected void OnEnable()
    {
        GameManager.Instance.OnStateChange += OnGameStateChange;
    }

    public virtual void OnInit()
    {
        currentHP = data.HP;
        weapon.OnInit();
    }

    protected virtual void OnDespawn()
    {

    }

    public virtual void OnHit(float damage)
    {
        SoundManager.Instance.PlayAudio(AudioType.hurt, 0.5f);
        currentHP = currentHP - damage < 0 ? 0 : currentHP - damage;
    }

    protected void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                spriteRenderer.enabled = false;
                break;

            case GameState.Gameplay:
                spriteRenderer.enabled = true;
                break;

            case GameState.Lose:
            case GameState.Win:
            case GameState.Draw:
                forceBar.gameObject.SetActive(false);
                animController.ChangeAnim(CharacterAnim.idle);
                break;

            default:
                break;
        }
    }

    protected void Jump()
    {
        isGrounded = false;
        myRigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        animController.ChangeAnim(CharacterAnim.jump);
        SoundManager.Instance.PlayAudio(AudioType.jump, 0.5f);
    }

    protected virtual void Attack(float forcePercent, int side)
    {

    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constant.TAG_GROUND))
        {
            isGrounded = true;
            animController.ChangeAnim(CharacterAnim.idle);
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
