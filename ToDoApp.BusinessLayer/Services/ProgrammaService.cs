using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ToDoApp.BusinessLayer.Exceptions;
using ToDoApp.BusinessLayer.Models;
using ToDoApp.BusinessLayer.Services.Interfaces;
using ToDoApp.DataAccessLayer;
using ToDoApp.DataAccessLayer.Entities;

namespace ToDoApp.BusinessLayer.Services
{
    public class ProgrammaService : IProgrammaService
    {
        private readonly IMapper _mapper;
        private readonly TodoappContext _db;
        private readonly IRoleService _roleService;

        public ProgrammaService(IMapper mapper, TodoappContext db, IRoleService roleService)
        {
            _mapper = mapper;
            _db = db;
            _roleService = roleService;
        }

        public async Task<bool> DeleteProgrammaDTOAsync(int id, int uid)
        {
            if (!await _roleService.CheckIfUserWithIdHasRole(uid, Ruolo.AMMINISTRATORE)|| !await _roleService.CheckIfUserWithIdHasRole(uid, Ruolo.ADMIN)) throw new NoPermissionsException();

            var dto = await _db.Programma.FindAsync(id);
            if(dto is not null)
            {
                _db.Programma.Remove(dto);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<ProgrammaDTO>> GetProgrammaDTOAsync()
        {
            return await this._db.Programma
                    .OrderByDescending(item => item.Id)
                    .ProjectTo<ProgrammaDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<ProgrammaDTO> GetProgrammaDTOAsyncById(int id)
        {
            var dto = await this._db.Programma.FindAsync(id);

            if (dto is not null)
            {
                return this._mapper.Map<ProgrammaDTO>(dto);
            }
            return null;
        }

        public async Task<ProgrammaDTO> PostProgrammaDTOAsync(CreaProgrammaDTO programmaDTO, long aziendaId, int uid)
        {
            if (!await _roleService.CheckIfUserWithIdHasRole(uid, Ruolo.AMMINISTRATORE) || !await _roleService.CheckIfUserWithIdHasRole(uid, Ruolo.ADMIN)) throw new NoPermissionsException();

            var azienda = await this._db.Aziendas.FindAsync(aziendaId);

            if (azienda == null)
            {
                return null;
            }

            var programma = new Programma
            {
                Nome = programmaDTO.Nome,
                Orario = programmaDTO.Orario,
                Azienda = azienda
            };

             _db.Programma.Add(programma);
            var isDone = await this._db.SaveChangesAsync();

            if (isDone > 0)
            {
                return this._mapper.Map<ProgrammaDTO>(programma);
            }

            return null;

        }

        public async Task<bool> PutProgrammaDTOAsync(int id, CreaProgrammaDTO dto, int uid)
        {
            if (!await _roleService.CheckIfUserWithIdHasRole(uid, Ruolo.AMMINISTRATORE) || !await _roleService.CheckIfUserWithIdHasRole(uid, Ruolo.ADMIN)) throw new NoPermissionsException();

            var item = await this._db.Programma.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            item.Nome = dto.Nome;
            item.Orario = dto.Orario;

            var isDone = await this._db.SaveChangesAsync();

            if (isDone > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ProgrammaDTO>> ListaProgrammiPerOrario(String from, String to)
        {
           return await this._db.Programma
                    .Where(item => item.Orario >= DateTime.Parse(from) && item.Orario <= DateTime.Parse(to))
                    .OrderByDescending(item => item.Orario)
                    .ProjectTo<ProgrammaDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

    }
}
