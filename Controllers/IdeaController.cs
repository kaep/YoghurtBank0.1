using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using YoghurtBank.Data.Model;
using YoghurtBank.Services;
using System.Net;

namespace YoghurtBank.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class IdeaController : ControllerBase
    {
        private readonly ILogger<IdeaController> _logger;
        private readonly IIdeaRepository _repository;

        public IdeaController(ILogger<IdeaController> logger, IIdeaRepository repository)
        {
            _logger = logger;
            _repository = repository; 
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IdeaDetailsDTO), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IdeaDetailsDTO>> GetById(int id)
        {
            var result = await _repository.FindIdeaDetailsAsync(id);
            return result != null ? result : new NotFoundResult();
            //hvordan fungerer dette? I rasmus' kode ser det ud som om at 
            //ideadetailsdto slet ikke bliver returneret??? hjælp mig :( 
        }
    }
}