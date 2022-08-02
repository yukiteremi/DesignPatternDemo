using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class SkillItemView : MonoBehaviour,IPointerEnterHandler
{
    public Text text;
    public Button button;
    public UnityAction SkillClick;
    public UnityAction SkillMoveEnter;
    //public 
    public void OnPointerEnter(PointerEventData eventData)
    {
        SkillMoveEnter();
    }
    public void Init(UnityAction act1, UnityAction act2,PokeMonSkillData data)
    {
        SkillMoveEnter = act2;
        SkillClick = act1;
        text = UITool.GetUIComponent<Text>(gameObject, "Text");
        if (data==null)
        {
            text.text = "无";
        }
        else
        {
            text.text = data.SkillName;
        }
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(()=> { SkillClick(); });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
