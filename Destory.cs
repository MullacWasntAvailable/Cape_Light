using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destory : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if ( ps && !ps.IsAlive()) 
        {
            OnDestroy();
        }
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
