using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Services
{
    public interface IIdeaRepository
    {
        (HttpStatusCode, IEnumerable<IdeaDTO>) FindIdeaBySupervisorAsync(int userId);
        Task<(HttpStatusCode, IdeaDetailsDTO)> FindIdeaDetailsAsync(int IdeaId);
        (HttpStatusCode, int) CreateIdea(IdeaCreateDTO idea);

    }
}
