using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrence : MonoBehaviour
{
    [SerializeField] private string TransitionName;


    private void Start()
    {
        if (TransitionName == SceneManagment.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();

            UIFade.Instance.FadeToClear();
        }
    }
}
