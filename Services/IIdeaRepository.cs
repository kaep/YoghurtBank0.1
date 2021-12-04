using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Services
{
    public interface IIdeaRepository
    {
        Task<(HttpStatusCode code, IEnumerable<IdeaDTO> list)> FindIdeasBySupervisorIdAsync(int userId);
        Task<IdeaDetailsDTO> FindIdeaDetailsAsync(int IdeaId);
        Task<IdeaDetailsDTO> CreateAsync(IdeaCreateDTO idea);

        Task<int> DeleteAsync(int id); 
        Task<IdeaDetailsDTO> UpdateAsync(int id, IdeaUpdateDTO update);
    }
}
