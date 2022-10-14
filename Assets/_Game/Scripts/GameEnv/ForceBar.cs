using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBar : MonoBehaviour
{
    [SerializeField]
    private Transform fillTransform;

    public void UpdateFill(float percent)
    {
        fillTransform.localScale = new Vector3(percent, 1f, 1f);
    }
}
