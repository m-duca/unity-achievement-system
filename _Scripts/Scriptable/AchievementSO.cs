using UnityEngine;

/// <summary>
/// Store achievement's data
/// </summary>
[CreateAssetMenu(fileName = "AchievementSO_", menuName = "Achievements/AchievementSO")]
public class AchievementSO : ScriptableObject
{
    [Header(("Settings"))]
    [SerializeField] private string _saveID;
    [SerializeField] private string _achievementName;
    [SerializeField, TextArea()] private string _description;
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private int _targetProgressValue;

    // Properties
    public string SaveID { get { return _saveID.ToLower(); } }
    public string AchievementName { get { return _achievementName; } }
    public string Description { get { return _description; } }
    public Sprite IconSprite { get { return _iconSprite; } }
    public int TargetProgressValue { get { return _targetProgressValue;} }
}
