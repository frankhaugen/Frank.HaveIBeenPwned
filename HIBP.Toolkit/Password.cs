namespace HIBP.Toolkit
{
    public class Password
    {
        public string PasswordString { get; set; }
        public bool IsPwned { get; set; }
        public int TimesPwned { get; set; } = 0;
        public string Sha1Prefix { get; set; }
        public string Sha2Suffix { get; set; }
        public string Sha1Hash { get; set; }
    }
}