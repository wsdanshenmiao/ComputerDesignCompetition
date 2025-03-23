using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReminderAnswer : MonoBehaviour
{
    // 回答问题后的图片展示
    [SerializeField] private Transform correctAnswer;
    [SerializeField] private Transform wrongAnswer;
    
    private RecipeReminder reminder;
    
    private Transform selectTemplate;

    [SerializeField] private List<TextAsset> selects = new List<TextAsset>();
    
    public float textInterval = 100;

    public float answerShowTime = 1;
    
    public TextAsset correctSelect;
    
    [HideInInspector] public TMP_Text currSelect;

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
        correctAnswer.gameObject.SetActive(false);
        wrongAnswer.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        correctAnswer.gameObject.SetActive(false);
        wrongAnswer.gameObject.SetActive(false);
    }

    public void SetReminder(RecipeReminder reminder)
    {
        this.reminder = reminder;
    }

    public void RemoveReminder()
    {
        this.reminder = null;
    }
    
    public void Select(TMP_Text select)
    {
        currSelect = select;
    }

    // 确定选项
    public void DetermineSelect()
    {
        if (currSelect != null && correctSelect.text.Equals(currSelect.text.Substring(2))) {
            reminder?.UnlockReminder();
            StartCoroutine(AfterDetermineSelect(true));
        }
        else {
            StartCoroutine(AfterDetermineSelect(false));
        }
    }

    public void CloseAnswerUI()
    {
        reminder?.CloseAnswerUI();
    }

    // 显示回答结果并关闭
    private IEnumerator AfterDetermineSelect(bool rightSelect)
    {
        Transform answer = rightSelect ? correctAnswer : wrongAnswer;
        answer.gameObject.SetActive(true);
        yield return new WaitForSeconds(answerShowTime);
        answer.gameObject.SetActive(false);
        reminder?.CloseAnswerUI();
    }
}
