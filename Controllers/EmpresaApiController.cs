using AzaRRoide.Domain.Entities;
using AzaRRoide.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzaRRoide.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmpresaApiController : ControllerBase
    {
        private readonly IEmpresaIntegracao _empresaIntegracao;

        public EmpresaApiController(IEmpresaIntegracao empresaIntegracao)
        {
            _empresaIntegracao = empresaIntegracao;
        }

        [HttpGet("cnpj")]
        public async Task<ActionResult<EmpresaEntitie>> ListarDadosDaEmpresa(string cnpj)
        {
            try
            {
                EmpresaEntitie empresaEntitie = await _empresaIntegracao.ObterDadosDaEmpresa(cnpj);
                if (empresaEntitie == null)
                {
                    return BadRequest("Cnpj não encontrado");
                }
                return Ok(empresaEntitie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor: " + ex.Message);
            }
        }
    }
}
