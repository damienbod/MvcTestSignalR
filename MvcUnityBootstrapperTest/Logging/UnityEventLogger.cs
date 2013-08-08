using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MvcUnityBootstrapperTest.Logging
{
    [EventSource(Name = "UnityLogger")]
    public class UnityEventLogger : EventSource
    {
        public static readonly UnityEventLogger Log = new UnityEventLogger();

        private IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<DiagnosisEventSourceService>();
            
        private const int createUnityMessageEventId = 1;
        private const int disposeUnityMessageEventId = 2;
        private const int logUnityMessageEventId = 3;

        [Event(createUnityMessageEventId, Message = "CreateUnityMessage: {0}", Level = EventLevel.Informational)]
        public void CreateUnityMessage(string message)
        {
            if (IsEnabled()) WriteEvent(createUnityMessageEventId, message);

            hubContext.Clients.All.addDiagnosisMessage("UnityLogger",
                createUnityMessageEventId,
                DateTime.Now.ToString(CultureInfo.InvariantCulture),
                "CreateUnityMessage: " + message + " EventLevel.Informational");
        }

        [Event(disposeUnityMessageEventId, Message = "DisposeUnityMessage {0}", Level = EventLevel.Informational)]
        public void DisposeUnityMessage(string message)
        {
            if (IsEnabled()) WriteEvent(disposeUnityMessageEventId, message);

            hubContext.Clients.All.addDiagnosisMessage("UnityLogger",
               disposeUnityMessageEventId,
               DateTime.Now.ToString(CultureInfo.InvariantCulture),
               "DisposeUnityMessage: " + message + " EventLevel.Informational");
         }

        [Event(logUnityMessageEventId, Message = "{0}", Level = EventLevel.Informational)]
        public void LogUnityMessage(string message)
        {
            if (IsEnabled()) WriteEvent(logUnityMessageEventId, message);

            hubContext.Clients.All.addDiagnosisMessage("UnityLogger",
               logUnityMessageEventId,
               DateTime.Now.ToString(CultureInfo.InvariantCulture),
               message + " EventLevel.Informational");
        }
    }



}