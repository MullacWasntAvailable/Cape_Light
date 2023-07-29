using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparantDetection : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] private float transparancyAmount = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;


    private SpriteRenderer spriteRenderer;
    private Tilemap tileMap;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileMap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (spriteRenderer)
            {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparancyAmount));
            }
            else if (tileMap)
            {
                StartCoroutine(FadeRoutine(tileMap, fadeTime, tileMap.color.a, transparancyAmount));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (spriteRenderer)
        {
            StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 1f));
        }
        else if (tileMap)
        {
            StartCoroutine(FadeRoutine(tileMap, fadeTime, tileMap.color.a, 1f));
        }
    }



    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparancy)

        {
            float elapsedTime = 0;
            while (elapsedTime < fadeTime)
            {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparancy, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
            }
        }
    private IEnumerator FadeRoutine(Tilemap tileMap, float fadeTime, float startValue, float targetTransparancy)

    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparancy, elapsedTime / fadeTime);
            tileMap.color = new Color(tileMap.color.r, tileMap.color.g, tileMap.color.b, newAlpha);
            yield return null;
        }
    }





}
