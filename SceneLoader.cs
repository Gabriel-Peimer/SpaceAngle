using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //transition
    public Animator transition;
    private float transitionTime = 1f;

    public void LoadSceneByName(string sceneName, string triggerName)
    {
        StartCoroutine(LoadNextScene(sceneName, triggerName));
    }

    IEnumerator LoadNextScene(string sceneName, string triggerName)
    {
        transition.SetTrigger(triggerName);//setting the trigger to play the animation

        yield return new WaitForSeconds(transitionTime);//waiting so that the animation can play

        SceneManager.LoadScene(sceneName);
    }
}
