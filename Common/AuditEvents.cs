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
        ReadFailed = 3
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
    }
}
