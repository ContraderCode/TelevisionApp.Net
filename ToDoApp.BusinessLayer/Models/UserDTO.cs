﻿using System;
using System.Collections.Generic;
using ToDoApp.DataAccessLayer.Entities;

namespace ToDoApp.BusinessLayer.Models
{
    public  class UserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Indirizzo { get; set; }
        public DateTime DataNascita { get; set; }
        public List<ProgrammaDTO> listaPreferiti { get; set; }

        public Ruolo Ruolo { get; set; }

    }
}
