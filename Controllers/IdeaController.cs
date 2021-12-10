
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


        //vi har droppet actionresults for nuværende... keep it simple 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IdeaDetailsDTO), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<IdeaDetailsDTO> GetById(int id)
        {
            return await _repository.FindIdeaDetailsAsync(id);
            //hvordan fungerer dette? I rasmus' kode ser det ud som om at 
            //ideadetailsdto slet ikke bliver returneret??? hjælp mig :( 
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<IdeaDetailsDTO>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IEnumerable<IdeaDetailsDTO>> GetAll()
        {
            return await _repository.ReadAllAsync();
            //hvordan fungerer dette? I rasmus' kode ser det ud som om at 
            //ideadetailsdto slet ikke bliver returneret??? hjælp mig :( 
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            //der skal vel være noget logik der tjekker at det er korrekt bruger og den derfor godt må slettes? 
            return await _repository.DeleteAsync(id);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(IdeaDetailsDTO), 201)]
        public async Task<IdeaDetailsDTO> Post(IdeaCreateDTO idea)
        {
            return await _repository.CreateAsync(idea);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IdeaDetailsDTO> Put(int id, IdeaUpdateDTO update) 
        {
            var ideaToUpdate = await _repository.UpdateAsync(id, update);

            if (ideaToUpdate == null)
            {
                //returner en status, dette gøre metoden ikke inde i repo den returnere blot null. Der foretages dog allerede null tjek i repo metode.
                throw new NotImplementedException();
            } else 
            {
                return ideaToUpdate;
            }
        }
         
    }
}