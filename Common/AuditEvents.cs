using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum AuditEventTypes
    {
        UpdateSuccess = 0,
        UpdateFailed = 1,
        ReadSuccess = 2, 
        ReadFailed = 3,
        CountSuccess = 4,
        CountFailed = 5,
        AddSuccess = 6,
        AddFailed = 7,
        RemoveSuccess = 8,
        RemoveFailed = 9
    }

    public class AuditEvents
    {
        private static ResourceManager resourceMng = null;
        private static object resourceLock = new object();

        private static ResourceManager ResourceMng
        {
            get
            {
                lock (resourceLock)
                {
                    if(resourceMng == null)
                    {
                        resourceMng = new ResourceManager(typeof(AuditEventsFile).FullName, Assembly.GetExecutingAssembly());
                    }

                    return resourceMng;
                }
            }
        }

        public static string UpdateSuccess
        {
            get
            {
                return ResourceMng.GetString(AuditEventTypes.UpdateSuccess.ToString());
            }
        }

        public static string UpdateFailed
        {
            get
            {
                return ResourceMng.GetString(AuditEventTypes.UpdateFailed.ToString());
            }
        }

        public static string ReadSuccess
        {
            get
            {
                return ResourceMng.GetString(AuditEventTypes.ReadSuccess.ToString());
            }
        }

        public static string ReadFailed
        {
            get
            {
                return ResourceMng.GetString(AuditEventTypes.ReadFailed.ToString());
            }
        }

        public static string CountSuccess
        {
            get
            {
                return ResourceMng.GetString(AuditEventTypes.CountSuccess.ToString());
            }
        }

        public static string CountFailed
        {
            get
            {
                return ResourceMng.GetString(AuditEventTypes.CountFailed.ToString());
            }
        }

        public static string AddSuccess
        {
            get
            {
                return ResourceMng.GetString(AuditEventTypes.AddSuccess.ToString());
            }
        }

        public static string AddFailed
        {
            get
            {
                return ResourceMng.GetString(AuditEventTypes.AddFailed.ToString());
            }
        }

        public static string RemoveSuccess
        {
            get
            {
                return ResourceMng.GetString(AuditEventTypes.RemoveSuccess.ToString());
            }
        }

        public static string RemoveFailed
        {
            get
            {
                return ResourceMng.GetString(AuditEventTypes.RemoveFailed.ToString());
            }
        }
    }
}
