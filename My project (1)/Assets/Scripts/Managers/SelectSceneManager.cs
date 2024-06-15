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
            if (inst != this)
                GameObject.Destroy(this);
        }
        else 
        {
            inst = this;
            GameObject.DontDestroyOnLoad(this);
        }
    }

    public void Select(Sprite _sprite)
    {
        targetSprite = _sprite;
    }
}
