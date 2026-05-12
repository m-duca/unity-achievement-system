using UnityEngine;

/// <summary>
/// Create Notifications called by AchievementsManager
/// </summary>
public class AchievementNotificationSpawner : MonoBehaviour
{
    // Inspector
    [Header("References")]
    [SerializeField] private AchievementNotificationUI _notificationPrefab;

    public void SpawnNotification(AchievementSO achievement)
    {
        AchievementNotificationUI notification = Instantiate(_notificationPrefab, Vector3.zero, Quaternion.identity, transform);
        notification.Initialize(achievement.IconSprite, achievement.AchievementName, achievement.Description);
    }
}
