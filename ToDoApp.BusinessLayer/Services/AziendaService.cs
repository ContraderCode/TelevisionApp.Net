using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.BusinessLayer.Exceptions;
using ToDoApp.BusinessLayer.Models;
using ToDoApp.BusinessLayer.Services.Interfaces;
using ToDoApp.DataAccessLayer;
using ToDoApp.DataAccessLayer.Entities;

namespace ToDoApp.BusinessLayer.Services
{
    public class AziendaService : IAziendaService
    {
        private readonly IMapper _mapper;
        private readonly TodoappContext _db;
        private readonly IRoleService _roleService;

        public AziendaService(IMapper mapper, TodoappContext db, IRoleService roleService)
        {
            this._mapper = mapper;
            this._db = db;
            this._roleService = roleService;
        }

        public async Task<bool> DeleteAzienda(long uid, long id)
        {
            if (!await _roleService.CheckIfUserWithIdHasRole(uid, Ruolo.AMMINISTRATORE)) throw new NoPermissionsException();
            var azienda = await _db.Aziendas.FindAsync(id);

            if(azienda == null)
            {
                return false;
            }
            this._db.Aziendas.Remove(azienda);  
            await this._db.SaveChangesAsync();  
            return true;
        }

        public async Task<IEnumerable<AziendaDTO>> getAll(long uid)
        {
            return await this._db.Aziendas
                  .ProjectTo<AziendaDTO>(this._mapper.ConfigurationProvider)
                  .ToListAsync();

        }

        public async Task<AziendaDTO> getAziendaById(long uid, long id)
        {
            var aziendaDto = await this._db.Aziendas.FindAsync(id);

            if(aziendaDto == null)
            {
                return null;
            }
            return this._mapper.Map<AziendaDTO>(aziendaDto);
        }

        public async Task<AziendaDTO> PostAzienda(long uid, AziendaDTO aziendaDTO)
        {
            var azienda = new Azienda
            {
                Name = aziendaDTO.Name,
                Programmi = aziendaDTO.Programmi
            };

            this._db.Aziendas.Add(azienda);

            var isDone = await this._db.SaveChangesAsync();

            if (isDone > 0)
            {
                return this._mapper.Map<AziendaDTO>(azienda);
            }
            return null;
        }

        public async Task<bool> UpdateAzienda(long uid, long id, AziendaDTO aziendaDTO)
        {
            var azienda = await this._db.Aziendas.FindAsync(id);  
            
            if(azienda == null)
            {
                Console.WriteLine("Azienda non trovata");
            }
            azienda.Name = aziendaDTO.Name;
            azienda.Programmi = azienda.Programmi;

            var isDone = await this._db.SaveChangesAsync();

            if(isDone > 0)
            {
                return true;
            }
            return false;   
        }
    }
}
