using System;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float positionOffsetY;
    [SerializeField] private bool runOnce;
    private void Awake()
    {
       
    }

    private void LateUpdate()
    {
        float precisionMulti = 5f;
        spriteRenderer.sortingOrder = (int)(precisionMulti * -1 * (transform.position.y+ positionOffsetY));

        if (runOnce)
        {
            Destroy(this);
        }
    }
}
