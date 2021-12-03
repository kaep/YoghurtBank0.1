using System;
using System.Collections.Generic;
using YoghurtBank.Data.Model;
using System.Net;
using System.Threading.Tasks;
using YoghurtBank.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace YoghurtBank.Services 
{
   
    public class CollaborationRequestRepository : ICollaborationRequestRepository
    {
        private readonly IYoghurtContext _context;
        public CollaborationRequestRepository(IYoghurtContext context)
        {
            _context = context;
        }

        public CollaborationRequestDetailsDTO Create(CollaborationRequestCreateDTO request)
        {
            //error handling i tilfælde af nulls på requester+requesteeeeeee
            var requester = (Student) _context.Users.Find(request.StudentId);
            var requestee = (Supervisor) _context.Users.Find(request.SupervisorId);
            var entity = new CollaborationRequest
            {
                Requester = requester,
                Requestee = requestee,
                Application = request.Application,
                Idea =  _context.Ideas.Find(request.IdeaId),
                Status = CollaborationRequestStatus.Waiting
            };

            _context.CollaborationRequests.Add(entity);
            _context.SaveChanges();
            
            return new CollaborationRequestDetailsDTO
            {
                StudentId = entity.Requester.Id,
                SupervisorId = entity.Requestee.Id,
                Status = entity.Status,
                Application = entity.Application,
            };
        }

        public async Task<CollaborationRequestDetailsDTO> CreateAsync(CollaborationRequestCreateDTO request)
        {
            var entity = new CollaborationRequest
            {
                Requester = (Student) await _context.Users.FindAsync(request.StudentId),
                Requestee = (Supervisor) await _context.Users.FindAsync(request.SupervisorId),
                Application = request.Application,
                Idea = await _context.Ideas.FindAsync(request.IdeaId),
                Status = CollaborationRequestStatus.Waiting
            };
            _context.CollaborationRequests.Add(entity);
            await _context.SaveChangesAsync();

            return new CollaborationRequestDetailsDTO
            {
                StudentId = entity.Requester.Id,
                SupervisorId = entity.Requestee.Id,
                Status = entity.Status,
                Application = entity.Application,
            };
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

        public async Task<CollaborationRequestDetailsDTO> AddCollaborationRequestAsync(CollaborationRequestCreateDTO requestCreateDTO)
        {
            var toBeAdded = new CollaborationRequest
            {
                //baaah casting necessary :( 
                
                Requester = (Student)  await _context.Users.FindAsync(requestCreateDTO.StudentId),
                Requestee = (Supervisor) await _context.Users.FindAsync(requestCreateDTO.SupervisorId),
                Idea = await _context.Ideas.FindAsync(requestCreateDTO.IdeaId),
                Application = requestCreateDTO.Application,
                Status = CollaborationRequestStatus.Waiting //denne skal vel være noget new eller ?
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
            
            return toBeReturned; 
        }

        public async Task<IEnumerable<CollaborationRequestDetailsDTO>> FindRequestsByIdeaAsync(int ideaId)
        {
            return await _context.CollaborationRequests.Where(c => c.Idea.Id == ideaId).Select(c => new CollaborationRequestDetailsDTO
            {
                StudentId = c.Requester.Id,
                SupervisorId = c.Requestee.Id,
                Application = c.Application,
                Status = c.Status
                
            }).ToListAsync();   
       
       
        }

        public (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}