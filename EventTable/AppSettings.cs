using System;
using System.Collections.Generic;
using System.Text;

namespace EventTable
{
    public static class AppSettings
    {
        public static string Url { get; set; } = "https://git.heroku.com/eventtable.git";

        public static string Name { get; set; } = "EventTablebot";

        public static string Key { get; set; } = "1054347333:AAHsNZrggwnqFPV3vmgi1exYxMd5hMsuEH4";

        public static string DBConnection { get; set; } = "Database=event_table;Host=localhost;Username=postgres;Password=8520;Persist Security Info=True";

    }
}
