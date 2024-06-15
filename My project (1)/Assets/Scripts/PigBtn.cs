using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PigBtn : MonoBehaviour
{
    private void Awake()
    {
        var btn = GetComponent<Button>();

        Sprite sprite = Resources.Load<Sprite>("Pig") as Sprite;

        btn.onClick.AddListener(() => { SelectSceneManager.Inst.Select(sprite); });
    }
}
