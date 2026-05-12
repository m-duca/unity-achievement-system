using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Show Achievement's current progress 
/// Attached to Prefab_AchievementProgressUI
/// </summary>
public class AchievementProgressUI : MonoBehaviour
{
    // Inspector
    [Header("Settings")]
    [SerializeField] private float _lockedAlphaValue;

    [Header("References")]
    [SerializeField] private CanvasGroup _progressCg;
    [SerializeField] private Image _iconImg;
    [SerializeField] private TextMeshProUGUI _nameTxt;
    [SerializeField] private TextMeshProUGUI _descriptionTxt;
    [SerializeField] private TextMeshProUGUI _currentProgressTxt;
    [SerializeField] private Image _progressLeftImage;
    [SerializeField] private Image _currentProgressImage;
    [SerializeField] private Image _rewardImage;

    // Not serialized
    private float _maxProgressWidth;

    private void Awake() => _maxProgressWidth = _progressLeftImage.rectTransform.sizeDelta.x;

    public void Initialize(Sprite iconSprite, string name, string description, int currentProgress, int targetProgress)
    {
        _iconImg.sprite = iconSprite;
        _nameTxt.text = name;
        _descriptionTxt.text = description;

        SetProgress(currentProgress, targetProgress);
    }

    private void SetProgress(int currentProgress, int targetProgress)
    {
        if (currentProgress < targetProgress)
            _progressCg.alpha = _lockedAlphaValue;
        else
            _progressCg.alpha = 1f;

        _currentProgressTxt.text = $"{currentProgress} / {targetProgress}";

        float progress = Mathf.Clamp01((float)currentProgress / targetProgress);
        RectTransform rect = _currentProgressImage.rectTransform;
        rect.sizeDelta = new Vector2(_maxProgressWidth * progress, rect.sizeDelta.y);
    }
}