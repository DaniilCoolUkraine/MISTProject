using MistProject.UI.Screen;
using Zenject;

namespace MistProject.MonoInstallers
{
    public class MainScrollbarInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainScrollbar>().FromComponentInHierarchy().AsSingle();
        }
    }
}