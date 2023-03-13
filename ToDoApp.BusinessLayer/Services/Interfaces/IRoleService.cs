using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DataAccessLayer.Entities;

namespace ToDoApp.BusinessLayer.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Boolean> CheckIfUserWithIdHasRole(long uid, Ruolo ruolo);
    }
}
