using MistProject.Utils.Location;
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