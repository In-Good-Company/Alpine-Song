using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Transition_IndoorSwitch : Camera_IndoorSwitch
{
    public Animator transition;
    public bool sceneChange;
    public string Scene;
    public float transitionTime = 1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player)
        {
            if (sceneChange == true)
            {
                LoadNextScene();
                //StartCoroutine(PlayTransition(Scene));
            }
            navSwitch.enabled = false;
            Player.transform.position = playerTransitionPos.transform.position;
            mainCam.transform.position = camPos.transform.position;

            navSwitch.enabled = true;

           
        }
    }
    public override void Interact()
    {
        base.Interact();
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        StartCoroutine(PlayTransition(Scene));
        //SceneManager.LoadScene(sceneToLoad.buildIndex);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            this.GetComponent<SphereCollider>().enabled = false;
        }
    }

    IEnumerator PlayTransition(string scene)
    {
        transition.SetTrigger("Start");
        AsyncOperation asynchOperation = SceneManager.LoadSceneAsync(scene);
        transition.SetTrigger("Start");
        Debug.Log("Started Wait");


        yield return new WaitForSeconds(transitionTime);
        if (sceneChange)
        {
            transition.SetTrigger("loadingBar");
        }

        SceneManager.LoadScene(scene);


        //play animaton

        //wait

        //Load it
    }
}
