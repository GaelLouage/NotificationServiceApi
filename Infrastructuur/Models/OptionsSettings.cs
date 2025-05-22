using Infrastructuur.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Models
{
    public class OptionsSettings
    {
        public EmailDataEntity EmailData { get; set; } = new EmailDataEntity();
        public EmailReciever EmailReciever { get; set; } = new EmailReciever();
        public SmsDataEntity SmsData { get; set; } = new SmsDataEntity();
        public SmsReciever SmsReciever { get; set; } = new SmsReciever();
    }
}
