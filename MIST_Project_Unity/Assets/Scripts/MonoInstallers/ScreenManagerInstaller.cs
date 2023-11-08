using MistProject.UI.Screen;
using Zenject;

namespace MistProject.MonoInstallers
{
    public class ScreenManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ScreenManager>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}