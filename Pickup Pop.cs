using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPop : MonoBehaviour
{
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;

  

    private void Awake()
    {
        StartCoroutine(AnimCurveSpawnRoutine());

    }
   


    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;

        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);


        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightT, heightY);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);

            yield return null;
        }


    }
}
