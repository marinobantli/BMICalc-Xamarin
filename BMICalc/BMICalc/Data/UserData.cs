using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMICalc.Data
{
    public class UserData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
