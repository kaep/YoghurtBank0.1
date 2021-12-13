
namespace YoghurtBank.Services 
{
   
    public class CollaborationRequestRepository : ICollaborationRequestRepository
    {
        private readonly IYoghurtContext _context;
        public CollaborationRequestRepository(IYoghurtContext context)
        {
            _context = context;
        }
        
        public async Task<(Status status, CollaborationRequestDetailsDTO dto)> CreateAsync(CollaborationRequestCreateDTO request)
        {
            var student = (Student) await _context.Users.FindAsync(request.StudentId);
            if(student == null) return (Status.BadRequest, null);
            var super = (Supervisor) await _context.Users.FindAsync(request.SupervisorId);
            if(super == null) return (Status.BadRequest, null);

            var entity = new CollaborationRequest
            {
                Requester = student,
                Requestee = super,
                Application = request.Application,
                Idea = await _context.Ideas.FindAsync(request.IdeaId),
                Status = CollaborationRequestStatus.Waiting // bliver status ikke sat automatisk
            };
            
            _context.CollaborationRequests.Add(entity);
            super.CollaborationRequests.Add(entity);
            student.CollaborationRequests.Add(entity);
            await _context.SaveChangesAsync();

            var toBeReturned = new CollaborationRequestDetailsDTO
            {
                StudentId = entity.Requester.Id,
                SupervisorId = entity.Requestee.Id,
                Status = entity.Status,
                Application = entity.Application,
            };

            return (Status.Created, toBeReturned);
        }


        public async Task<(Status status, CollaborationRequestDetailsDTO dto)> FindById(int id)
        {
            var collabRequest = await _context.CollaborationRequests.FindAsync(id);

            if(collabRequest == null)
            { 
                return (Status.NotFound, null);
            }
            
            
            var toBeReturned = new CollaborationRequestDetailsDTO
            {
                StudentId = _context.Users.FindAsync(collabRequest.Id).Result.Id,
                SupervisorId = _context.Users.FindAsync(collabRequest.Id).Result.Id,
                Status = collabRequest.Status,
                Application = collabRequest.Application
            };

            return (Status.Found, toBeReturned);
        }

        public async Task<(Status status, int id)> DeleteAsync(int id)
        {
            var entity = await _context.CollaborationRequests.FindAsync(id);
            if (entity == null)
            {
                return (Status.NotFound, null);
            }

            _context.CollaborationRequests.Remove(entity);
            await _context.SaveChangesAsync();
            return (Status.Deleted, entity.Id);
        }


        public async Task<(Status status, IReadOnlyCollection<CollaborationRequestDetailsDTO> requests)> FindRequestsByIdeaAsync(int ideaId)
        {
            var requests = await _context.CollaborationRequests.Where(c => c.Idea.Id == ideaId).Select(c => new CollaborationRequestDetailsDTO
            {
                StudentId = c.Requester.Id,
                SupervisorId = c.Requestee.Id,
                Application = c.Application,
                Status = c.Status
                
            }).ToListAsync();

            if(requests == null)
            {
                return (Status.NotFound, null);
            }

            return (Status.Found, requests.AsReadOnly());

        }

        public async Task<(Status status, CollaborationRequestDetailsDTO dto)> UpdateAsync(int id, CollaborationRequestUpdateDTO updateRequest)
        {
            //TODO should more properties be update-able? 
            
            var entity = await _context.CollaborationRequests.FindAsync(id);
            
            if (entity == null)
            {
                return (Status.NotFound, null);
            }
            
            entity.Status = updateRequest.Status;
            await _context.SaveChangesAsync();
            var toBeReturned = new CollaborationRequestDetailsDTO
            {
                Status = entity.Status,
                Application = entity.Application,
                StudentId = entity.Requester.Id,
                SupervisorId = entity.Requestee.Id
            };

            return (Status.Updated, toBeReturned);
        }


        public async Task<(Status status, IReadOnlyCollection<CollaborationRequestDetailsDTO> requests)> FindRequestsBySupervisorAsync(int supervisorId)
        {
            //overvej type checking, så vi er sikre på at metoden ikke bruges til at finde den forkerte type user
            
            var listOfUsers = await _context.CollaborationRequests.Where(c => c.Requestee.Id == supervisorId).Select(c => new CollaborationRequestDetailsDTO
            {
                StudentId = c.Requester.Id,
                SupervisorId = c.Requestee.Id,
                Application = c.Application,
                Status = c.Status
                
            }).ToListAsync();

            if (listOfUsers == null) 
            {
                return (Status.NotFound, null);
            }
            return (Status.Found, listOfUsers.AsReadOnly());
        }

        public async Task<(Status status, IReadOnlyCollection<CollaborationRequestDetailsDTO> requests)> FindRequestsByStudentAsync(int studentId)
        {
            //overvej type checking, så vi er sikre på at metoden ikke bruges til at finde den forkerte type user

            var listOfUsers = await _context.CollaborationRequests.Where(c => c.Requester.Id == studentId).Select(c => new CollaborationRequestDetailsDTO
            {
                StudentId = c.Requester.Id,
                SupervisorId = c.Requestee.Id,
                Application = c.Application,
                Status = c.Status
                
            }).ToListAsync();

            if (listOfUsers == null) 
            {
                return (Status.NotFound, null);
            }

            return (Status.Found, listOfUsers.AsReadOnly());
        }
    }
}
