﻿namespace FinAccounting
{
    public class DatabaseSettings
    {
        public string Provider { get; }
        public string Host { get; }
        public string Database { get; } 
        public string Username { get; }
        public string Password { get; } = string.Empty;
        public string Port { get; } = "5432";
        public string Schema { get; } = "public";

        public DatabaseSettings(string provider, string host, string database, string username, 
                                string password = "", string port = "5432", string schema = "public")
        {
            Provider = provider;
            Host = host;
            Database = database;
            Username = username;
            Password = password;
            Port = port;
            Schema = schema;
        }
    }
}
