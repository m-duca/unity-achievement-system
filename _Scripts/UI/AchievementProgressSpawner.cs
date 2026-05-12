using UnityEngine;

/// <summary>
/// Instantiate all AchievementProgressUI prefabs when Achievement List is opened
/// </summary>
public class AchievementProgressSpawner : MonoBehaviour
{
    // Inspector
    [Header("References")]
    [SerializeField] private AchievementProgressUI _progressPrefab;
    [SerializeField] private Transform _contentVl;

    private void OnEnable() => SpawnAll();

    private void SpawnAll()
    {
        ClearBeforeSpawn();

        AchievementSO[] achievements = AchievementsManager.Instance.GetAllAchievements();

        for (int i = 0; i < achievements.Length; i++)
        {
            AchievementSO achievement = achievements[i];

            AchievementProgressUI progressUI = Instantiate(_progressPrefab, _contentVl);

            progressUI.Initialize(
                achievement.IconSprite,
                achievement.AchievementName,
                achievement.Description,
                AchievementsManager.Instance.GetProgress(achievement),
                achievement.TargetProgressValue
            );
        }
    }

    private void ClearBeforeSpawn()
    {
        for (int i = _contentVl.childCount - 1; i >= 0; i--)
            Destroy(_contentVl.GetChild(i).gameObject);
    }
}