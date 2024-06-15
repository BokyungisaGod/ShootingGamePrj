using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamBtn : MonoBehaviour
{
    private void Awake()
    {
        var btn = GetComponent<Button>();

        Sprite sprite = Resources.Load<Sprite>("Ham") as Sprite;

        btn.onClick.AddListener(() => { SelectSceneManager.Inst.Select(sprite); });
    }
}
