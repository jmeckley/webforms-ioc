using System;
using System.IO;

namespace WebApplication.Core.Implementation
{
    public class ServiceSettings
    {
        public string BaseDirectory { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
    }
}