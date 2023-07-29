using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
  
    [SerializeField] private float roamChangeDirFloat = 2f;


    private enum State
    {
        Roaming
    }
    private State state;
    private EnemyPathFinding enemyPathFinding;

    private void Awake()
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming; 
    }
    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine() { 
        while (state == State.Roaming)
        {
            Vector2 roamPostion= GetRoamingPostion();
            enemyPathFinding.MoveTo(roamPostion);
            yield return new WaitForSeconds(roamChangeDirFloat);
        }
   
    }

    private Vector2 GetRoamingPostion()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
