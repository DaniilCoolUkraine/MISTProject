using MistProject.Utils;
using Zenject;

namespace MistProject.MonoInstallers
{
    public class LocationUtilsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LocationUtils>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}