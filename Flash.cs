using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material WhiteFlash;
    [SerializeField] private float restoreDefualtMatTime = .2f;

    private Material defaultMat;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public float GetRestoreMatTime()
    {
        return restoreDefualtMatTime;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = WhiteFlash;
        yield return new WaitForSeconds(restoreDefualtMatTime);
        spriteRenderer.material = defaultMat;
    }
   
}
