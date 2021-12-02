using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Services
{
    public interface IIdeaRepository
    {
        Task<(HttpStatusCode, Task<IEnumerable<IdeaDTO>>)> FindIdeaBySupervisorAsync(int userId);
        (HttpStatusCode, Task<IdeaDetailsDTO>) FindIdeaDetailsAsync(int IdeaId);
        (HttpStatusCode, int) CreateIdea(IdeaCreateDTO idea);

    }
}
