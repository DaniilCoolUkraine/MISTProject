using MistProject.Config;
using UnityEngine;
using Zenject;

namespace MistProject.MonoInstallers
{
    [CreateAssetMenu(fileName = "GlobalSettingsInstaller", menuName = "Config/GlobalSettingsInstaller", order = 0)]
    public class GlobalSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GlobalSettingsSO _globalSettings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GlobalSettingsSO>().FromInstance(_globalSettings).AsSingle();
        }
    }
}