using System.Diagnostics;
using SignalR.Hubs;

namespace log4net.SignalR
{
    public class SignalrAppenderHub : Hub
    {
        public SignalrAppenderHub()
        {
            var signalrAppender = SignalrAppender.Instance;
            signalrAppender.MessageLoggedEvent += OnMessageLoggedEvent;
        }

        public void Listen()
        {
        }

        private void OnMessageLoggedEvent(object sender, MessageLoggedEventArgs e)
        {
            var logEventObject = new
            {
                e.FormattedEvent,
                Message = e.LoggingEvent.ExceptionObject != null ? e.LoggingEvent.ExceptionObject.Message : e.LoggingEvent.RenderedMessage,
                Level = e.LoggingEvent.Level.Name,
                TimeStamp = e.LoggingEvent.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fff")
                /* e.LoggingEvent.Domain,
                e.LoggingEvent.Identity,
                e.LoggingEvent.LocationInformation,
                e.LoggingEvent.LoggerName,
                e.LoggingEvent.MessageObject,
                e.LoggingEvent.Properties,
                e.LoggingEvent.ThreadName,
                e.LoggingEvent.UserName */
            };

            Clients.onLoggedEvent(logEventObject);
        }
    }
}