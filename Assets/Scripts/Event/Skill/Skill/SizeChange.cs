using System.Collections;
using UnityEngine;

public class SizeChange : Skill
{
    [Header("尺寸变化配置")]
    [SerializeField] private float changeDuration = 5f;      // 变化持续时间
    [SerializeField] private float changeSpeed = 2f;         // 变化速度

    private Coroutine currentSizeChangeCoroutine;           // 当前正在执行的尺寸变化协程

    public BoolEventSO OnPlayerSizeChanged;

    // 用于获取当前状态的属性
    public bool IsEnlarged;//变大状态
    public bool IsShrunk;//变小状态
    public bool IsNormal = true; //原状

    private void Awake()
    {
        // 确保初始状态为正常大小
        IsEnlarged = false;
        IsShrunk = false;
        IsNormal = true;
    }

    // 放大效果
    public void EnlargeSize()
    {
        AudioManager.PlayAudio(AudioName.PlayerChangeSize, true);
        if (currentSizeChangeCoroutine != null)
        {
            StopCoroutine(currentSizeChangeCoroutine);
        }
        float scale = player.playerCharacter.playerPara.bigSize;
        currentSizeChangeCoroutine = StartCoroutine(ChangeSizeCoroutine(scale));
    }

    // 缩小效果
    public void ShrinkSize()
    {
        AudioManager.PlayAudio(AudioName.PlayerChangeSize, true);
        if (currentSizeChangeCoroutine != null)
        {
            StopCoroutine(currentSizeChangeCoroutine);
        }
        float scale = player.playerCharacter.playerPara.smallSize;
        currentSizeChangeCoroutine = StartCoroutine(ChangeSizeCoroutine(scale));
    }

    // 取消尺寸变化效果
    public void CancelSizeChange()
    {
        // 设置音效
        AudioManager.PlayAudio(AudioName.PlayerChangeSize, true);
        if (currentSizeChangeCoroutine != null)
        {
            StopCoroutine(currentSizeChangeCoroutine);
        }
        float scale = player.playerCharacter.playerPara.originSize;
        currentSizeChangeCoroutine = StartCoroutine(ChangeSizeCoroutine(scale));
    }

    private IEnumerator ChangeSizeCoroutine(float targetScale)
    {
        PlayerSO playerPara = player.playerCharacter.playerPara;

        Vector3 targetSize = new Vector3(targetScale, targetScale, targetScale);
        float elapsedTime = 0f;
        Vector3 currentScale = player.transform.localScale;

        // 重置所有状态
        IsEnlarged = false;
        IsShrunk = false;
        IsNormal = false;

        // 设置新状态
        if (targetScale > playerPara.originSize)
            IsEnlarged = true;
        else if (targetScale < playerPara.originSize)
            IsShrunk = true;
        else
            IsNormal = true;

        // 逐渐变化到目标尺寸
        while (elapsedTime < 1f / changeSpeed)
        {
            elapsedTime += Time.deltaTime;
            player.transform.localScale = Vector3.Lerp(currentScale, targetSize, elapsedTime * changeSpeed);
            yield return null;
        }

        player.transform.localScale = targetSize;
        OnPlayerSizeChanged.RaiseEvent(targetScale > playerPara.originSize);

        // 如果不是恢复原状，则等待持续时间后自动恢复
        if (targetScale != playerPara.originSize)
        {
            yield return new WaitForSeconds(changeDuration);

            if (currentSizeChangeCoroutine != null)
                StopCoroutine(currentSizeChangeCoroutine);

            currentSizeChangeCoroutine = StartCoroutine(
                ChangeSizeCoroutine(playerPara.originSize));
        }
    }
}
