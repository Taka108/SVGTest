using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField]
    private RectTransform _mask;
    [SerializeField]
    private FadeScreenManager _fadeScreenManager;
    private Sequence _loadingAnimation;
    private Tween _hideAnimation;
    private Tween _showAnimation;
    private readonly Vector2 _nonLoadingSize = Vector2.one * 6000.0f;
    private readonly Vector2 _loadingMaskSize = Vector2.one * 100.0f;
    private void Init()
    {
        _hideAnimation?.Kill(true);
        _showAnimation?.Kill(true);
        _loadingAnimation?.Kill(true);
    }
    public void StartLoadAnimation(System.Action onStarted)
    {
        PlayLoadAnimation(
            () => {
                onStarted?.Invoke();
                PlayLoadingAnimation();
            }
            );
    }
    public void FinsihLoadAnimation(System.Action onComplete)
    {
        if (_loadingAnimation == null)
        {
            PlayFinishAnimation(onComplete);
        }
        else
        {
            // ロード中アニメをやり切ってからロード完了アニメ開始
            _loadingAnimation.OnStepComplete(
                () =>
                {
                    PlayFinishAnimation(onComplete);
                }
                );
        }
    }
    private void PlayLoadAnimation(System.Action onComplete)
    {
        Init();
        _fadeScreenManager.StartFade(Color.white, 1.0f, null);
        _hideAnimation = _mask.DOSizeDelta(_loadingMaskSize, 0.8f).SetEase(Ease.OutBack, 0.5f)
        .OnComplete(() => onComplete?.Invoke());
    }
    private void PlayLoadingAnimation()
    {
        Init();
        _loadingAnimation = DOTween.Sequence();
        _loadingAnimation.AppendInterval(1.0f);
        _loadingAnimation.Append(_mask.DORotate(Vector3.forward * 12.5f, 0.2f).SetEase(Ease.OutQuad));
        _loadingAnimation.Append(_mask.DORotate(Vector3.forward * -(360.0f + 12.5f + 12.5f), 0.4f,RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad));
        _loadingAnimation.Append(_mask.DORotate(Vector3.zero, 0.15f).SetEase(Ease.InQuad));
        _loadingAnimation.SetLoops(-1);
    }
    private void PlayFinishAnimation(System.Action onComplete)
    {
        Init();
        _fadeScreenManager.StartFade(Color.clear, 0.5f, null);
        _mask.DOSizeDelta(_nonLoadingSize, 0.8f).SetEase(Ease.InBack, 0.5f)
        .OnComplete(() => onComplete?.Invoke());
    }
}
