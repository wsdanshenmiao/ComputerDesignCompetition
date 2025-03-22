using System;
using UnityEngine;
using UnityEngine.UI;

public class RecipeReminder : MonoBehaviour
{
    private Image image;
    private Button button;

    [SerializeField] private ReminderAnswer answerUI;
    
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;
    
    private bool isLocked = true;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OpenAnswerUI);
    }

    private void Start()
    {
        answerUI.gameObject.SetActive(false);
        answerUI.SetReminder(this);
        LockReminder();
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OpenAnswerUI);
    }

    public void LockReminder()
    {
        isLocked = true;
        image.sprite = lockedSprite;
    }

    public void UnlockReminder()
    {
        isLocked = false;
        image.sprite = unlockedSprite;
    }

    public void OpenAnswerUI()
    {
        if (isLocked) {
            answerUI.gameObject.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void CloseAnswerUI()
    {
        transform.parent.gameObject.SetActive(true);
        answerUI.gameObject.SetActive(false);
        answerUI.currSelect = null;
    }
}