﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // add test data to the data file
            InitializeDataFileXML.AddTestData();

            // instantiate the controller
            Controller appContoller = new Controller();
        }
    }
}
