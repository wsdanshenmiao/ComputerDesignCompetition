using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpriteSwitcher : MonoBehaviour
{
    [Header("Settings")]
    public Sprite[] sprites;
    public float fadeDuration = 1f;

    [Header("Events")] 
    public UnityEvent onTransitionComplete;

    private SpriteRenderer _baseRenderer;
    private SpriteRenderer _overlayRenderer;
    private int _currentIndex = -1;
    private Coroutine _currentCoroutine;

    void Awake()
    {
        // 初始化基础渲染器
        _baseRenderer = GetComponent<SpriteRenderer>() ?? gameObject.AddComponent<SpriteRenderer>();
        _baseRenderer.sprite = null;
        _baseRenderer.color = Color.clear;

        // 创建覆盖层渲染器
        _overlayRenderer = new GameObject("OverlayRenderer").AddComponent<SpriteRenderer>();
        _overlayRenderer.transform.SetParent(transform);
        _overlayRenderer.transform.localPosition = Vector3.zero;
        _overlayRenderer.transform.localScale = Vector3.one;
        _overlayRenderer.sortingOrder = _baseRenderer.sortingOrder + 1;
        _overlayRenderer.color = new Color(1, 1, 1, 0);
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
            _baseRenderer.sprite = sprites[targetIndex];
            yield return FadeInBase();
            _currentIndex = targetIndex;
        }
        else
        {
            // 常规切换流程
            _overlayRenderer.sprite = sprites[targetIndex];
            yield return FadeInOverlay();
            
            _baseRenderer.sprite = sprites[targetIndex];
            _baseRenderer.color = Color.white;
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
            _baseRenderer.color = new Color(1, 1, 1, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _baseRenderer.color = Color.white;
    }

    // 覆盖层淡入
    private IEnumerator FadeInOverlay()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            _overlayRenderer.color = new Color(1, 1, 1, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _overlayRenderer.color = Color.white;
    }

    // 覆盖层淡出
    private IEnumerator FadeOutOverlay()
    {
        _overlayRenderer.color = Color.white;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);
            _overlayRenderer.color = new Color(1, 1, 1, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _overlayRenderer.color = new Color(1, 1, 1, 0);
        _overlayRenderer.sprite = null;
    }

    void OnDestroy()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }
}