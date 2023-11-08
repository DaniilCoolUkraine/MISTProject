using MistProject.UI.Navigation;
using Zenject;

namespace MistProject.MonoInstallers
{
    public class NavigationManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<NavigationManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}