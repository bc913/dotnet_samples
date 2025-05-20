using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using SQLite;

var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BcanMyApp", "finance.db");



DapperRunner.Run(dbPath);


