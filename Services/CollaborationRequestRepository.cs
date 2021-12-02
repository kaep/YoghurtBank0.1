using System;
using System.Collections.Generic;
using YoghurtBank.Data.Model;
using System.Net;
using System.Threading.Tasks;
using YoghurtBank.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace YoghurtBank.Services 
{
   
    public class CollaborationRequestRepository : ICollaborationRequestRepository
    {
        private readonly IYoghurtContext _context;
        public CollaborationRequestRepository(IYoghurtContext context)
        {
            _context = context;
        }
        
        //er det forkert at de kender til hinanden
        public async Task<CollaborationRequestDetailsDTO> FindById(int id)
        {
            var collabRequest = await _context.CollaborationRequests.FindAsync(id);

            if(collabRequest == null){ 
                return null;
                }
            return new CollaborationRequestDetailsDTO
            {
                StudentId = _context.Users.FindAsync(collabRequest.Id).Result.Id,
                SupervisorId = _context.Users.FindAsync(collabRequest.Id).Result.Id,
                Status = collabRequest.Status,
                Application = collabRequest.Application
            };
        }

        public async Task<(HttpStatusCode, CollaborationRequestDetailsDTO)> AddCollaborationRequestAsync(CollaborationRequestCreateDTO requestCreateDTO)
        {
            var toBeAdded = new CollaborationRequest
            {
                //baaah casting necessary :( 
                Requester = (Student) _context.Users.Find(requestCreateDTO.StudentId),
                Requestee = (Supervisor) _context.Users.Find(requestCreateDTO.SupervisorId),
                Idea = _context.Ideas.Find(requestCreateDTO.IdeaId),
            };

            _context.CollaborationRequests.Add(toBeAdded);
            await _context.SaveChangesAsync();

            var toBeReturned = new CollaborationRequestDetailsDTO
            {
                StudentId = toBeAdded.Requester.Id,
                SupervisorId = toBeAdded.Requestee.Id,
                Application = toBeAdded.Application,
                Status = toBeAdded.Status //denne skal vel sættes til new som default eller noget, right?
            };
            
            return (HttpStatusCode.Processing, toBeReturned); 
        }

        public async Task<IEnumerable<CollaborationRequestDetailsDTO>> FindRequestsByIdeaAsync(int ideaId)
        {
            return await _context.CollaborationRequests.Where(c => c.Idea.Id == ideaId).Select(c => new CollaborationRequestDetailsDTO
            {
                StudentId = c.Requester.Id,
                SupervisorId = c.Requestee.Id,
                Application = c.Application,
                Status = c.Status}).ToListAsync();   
        }

        public (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}