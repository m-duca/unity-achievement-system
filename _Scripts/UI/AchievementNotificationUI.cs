using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Show notification on screen by a certain time (fade in / fade out effect using CanvasGroup)
/// Attached to Prefab_AchievementNotificationUI
/// </summary>
public class AchievementNotificationUI : MonoBehaviour
{
    // Inspector
    [Header("Settings")]
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _visibleTime;

    [Header("References")]
    [SerializeField] private CanvasGroup _notificationCg;
    [SerializeField] private Image _iconImg;
    [SerializeField] private TextMeshProUGUI _nameTxt;
    [SerializeField] private TextMeshProUGUI _descriptionTxt;

    public void Initialize(Sprite iconSprite, string name, string description)
    {
        _iconImg.sprite = iconSprite;
        _nameTxt.text = name;
        _descriptionTxt.text = description;

        StartCoroutine(Show_Coroutine());
    }

    private IEnumerator Show_Coroutine()
    {
        // Starts invisibile
        _notificationCg.alpha = 0f;

        // Fade In
        float timer = 0f;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            _notificationCg.alpha = Mathf.Lerp(0f, 1f, timer / _fadeDuration);

            yield return null;
        }
        _notificationCg.alpha = 1f;

        // Visibile time
        yield return new WaitForSeconds(_visibleTime);

        // Fade Out
        timer = 0f;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            _notificationCg.alpha = Mathf.Lerp(1f, 0f, timer / _fadeDuration);

            yield return null;
        }
        _notificationCg.alpha = 0f;

        Destroy(gameObject);
    }
}