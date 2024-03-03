using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoad : MonoBehaviour
{
    // Start is called before the first frame
    public Slider progressBar;
    public Text loadText;
    public static string loadScene;
    public static int loadType;
    AsyncOperation operation;
    IEnumerator LoadScene() 
    {
        
        yield return null;
        if(PlayerPrefs.GetInt("Character")==1)
             operation = SceneManager.LoadSceneAsync("Play");
        else 
        {
            Debug.Log("2¿Â");
            operation = SceneManager.LoadSceneAsync("Play 1");
        }
            
        operation.allowSceneActivation = false;
        while (!operation.isDone) 
        {
            yield return null;
            if (progressBar.value < 0.9f) 
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 0.9f, Time.deltaTime);
            }
            if (progressBar.value >= 0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.deltaTime);
            }
            if (progressBar.value >=1f) 
            {
                loadText.text = "Press SpaceBar";
            }
            if (Input.GetKeyDown(KeyCode.Space)&&progressBar.value>=1f&&operation.progress>=0.9f) 
            {
                operation.allowSceneActivation = true;
            }
        }
    }
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
