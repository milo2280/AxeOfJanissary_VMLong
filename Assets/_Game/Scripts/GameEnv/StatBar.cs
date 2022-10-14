using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [SerializeField]
    private Transform myTransform, hpFill, mpFill;
    [SerializeField]
    private Image avatar;

    private Character character;

    private void OnEnable()
    {
        LevelManager.Instance.OnHPChange += UpdateHP;
    }

    public void OnInit(Character character, float scale)
    {
        if (this.character != character)
        {
            this.character = character;
            avatar.sprite = character.Data.Avatar;
        }

        UpdateHP();
        Vector3 temp = myTransform.localScale;
        myTransform.localScale = new Vector3(Mathf.Sign(temp.x), 1f, 1f) * scale;
    }

    private void UpdateHP()
    {
        hpFill.localScale = new Vector3(character.CurrentHPPercent, 1f, 1f);
    }

    private void OnDisable()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnHPChange -= UpdateHP;
        }
    }
}
