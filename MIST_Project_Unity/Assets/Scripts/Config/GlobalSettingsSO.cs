using UnityEngine;

namespace MistProject.Config
{
    [CreateAssetMenu(fileName = "GlobalSettings", menuName = "Config/GlobalSettings", order = 0)]
    public class GlobalSettingsSO : ScriptableObject
    {
        [field:SerializeField] public bool UseCelsius { get; private set; }
        [field:SerializeField] public bool UseMetricSystem { get; private set; }
    }
}