using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField]
    private Rigidbody2D myRigidbody2D;
    [SerializeField]
    private Transform myTransform;

    private WeaponData data;

    public void OnInit(WeaponData weaponData, float forcePercent, int side)
    {
        this.data = weaponData;
        myTransform.localScale = Vector3.Scale(myTransform.localScale, new Vector3(side, 1f, 1f));

        Vector2 flyForce = Vector2.Scale(weaponData.MinFlyForce + weaponData.HoldFlyForce * forcePercent, new Vector2(side, 1f));
        float spinForce = (weaponData.MinSpinForce + weaponData.HoldSpinForce * forcePercent) * Time.deltaTime * side;

        myRigidbody2D.AddForce(flyForce, ForceMode2D.Impulse);
        myRigidbody2D.AddTorque(spinForce, ForceMode2D.Impulse);
    }

    private void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constant.TAG_CHARACTER) || collision.gameObject.CompareTag(Constant.TAG_COLLECTOR))
        {
            IHit hit = Cache<IHit>.Get(collision.collider);
            if (hit != null) hit.OnHit(data.Damage);
            OnDespawn();
        }
        else
        {
            SoundManager.Instance.PlayAudio(AudioType.collision, 0.5f);
        }
    }
}
