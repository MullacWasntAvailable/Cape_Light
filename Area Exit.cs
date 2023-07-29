using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private float waitToLoadTime = 1;
    private void OnTriggerEnter2D(Collider2D cother)
    {
        if (cother.gameObject.GetComponent<PlayerController>())
        {   
            SceneManagment.Instance.SetTransitionName(sceneTransitionName);

            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadScreenRoutine());
        }
      
    }

    private IEnumerator LoadScreenRoutine()
    {
        while (waitToLoadTime>= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
    
}
