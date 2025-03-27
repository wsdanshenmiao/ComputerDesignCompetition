using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
    [Header("Settings")]
    public Sprite[] sprites;
    public float fadeDuration = 1f;

    [Header("Events")] 
    public UnityEvent onTransitionComplete;

    public Image _baseImage;
    private Image _overlayImage;
    private int _currentIndex = -1;
    private Coroutine _currentCoroutine;

    void Awake()
    {
        // 初始化基础Image组件
        _baseImage = GetComponent<Image>() ?? gameObject.AddComponent<Image>();
        _baseImage.sprite = null;
        _baseImage.color = Color.clear;

        // 创建覆盖层Image组件
        GameObject overlayObj = new GameObject("OverlayImage");
        overlayObj.transform.SetParent(transform);
        overlayObj.transform.localPosition = Vector3.zero;
        overlayObj.transform.localScale = Vector3.one;
        _overlayImage = overlayObj.AddComponent<Image>();
        _overlayImage.rectTransform.anchorMin = Vector2.zero;
        _overlayImage.rectTransform.anchorMax = Vector2.one;
        _overlayImage.rectTransform.offsetMin = Vector2.zero;
        _overlayImage.rectTransform.offsetMax = Vector2.zero;
        _overlayImage.color = new Color(1, 1, 1, 0);
        
        // 确保渲染顺序
        _overlayImage.transform.SetAsLastSibling();
    }

    public void NextSprite(int nextIndex)
    {
        if (sprites.Length == 0)
        {
            Debug.LogWarning("No sprites available");
            return;
        }

        // 停止正在运行的协程
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        // 计算有效索引
        if (nextIndex == -1)
        {
            nextIndex = (_currentIndex + 1) % sprites.Length;
        }
        else if (nextIndex < -1 || nextIndex >= sprites.Length)
        {
            Debug.LogError($"Invalid sprite index: {nextIndex}");
            return;
        }

        // 启动新协程
        _currentCoroutine = StartCoroutine(TransitionCoroutine(nextIndex));
    }

    private IEnumerator TransitionCoroutine(int targetIndex)
    {
        // 首次切换的特殊处理
        if (_currentIndex == -1)
        {
            _baseImage.sprite = sprites[targetIndex];
            yield return FadeInBase();
            _currentIndex = targetIndex;
        }
        else
        {
            // 常规切换流程
            _overlayImage.sprite = sprites[targetIndex];
            yield return FadeInOverlay();
            
            _baseImage.sprite = sprites[targetIndex];
            _baseImage.color = Color.white;
            _currentIndex = targetIndex;
            
            yield return FadeOutOverlay();
        }

        onTransitionComplete.Invoke();
        Debug.Log($"Transition complete to index: {targetIndex}");
    }

    // 基础层淡入（首次使用）
    private IEnumerator FadeInBase()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            _baseImage.color = new Color(1, 1, 1, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _baseImage.color = Color.white;
    }

    // 覆盖层淡入
    private IEnumerator FadeInOverlay()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            _overlayImage.color = new Color(1, 1, 1, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _overlayImage.color = Color.white;
    }

    // 覆盖层淡出
    private IEnumerator FadeOutOverlay()
    {
        _overlayImage.color = Color.white;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);
            _overlayImage.color = new Color(1, 1, 1, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _overlayImage.color = new Color(1, 1, 1, 0);
        _overlayImage.sprite = null;
    }

    void OnDestroy()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }
}