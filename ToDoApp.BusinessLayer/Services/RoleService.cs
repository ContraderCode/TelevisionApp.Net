using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.BusinessLayer.Services.Interfaces;
using ToDoApp.DataAccessLayer;
using ToDoApp.DataAccessLayer.Entities;

namespace ToDoApp.BusinessLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly TodoappContext _dbContext;

        public RoleService(TodoappContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckIfUserWithIdHasRole(long uid, Ruolo ruolo)
        {
            var user = await _dbContext.Users.FindAsync(uid);
            await _dbContext.SaveChangesAsync();
            if (user.Ruolo == ruolo)
            {
                return true;
            }
            else return false;
        }
    }
}
