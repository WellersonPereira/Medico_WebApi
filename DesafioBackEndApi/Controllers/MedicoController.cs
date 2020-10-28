using DesafioBackEndApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DesafioBackEndApi.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace DesafioBackEndApi.Controllers
{
    [Authorize]
    [Route("medico")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        /// <summary>
        /// Obter todos os médicos.
        /// </summary>
        /// <response code="200">A lista de médicos foi obtido com sucesso.</response>
        /// <response code="500">Ocorreu um erro ao obter a lista de médicos.</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Medico>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Medico>>> Get([FromServices] MedicoContext context)
        {
            var medicos = await context.Medicos
                .ToListAsync();
            return medicos;
        }

        /// <summary>
        /// Obter um médico específico pela ID.
        /// </summary>
        /// <param name="id">ID do médico</param>
        /// <response code="200">o médico foi obtido com sucesso.</response>
        /// <response code="404">Não foi encontrado o médico com o ID especificado</response>
        /// <response code="500">Ocorreu um erro ao obter o médico.</response>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(Medico), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Medico>>> GetById([FromServices] MedicoContext context, Guid id)
        {
            var medicos = await context.Medicos
                .FirstOrDefaultAsync(x => x.Id == id);
            return Ok(medicos);
        }

        /// <summary>
        /// Obter uma lista de médicos pela Especialidade.
        /// </summary>
        /// <param name="especialidade">Especialidade do médico</param>
        /// <response code="200">A lista de médicos foi obtido com sucesso.</response>
        /// <response code="404">Não foi encontrado médico com a especialidade especificada</response>
        /// <response code="500">Ocorreu um erro ao obter a lista de médicos.</response>
        /// <returns></returns>
        [HttpGet("{especialidade}")]
        [ProducesResponseType(typeof(List<Medico>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Medico>>> GetByEspecialidade([FromServices] MedicoContext context, string especialidade)
        {
            var medicos = await context.Medicos.Where(x => x.Especialidades.Contains(especialidade)).ToListAsync();
            return Ok(medicos);
        }

        /// <summary>
        /// Alterar um médico.
        /// </summary>
        /// <param name="model">Modelo de médico.</param>
        /// <response code = "200">O médico foi alterado com sucesso</response>
        /// <response code = "400">O modelo de médico enviado é inválido</response>
        /// <response code="404">Não foi encontrado o médico com o ID especificado</response>
        /// <response code="500">Ocorreu um erro ao alterar o médico.</response>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Medico>> Post([FromServices] MedicoContext context,
            [FromBody] Medico model)
        {
            if (ModelState.IsValid)
            {
                context.Medicos.Add(model);
                await context.SaveChangesAsync();
                return Ok(model.Id);
            }
            else
                return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletar médico.
        /// </summary>
        /// <param name="id">ID do médico</param>
        /// <response code = "200">O médico foi deletado com sucesso</response>
        /// <response code="404">Não foi encontrado o médico com o ID especificado</response>
        /// <response code="500">Ocorreu um erro ao deletar o médico.</response>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Medico>> Delete ([FromServices] MedicoContext context, Guid id)
        {
            var medico = await context.Medicos.FirstAsync(x => x.Id == id);
            context.Remove(medico);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
