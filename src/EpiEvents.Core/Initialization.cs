using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace EpiEvents.Core
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class Initialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var mediator = context.Locate.Advanced.GetInstance<EventsMediator>();
            mediator.Initialize();
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}