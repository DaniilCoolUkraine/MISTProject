using UnityEngine;

namespace MistProject.Config
{
    [CreateAssetMenu(fileName = "GlobalSettings", menuName = "Config/GlobalSettings", order = 0)]
    public class GlobalSettingsSO : ScriptableObject
    {
        [field:SerializeField] public bool UseCelsius { get; set; }
        [field:SerializeField] public bool UseMetricSystem { get; set; }
        [field:SerializeField] public bool UseTwelveHoursSystem { get; set; }
        [field:SerializeField] public bool EnableAnimations { get; set; }
    }
}