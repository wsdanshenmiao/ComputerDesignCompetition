using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReminderAnswer : MonoBehaviour
{
    private Transform selectTemplate;

    public float textInterval = 100;

    public TextAsset correctSelect;
    [SerializeField] List<TextAsset> selects = new List<TextAsset>();
    
    public TMP_Text currSelect;

    private void Awake()
    {
        selectTemplate = transform.Find("SelectTemplate");
    }

    private void Start()
    {
        for (int i = 0; i < selects.Count; i++) {
            RectTransform trans = Instantiate(selectTemplate, transform).GetComponent<RectTransform>();
            trans.gameObject.SetActive(true);
            trans.anchoredPosition = new Vector2(
                trans.anchoredPosition.x, trans.anchoredPosition.y - i * textInterval);
            string text = (char)('A' + i) + ":" + selects[i].text;
            trans.GetComponentInChildren<TMP_Text>().text = text;
        }
    }

    public void Select(TMP_Text select)
    {
        currSelect = select;
    }

    public void DetermineSelect()
    {
        if (currSelect != null && correctSelect.text.Equals(currSelect.text.Substring(2))) {
            GetComponentInParent<RecipeReminder>().UnlockReminder();
        }
        gameObject.SetActive(false);
    }
}
