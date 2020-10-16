using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RolaltyCardProject.Models
{
    public class Message
    {
        public enum Type
        {
            Successful,
            Error,
            Info
        }

        public Type MessageType { get; set; }
        public string Text { get; set; }

        public string GetAlertNavClass()
        {
            return MessageType switch
            {
                Type.Successful => "alert-success",
                Type.Error => "alert-danger",
                Type.Info => "alert-info",
                _ => "",
            };
        }
    }
}
