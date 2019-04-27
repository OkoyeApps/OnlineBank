using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Infrastructure.ViewModels
{
    public class TempdataClass
    {
        public string message { get; set; }
        public string Route { get; set; }
        public messageType messageType { get; set; }
    }

    public enum messageType
    {
        Error = 1,
        Success = 2
    }
}