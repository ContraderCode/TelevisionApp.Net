using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.BusinessLayer.Services.Interfaces;
using ToDoApp.DataAccessLayer.Entities;

namespace ToDoApp.BusinessLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUserService _userService;

        public RoleService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> CheckIfUserWithIdHasRole(long uid, Ruolo ruolo)
        {
            var user = await _userService.GetUserById(uid);
            if (user.Ruolo == ruolo)
            {
                return true;
            }
            else return false;
        }
    }
}
