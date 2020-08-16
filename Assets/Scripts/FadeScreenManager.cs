using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum FadeType
{
    IN,
    OUT
}

public enum FadeDrawType
{
    NORMAL,
}

/// <summary>
/// フェード
/// </summary>
public class FadeScreenManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("フェード用画像")]
    private Image _fadeImage;
    [SerializeField]

    Tween _fadeTween;

    protected void Awake()
    {
        _fadeImage.material.SetFloat("_Gradation", 1);
        _fadeImage.color = Color.clear;
    }
    /// <summary>
    /// 通常フェードをかける
    /// </summary>
	public void StartFade(Color fadeColor, float duration, Action onComplete)
    {
        if (_fadeTween != null)
        {
            _fadeTween.Complete(true);
        }

        //透明からのフェードは、無色フェード色からフェードする
        if (_fadeImage.color.a == 0.0f)
        {
            _fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0.0f);
        }

        _fadeTween = DOTween.To(
            () => _fadeImage.color,                 // 何を対象にするのか
            color => _fadeImage.color = color,      // 値の更新
            fadeColor,                              // 最終的な値
            duration                                // アニメーション時間
        ).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    public void OnDestroy()
    {
        _fadeImage.material.SetFloat("_Gradation", 0);
    }
}
