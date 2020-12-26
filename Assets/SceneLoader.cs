using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Interactable
{
    // Start is called before the first frame update
    public Animator transition;
    public string sceneName;
    public Scene sceneToLoad;
    public float transitionTime = 1f;

    public override void Interact()
    {
        base.Interact();
        LoadNextScene();
    }
    //void Start()
    //{
    //    
    //}
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(sceneName));
        //SceneManager.LoadScene(sceneToLoad.buildIndex);
    }

    IEnumerator LoadScene(string scene)
    {
        transition.SetTrigger("Start");
        Debug.Log("Started Wait");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(scene);
        
        
        //play animaton

        //wait

        //Load it
    }
}
