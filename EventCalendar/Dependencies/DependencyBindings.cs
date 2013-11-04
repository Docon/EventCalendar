using EventCalendar.Data.CalendarEventProviders;
using Ninject.Modules;

namespace EventCalendar.Dependencies
{
    public class DependencyBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ICalendarEventProvider>().To<GoogleCalendarProvider>();
        }
    
    }
}