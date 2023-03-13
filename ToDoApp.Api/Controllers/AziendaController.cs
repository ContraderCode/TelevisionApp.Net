using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.BusinessLayer.Models;
using ToDoApp.BusinessLayer.Services.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AziendaController : ControllerBase
    {
        private readonly IAziendaService _aziendaService;

        public AziendaController(IAziendaService aziendaService)
        {
            this._aziendaService = aziendaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AziendaDTO>>> getAll(long uid)
        {
            return this.Ok(await this._aziendaService.getAll(uid));    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AziendaDTO>> getAziendaByid(long uid, long id)
        {
            return this.Ok(await this._aziendaService.getAziendaById(uid, id));
        }

        //ADMIN e AZIENDA

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> deleteAzienda(long uid, long id)
        {
            return this.Ok(await this._aziendaService.DeleteAzienda(uid, id));   
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> updateAzienda(long uid, long id, AziendaDTO aziendaDTO)
        {
            return this.Ok(await this._aziendaService.UpdateAzienda(uid, id, aziendaDTO));   
        }

        [HttpPost]
        public async Task<ActionResult<AziendaDTO>> postAzienda(long uid, AziendaDTO aziendaDTO)
        {
            return this.Ok(await this._aziendaService.PostAzienda(uid, aziendaDTO));   
        }


    }
}
