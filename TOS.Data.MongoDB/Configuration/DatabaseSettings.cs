﻿namespace TOS.Data.MongoDB.Configuration
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string Database { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }
        public bool UseCosmos { get; set; }
    }
}
