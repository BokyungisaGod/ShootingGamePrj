using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneManager : MonoBehaviour
{
    private static SelectSceneManager inst;

    public static SelectSceneManager Inst => inst;

    public Sprite targetSprite;

    public void Start()
    {
        if (inst != null)
        {
            Debug.Log("!!");

            if (inst != this)
            {
                Debug.Log("!!");
                GameObject.Destroy(this);
            }
        }
        else 
        {
            inst = this;
            GameObject.DontDestroyOnLoad(this);
        }
    }

    public void Select(Sprite _sprite)
    {
        Debug.Log(_sprite.name);

        targetSprite = _sprite;
    }
}
