using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinusHP : GameUnit
{
    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private Transform myTransform;
    [SerializeField]
    private AnimController animController;

    private float timer;
    private Vector3 fadeDes;

    private const float FADE_TIME = 1f;

    private static readonly Vector3 FADE_OFFSET = new Vector3(0f, 1f, 0f);

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            myTransform.position = Vector3.Lerp(myTransform.position, fadeDes, Time.deltaTime);
            if (timer < 0)
            {
                SimplePool.Despawn(this);
            }
        }
    }

    public void OnInit(float hp)
    {
        timer = FADE_TIME;
        fadeDes = myTransform.position + FADE_OFFSET;
        hpText.text = "-" + hp.ToString();
        animController.ChangeAnim(MinusHPAnim.play);
    }
}
