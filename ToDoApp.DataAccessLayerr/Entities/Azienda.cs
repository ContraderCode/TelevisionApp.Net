﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.DataAccessLayer.Entities
{
    public partial class Azienda
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<Programma> Programmi {get; set; }
    }
}
