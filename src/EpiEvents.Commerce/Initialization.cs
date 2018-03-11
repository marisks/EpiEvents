using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace EpiEvents.Commerce
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class Initialization : IInitializableModule
    {
        private bool _initialized;

        public void Initialize(InitializationEngine context)
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;

            var mediator = context.Locate.Advanced.GetInstance<EventsMediator>();
            mediator.Initialize();
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}