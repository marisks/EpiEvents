# EpiEvents

_EpiEvents_ is a library which makes it easier to handle _Episerver_ events by using a mediator. _EpiEvents_ allows a developer to handle an _Episerver_ event by implementing an event handler for the event he cares.

_EpiEvents.Core_ implements _Episerver_ content event handling.

_EpiEvents.Commerce_ implements _Episerver Commerce_ event handling.

# Install

Install using _NuGet_.

```
Install-Package EpiEvents.Core
Install-Package EpiEvents.Commerce
```

# Configure

You have to configure [MediatR](https://github.com/jbogard/MediatR) according to its latest [documentation](https://github.com/jbogard/MediatR/wiki#structuremap). If you are using _MediatR_ only for _EpiEvents_, it is enough to configure only notification handlers. Below is an example of the _StructureMap_ configuration for it.

```
Scan(x =>
{
    x.TheCallingAssembly();
    x.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
    x.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
});
For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
For<IMediator>().Use<Mediator>();
```

You also have to configure _EpiEvent_ settings in the _StructureMap_ config.

```
For<EpiEvents.Core.ISettings>().Use<EpiEvents.Core.DefaultSettings>();
```

Default settings disables all loading events. Loading events causes _Episerver_ to slow down. But you can enable loading events in the _appSettings_.

```
<add key="EpiEvents:EnableLoadingEvents" value="true" />
```

# Usage

The package implements all events of _IContentEvents_ interface. You can find a full list of events under [src/EpiEvents.Core/Events](src/EpiEvents.Core/Events). There are some differences, though - there are some additional events. For example, there is a _CopyingContent_ event which is not found in the _IContentEvents_. This event is triggered by the content copying instead of a _CreatingContent_ event with _CopyContentEventArgs_. More info about content events can be found in the [Episerver Content Events Explained](http://marisks.net/2017/01/22/episerver-content-events-explained/) article.

Handling of an event is simple. Create [MediatR's](https://github.com/jbogard/MediatR/wiki#publishing) _INotificationHandler_ with a type parameter of the event you want to handle.

```
public class SampleHandler : INotificationHandler<CreatedContent>
{
    public void Handle(CreatedContent notification)
    {
        // Handle your event
    }
}
```
