using MistProject.Requests;
using Zenject;

namespace MistProject.MonoInstallers
{
    public class RequestHolderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<RequestHolder>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}