﻿using GeometryDashAPI;
using GeometryDashAPI.Data;
using GeometryDashAPI.Data.Models;
using GeometryDashAPI.Levels;
using GeometryDashAPI.Levels.Enums;
using GeometryDashAPI.Levels.GameObjects;
using GeometryDashAPI.Levels.GameObjects.Triggers;
using GeometryDashAPI.Memory;
using GeometryDashAPI.Server;
using GeometryDashAPI.Server.Enums;
using GeometryDashAPI.Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Examples
{
    //This class for the only test.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Call 'F'");
            F();
            Console.WriteLine("'F' called");
            Console.ReadKey();
        }

        private static void F()
        {
            var name = new GameServer().DownloadLevel(64144455).Name;
            Console.WriteLine(name);
            var local = new LocalLevels();
            var w = local.GetLevel("Test", 3);
            local.Remove(local.GetLevel("test", 0));
            local.Save();
        }
    }
}
