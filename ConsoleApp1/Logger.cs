using System;
using Calabonga.EntityFramework;

namespace Calabonga.Framework.Demo
{
    public class Logger : IEntityFrameworkLogService
    {
        public void LogInfo(string message)
        {
            return;
        }

        public void LogError(Exception exception)
        {
            return;
        }
    }
}