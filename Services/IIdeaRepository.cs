using System;
using System.Collections.Generic;
using System.Text;
using System.Net.HttpStatusCode;

namespace YoghurtBank.Services
{
    public interface IIdeaRepository
    {
        (HttpStatusCode, Task<IEnumerable<IdeaDTO>>) FindIdeaBySupervisorAsync(int userId);
        (HttpStatusCode, Task<IdeaDetailsDTO>) FindIdeaDetailsAsync(int IdeaId);
        (HttpStatusCode, int) CreateIdea(IdeaCreateDTO idea);

    }
}
