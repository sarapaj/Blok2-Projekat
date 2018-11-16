using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Audit : IDisposable
    {
        private static EventLog newLog = null;
        //const string sourceName = "CustomLogSource";
        //const string logName = "CustomLog";
        const string sourceName = "System.ServiceModel 4.0.0.0";
        const string logName = "Application";
        private static string message;

        static Audit()
        {
            try
            {
                if(!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, logName);
                }
                newLog = new EventLog(logName, Environment.MachineName, sourceName);
            }
            catch (Exception e)
            {
                newLog = null;
                Console.WriteLine("Greska prilikom kreiranja log fajla. Error = {0}", e.Message);
            }
        }

        public static void UpdateSuccess(WindowsIdentity winId)
        {
            if(newLog != null)
            {
                string username = (winId.Name).Split('\\')[1];
                message = String.Format("Update by {0} was successful", username);
                newLog.WriteEntry(message,EventLogEntryType.SuccessAudit);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivsanja eventa u event log"));
            }
        }
        public static void UpdateFailed(WindowsIdentity winId)
        {
            if (newLog != null)
            {
                string username = (winId.Name).Split('\\')[1];

                message = String.Format("Update by {0} failed", username);
                newLog.WriteEntry(message, EventLogEntryType.FailureAudit);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivsanja eventa u event log"));
            }
        }

        public static void ReadSuccess(WindowsIdentity winId)
        {
            if (newLog != null)
            {
                string username = (winId.Name).Split('\\')[1];
                message = String.Format("Read by {0} was successful", username);
                newLog.WriteEntry(message, EventLogEntryType.SuccessAudit);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivsanja eventa u event log"));
            }
        }
        public static void ReadFailed(WindowsIdentity winId)
        {
            if (newLog != null)
            {
                string username = (winId.Name).Split('\\')[1];
                message = String.Format("Read by {0} failed", username);
                newLog.WriteEntry(message, EventLogEntryType.FailureAudit);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivsanja eventa u event log"));
            }
        }

        public static void CountSuccess(WindowsIdentity winId)
        {
            if (newLog != null)
            {
                string username = (winId.Name).Split('\\')[1];
                message = String.Format("Count by {0} was successful", username);
                newLog.WriteEntry(message, EventLogEntryType.SuccessAudit);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivsanja eventa u event log"));
            }
        }
        public static void CountFailed(WindowsIdentity winId)
        {
            if (newLog != null)
            {
                string username = (winId.Name).Split('\\')[1];
                message = String.Format("Count by {0} failed", username);
                newLog.WriteEntry(message, EventLogEntryType.FailureAudit);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivsanja eventa u event log"));
            }
        }

        public static void AddSuccess(WindowsIdentity winId)
        {
            if (newLog != null)
            {
                string username = (winId.Name).Split('\\')[1];
                message = String.Format("Add entity by {0} was successful", username);
                newLog.WriteEntry(message, EventLogEntryType.SuccessAudit);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivsanja eventa u event log"));
            }
        }
        public static void AddFailed(WindowsIdentity winId)
        {
            if (newLog != null)
            {
                string username = (winId.Name).Split('\\')[1];
                message = String.Format("Add entity by {0} failed", username);
                newLog.WriteEntry(message, EventLogEntryType.FailureAudit);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivsanja eventa u event log"));
            }
        }

        public static void RemoveSuccess(WindowsIdentity winId)
        {
            if (newLog != null)
            {
                string username = (winId.Name).Split('\\')[1];
                message = String.Format("Remove entity by {0} was successful", username);
                newLog.WriteEntry(message, EventLogEntryType.SuccessAudit);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivsanja eventa u event log"));
            }
        }
        public static void RemoveFailed(WindowsIdentity winId)
        {
            if (newLog != null)
            {
                string username = (winId.Name).Split('\\')[1];
                message = String.Format("Remove entity by {0} failed", username);
                newLog.WriteEntry(message, EventLogEntryType.FailureAudit);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivsanja eventa u event log"));
            }
        }

        public void Dispose()
        {
            if (newLog != null)
            {
                newLog.Dispose();
                newLog = null;
            }
        }
    }
}
