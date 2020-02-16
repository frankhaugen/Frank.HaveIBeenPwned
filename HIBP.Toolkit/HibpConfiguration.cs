using System;

namespace HIBP.Toolkit
{
    public class HibpConfiguration
    {
        public string ApiKey { get; set; }
        public Uri BaseAddress { get; set; } = new Uri("https://haveibeenpwned.com/api/v3");
        public Uri PwnedPasswordAddress { get; set; } = new Uri("https://api.pwnedpasswords.com/range");
        public string ApplicationName { get; set; } = "HIBP.Toolkit";
    }
}