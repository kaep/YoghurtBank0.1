namespace YoghurtBank.ControllerTests
{
    public class IdeaControllerTests
    {
        private readonly IdeaController _controller;
        private readonly Mock<IIdeaRepository> _repoMock; 

        public IdeaControllerTests()
        {
            var logMock = new Mock<ILogger<IdeaController>>();
            _repoMock = new Mock<IIdeaRepository>();
            _controller = new IdeaController(logMock.Object, _repoMock.Object);

        }

        //skal laves om hvis vi vælger at bruge actionresults?
        [Fact]
        public async Task GetById_given_non_existing_id_returns_null()
        {
            #region Arrange
            var id = 666;
            IdeaDetailsDTO idea = null;

            _repoMock.Setup(m => m.FindIdeaDetailsAsync(id)).ReturnsAsync(idea);
            #endregion

            #region Act
            var result = await _controller.GetById(id);
            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task GetById_given_existing_id_returns_detailsDTO()
        {
            #region Arrange
            var id = 2;

            var IdeaDetailsDTO = new IdeaDetailsDTO
            {
                CreatorId = 1,
                Id = 2,
                Title = "Data Intelligence is good",
                Subject = "Data Intelligence",
                Posted = DateTime.Now,
                Description = "Data Intelligence gives value",
                AmountOfCollaborators = 1,
                Open = true,
                TimeToComplete = DateTime.Today - DateTime.Today,
                StartDate = DateTime.Now,
                Type = IdeaType.PhD
            };

            _repoMock.Setup(m => m.FindIdeaDetailsAsync(id)).ReturnsAsync(IdeaDetailsDTO);
            #endregion

            #region Act
            var result = await _controller.GetById(id);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(IdeaDetailsDTO, result);
            #endregion
        }

        [Fact]
        public async Task Post_given_valid_idea_returns_dto()
        {
            #region Arrange
            var idea = new IdeaCreateDTO();

            var details = new IdeaDetailsDTO {
                CreatorId = 2,
                Id = 2,
                Title = "Data Intelligence is good",
                Subject = "Data Intelligence",
                Posted = DateTime.Now,
                Description = "Data Intelligence gives value",
                AmountOfCollaborators = 1,
                Open = true,
                TimeToComplete = DateTime.Today - DateTime.Today,
                StartDate = DateTime.Now,
                Type = IdeaType.PhD
            };  

            _repoMock.Setup(m => m.CreateAsync(idea)).ReturnsAsync(details);
            #endregion

            #region Act
            var result = await _controller.Post(idea);
            #endregion

            #region Assert
            Assert.NotNull(result);
            #endregion
        }

        [Fact]
        public async Task Delete_given_valid_id_returns_id()
        {
            #region Arrange
            _repoMock.Setup(m => m.DeleteAsync(1)).ReturnsAsync(1);
            #endregion

            #region Act
            var result = await _controller.Delete(1);
            #endregion

            #region Assert
            Assert.Equal(1, result);
            #endregion
        }

        [Fact]
        public async Task Put_Given_ideaUpdateDTO_returns_updated_ideaDetailsDTO()
        {     
            var ideaUpdateDTO1 = new IdeaUpdateDTO 
            {
                Id = 1,
                Title = "Data Intelligence is good",
                Subject = "Data Intelligence",
                Description = "Data Intelligence gives value",
                AmountOfCollaborators = 5,
                Open = true,
                TimeToComplete = DateTime.Today - DateTime.Today,
                StartDate = DateTime.Now,
                Type = IdeaType.PhD
            }; 
            
            var details = new IdeaDetailsDTO();
             
            _repoMock.Setup(item => item.UpdateAsync(1, ideaUpdateDTO1)).ReturnsAsync(details);

            var result = await _controller.Put(1, ideaUpdateDTO1);
            
            Assert.Equal(details, result);
        }
    }
}