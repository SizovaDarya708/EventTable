using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace EventTable
{

    public static class AppSettings
    {
        public static string Url { get; set; } = "https://git.heroku.com/eventtable.git";

        public static string Name { get; set; } = "EventTablebot";

        public static string Key { get; set; } = ConfigurationManager.ConnectionStrings["Token"].ConnectionString;

        public static string DBConnection { get; } = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    }
}
