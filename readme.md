# EpiEvents

_EpiEvents_ is a library which makes it easier to handle _Episerver_ events by using a mediator. _EpiEvents_ allows a developer to handle an _Episerver_ event by implementing an event handler for the event he cares.

_EpiEvents.Core_ implements _Episerver_ content event handling.

# Install

Install using _NuGet_.

```
Install-Package EpiEvents.Core
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