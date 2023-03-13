using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.BusinessLayer.Models;

namespace ToDoApp.BusinessLayer.Services.Interfaces
{
    public interface IProgrammaService
    {
        Task<IEnumerable<ProgrammaDTO>> GetProgrammaDTOAsync();
        Task<ProgrammaDTO> GetProgrammaDTOAsyncById(int id);
        Task<ProgrammaDTO> PostProgrammaDTOAsync(CreaProgrammaDTO programmaDTO, long aziendaId, int uid);
        Task<bool> PutProgrammaDTOAsync(int id, CreaProgrammaDTO programmaDTO, int uid);
        Task<bool> DeleteProgrammaDTOAsync(int id, int uid);

        Task<IEnumerable<ProgrammaDTO>> ListaProgrammiPerOrario(String from, String to);
    }
}
