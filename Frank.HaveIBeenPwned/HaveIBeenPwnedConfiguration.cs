using System;

namespace Frank.HaveIBeenPwned
{
    public class HaveIBeenPwnedConfiguration
    {
        public string ApiKey { get; set; }
        public Uri BaseAddress { get; set; }
        public Uri PwnedPasswordAddress { get; set; }
        public string ApplicationName { get; set; }
    }
}