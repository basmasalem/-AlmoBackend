using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
   public class Settings
    {
        public int SettingsId { get; set; }
        public string PrivacyPolcy { get; set; }
        public string TermsAndConditions { get; set; }
        public decimal ThreeMonthsCost { get; set; }

        public decimal OneMonthCost { get; set; }
    }
}
