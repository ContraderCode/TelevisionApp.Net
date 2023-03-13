using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.BusinessLayer.Models;

namespace ToDoApp.BusinessLayer.Services.Interfaces
{
    public interface IAziendaService
    {
        Task<IEnumerable<AziendaDTO>> getAll(long uid);

        Task<AziendaDTO> getAziendaById(long uid, long id);
        Task<AziendaDTO> PostAzienda(long uid, AziendaDTO aziendaDTO);

        Task<bool> DeleteAzienda(long uid, long id);

        Task<bool> UpdateAzienda(long uid, long id, AziendaDTO aziendaDTO);
    }
}
