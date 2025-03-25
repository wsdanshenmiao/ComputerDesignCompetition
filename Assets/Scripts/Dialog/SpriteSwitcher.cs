using DG.Tweening;
using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    [Header("Settings")]
    public Sprite[] sprites;
    public float fadeDuration = 1f;
    
    [Header("Events")]
    public UnityEngine.Events.UnityEvent onTransitionComplete;

    private SpriteRenderer _baseRenderer;
    private SpriteRenderer _overlayRenderer;
    private int _currentIndex = -1; // 初始索引改为-1
    private Sequence _transitionSequence;

    void Awake()
    {
        // 初始化基础渲染器
        _baseRenderer = GetComponent<SpriteRenderer>() ?? gameObject.AddComponent<SpriteRenderer>();
        _baseRenderer.sprite = null; // 确保初始不显示

        // 创建覆盖层渲染器
        _overlayRenderer = new GameObject("OverlayRenderer").AddComponent<SpriteRenderer>();
        _overlayRenderer.transform.SetParent(transform);
        _overlayRenderer.transform.localPosition = Vector3.zero;
        _overlayRenderer.transform.localScale = _baseRenderer.transform.localScale;
        _overlayRenderer.sortingOrder = _baseRenderer.sortingOrder + 1;
        _overlayRenderer.color = new Color(1, 1, 1, 0);
    }

    public void NextSprite(int _nextIndex)
    {
        if (sprites.Length == 0)
        {
            Debug.LogWarning("No sprites available");
            return;
        }

        // 停止正在进行的动画
        if (_transitionSequence != null && _transitionSequence.IsActive())
            _transitionSequence.Kill();

        // 计算下一个索引
        if (_nextIndex == -1) // 使用下一张背景图片
            _nextIndex = (_currentIndex + 1) % sprites.Length;
        
        // 有效性检查
        if (_nextIndex >= sprites.Length || sprites[_nextIndex] == null)
        {
            Debug.LogError($"Invalid sprite index: {_nextIndex}");
            return;
        }

        // 配置覆盖层
        _overlayRenderer.sprite = sprites[_nextIndex];
        _overlayRenderer.color = new Color(1, 1, 1, 0);

        // 如果是第一次切换，初始化基础层
        if (_currentIndex == -1)
        {
            _baseRenderer.color = Color.clear;
            _baseRenderer.sprite = sprites[_nextIndex];
        }

        // 创建动画序列
        _transitionSequence = DOTween.Sequence()
            .Append(_overlayRenderer.DOFade(1, fadeDuration).SetEase(Ease.Linear))
            .AppendCallback(() =>
            {
                // 更新基础层
                _baseRenderer.sprite = sprites[_nextIndex];
                _baseRenderer.color = Color.white;
                _currentIndex = _nextIndex;
                
                // 重置覆盖层
                _overlayRenderer.color = new Color(1, 1, 1, 0);
                _overlayRenderer.sprite = null;
                
                // 触发回调
                onTransitionComplete.Invoke();
            })
            .OnKill(() => 
            {
                if (_overlayRenderer != null)
                    _overlayRenderer.color = new Color(1, 1, 1, 0);
            });

        // 如果是第一次显示，直接渐入基础层
        if (_currentIndex == -1)
        {
            _transitionSequence = DOTween.Sequence()
                .Append(_baseRenderer.DOFade(1, fadeDuration).SetEase(Ease.Linear))
                .OnComplete(() => 
                {
                    _currentIndex = _nextIndex;
                    onTransitionComplete.Invoke();
                });
        }
    }

    void OnDestroy()
    {
        if (_transitionSequence != null)
            _transitionSequence.Kill();
    }
}