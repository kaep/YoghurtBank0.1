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
            //husk null-checking
            
            var entity = new CollaborationRequest
            {
                Requester = (Student) await _context.Users.FindAsync(request.StudentId),
                Requestee = (Supervisor) await _context.Users.FindAsync(request.SupervisorId),
                Application = request.Application,
                Idea = await _context.Ideas.FindAsync(request.IdeaId),
                Status = CollaborationRequestStatus.Waiting // bliver status ikke sat automatisk
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


        public async Task<CollaborationRequestDetailsDTO> FindById(int id)
        {
            var collabRequest = await _context.CollaborationRequests.FindAsync(id);

            if(collabRequest == null)
            { 
                return null;
            }
            
            //husk null-checking
            
            return new CollaborationRequestDetailsDTO
            {
                StudentId = _context.Users.FindAsync(collabRequest.Id).Result.Id,
                SupervisorId = _context.Users.FindAsync(collabRequest.Id).Result.Id,
                Status = collabRequest.Status,
                Application = collabRequest.Application
            };
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _context.CollaborationRequests.FindAsync(id);
            if (entity == null)
            {
                return -1; //BAD! create a status instead
            }

            _context.CollaborationRequests.Remove(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }


        public async Task<IEnumerable<CollaborationRequestDetailsDTO>> FindRequestsByIdeaAsync(int ideaId)
        {
            //husk null-checking på c.idea 
            return await _context.CollaborationRequests.Where(c => c.Idea.Id == ideaId).Select(c => new CollaborationRequestDetailsDTO
            {
                StudentId = c.Requester.Id,
                SupervisorId = c.Requestee.Id,
                Application = c.Application,
                Status = c.Status
                
            }).ToListAsync();   
        }

        public async Task<CollaborationRequestDetailsDTO> UpdateAsync(int id, CollaborationRequestUpdateDTO updateRequest)
        {
            //TODO should more properties be update-able? 
            
            var entity = await _context.CollaborationRequests.FindAsync(id);
            if (entity == null)
            {
                return null;  //RETURN A STATUS INSTEAD
            }

            entity.Status = updateRequest.Status;
            await _context.SaveChangesAsync();
            return new CollaborationRequestDetailsDTO
            {
                Status = entity.Status,
                Application = entity.Application,
                StudentId = entity.Requester.Id,
                SupervisorId = entity.Requestee.Id
            };
        }


        public (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}