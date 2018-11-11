using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Audit : IDisposable
    {
        private static EventLog newLog = null;
        const string sourceName = "Common.Audit";
        const string logName = "CustomLog";
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

        public static void UpdateSuccess(string username)
        {
            if(newLog != null)
            {
                message = String.Format(AuditEventsFile.UpdateSuccess, username);
                newLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivsanja eventa {0} u event log", (int)AuditEventTypes.UpdateSuccess));
            }
        }
        public static void UpdateFailed(string username)
        {
            if (newLog != null)
            {
                message = String.Format(AuditEventsFile.UpdateFailed, username);
                newLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivanja eventa {0} u event log", (int)AuditEventTypes.UpdateFailed));
            }
        }

        public static void ReadSuccess(string username)
        {
            if (newLog != null)
            {
                message = String.Format(AuditEventsFile.ReadSuccess, username);
                newLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivanja eventa {0} u event log", (int)AuditEventTypes.ReadSuccess));
            }
        }
        public static void ReadFailed(string username)
        {
            if (newLog != null)
            {
                message = String.Format(AuditEventsFile.ReadFailed, username);
                newLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivanja eventa {0} u event log", (int)AuditEventTypes.ReadFailed));
            }
        }

        public static void CountSuccess(string username)
        {
            if (newLog != null)
            {
                message = String.Format(AuditEventsFile.CountSuccess, username);
                newLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivanja eventa {0} u event log", (int)AuditEventTypes.CountSuccess));
            }
        }
        public static void CountFailed(string username)
        {
            if (newLog != null)
            {
                message = String.Format(AuditEventsFile.CountFailed, username);
                newLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivanja eventa {0} u event log", (int)AuditEventTypes.CountFailed));
            }
        }

        public static void AddSuccess(string username)
        {
            if (newLog != null)
            {
                message = String.Format(AuditEventsFile.AddSuccess, username);
                newLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivanja eventa {0} u event log", (int)AuditEventTypes.AddSuccess));
            }
        }
        public static void AddFailed(string username)
        {
            if (newLog != null)
            {
                message = String.Format(AuditEventsFile.AddFailed, username);
                newLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivanja eventa {0} u event log", (int)AuditEventTypes.AddFailed));
            }
        }

        public static void RemoveSuccess(string username)
        {
            if (newLog != null)
            {
                message = String.Format(AuditEventsFile.RemoveSuccess, username);
                newLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivanja eventa {0} u event log", (int)AuditEventTypes.RemoveSuccess));
            }
        }
        public static void RemoveFailed(string username)
        {
            if (newLog != null)
            {
                message = String.Format(AuditEventsFile.RemoveFailed, username);
                newLog.WriteEntry(message);
            }
            else
            {
                throw new ArgumentException(String.Format("Greska prilikom upisivanja eventa {0} u event log", (int)AuditEventTypes.RemoveFailed));
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
