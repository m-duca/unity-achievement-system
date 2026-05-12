using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle Achivements progression and unlocking
/// </summary>
public class AchievementsManager : MonoBehaviour
{
    // Singleton
    public static AchievementsManager Instance;

    // Inspector
    [Header("Debug")]
    [SerializeField] private bool _clearSaveOnStart = false;

    [Header("References")]
    [SerializeField] private AchievementSO[] _achievements;
    [SerializeField] private AchievementNotificationSpawner _notificationSpawner;

    // Not serialized
    private Dictionary<string, bool> _achievementsUnlocked;
    private Dictionary<string, int> _achievementsProgress;

    private const string PREFIX_ACHIEVEMENT_UNLOCKED = "achievementUnlocked_";
    private const string PREFIX_ACHIEVEMENT_PROGRESS = "achievementProgress_";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (_clearSaveOnStart)
            ClearSavedValues();

        _achievementsUnlocked = new Dictionary<string, bool>();
        _achievementsProgress = new Dictionary<string, int>();

        GetSavedValues();
    }

    private void GetSavedValues()
    {
        for (int i = 0; i < _achievements.Length; i++)
        {
            AchievementSO curAchievement = _achievements[i];

            _achievementsUnlocked[curAchievement.SaveID] = PlayerPrefs.GetInt(PREFIX_ACHIEVEMENT_UNLOCKED + curAchievement.SaveID) == (int)AchievementStateType.Unlocked;
            _achievementsProgress[curAchievement.SaveID] = PlayerPrefs.GetInt(PREFIX_ACHIEVEMENT_PROGRESS + curAchievement.SaveID);
        }
    }

    private void ClearSavedValues()
    {
        for (int i = 0; i < _achievements.Length; i++)
        {
            AchievementSO curAchievement = _achievements[i];

            PlayerPrefs.DeleteKey(PREFIX_ACHIEVEMENT_UNLOCKED + curAchievement.SaveID);
            PlayerPrefs.DeleteKey(PREFIX_ACHIEVEMENT_PROGRESS + curAchievement.SaveID);
        }

        PlayerPrefs.Save();
    }

    private void UnlockAchievement(AchievementSO achievement)
    {
        if (_achievementsUnlocked[achievement.SaveID]) // Block multiple calls at the same time
            return;

        _achievementsUnlocked[achievement.SaveID] = true;

        PlayerPrefs.SetInt(PREFIX_ACHIEVEMENT_UNLOCKED + achievement.SaveID, (int)AchievementStateType.Unlocked);
        PlayerPrefs.Save();

        _notificationSpawner.SpawnNotification(achievement);

        // Here you can unlock the achievement in other Environments like Steam
    }

    public void AddProgress(AchievementSO achievement, int progressValue)
    {
        if (_achievementsUnlocked[achievement.SaveID])
            return;

        _achievementsProgress[achievement.SaveID] = Mathf.Clamp(_achievementsProgress[achievement.SaveID] + progressValue, 0, achievement.TargetProgressValue);

        PlayerPrefs.SetInt(PREFIX_ACHIEVEMENT_PROGRESS + achievement.SaveID, _achievementsProgress[achievement.SaveID]);
        PlayerPrefs.Save();

        int currentProgress = _achievementsProgress[achievement.SaveID];
        if (currentProgress == achievement.TargetProgressValue)
            UnlockAchievement(achievement);
    }

    public bool IsUnlocked(AchievementSO achievement)
    {
        return _achievementsUnlocked[achievement.SaveID];
    }

    public int GetProgress(AchievementSO achievement)
    {
        return _achievementsProgress[achievement.SaveID];
    }

    public AchievementSO[] GetAllAchievements()
    {
        return _achievements;
    }
}
