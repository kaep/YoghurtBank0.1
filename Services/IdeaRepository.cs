using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using YoghurtBank.Data.Model;
using YoghurtBank.Infrastructure;

namespace YoghurtBank.Services
{
    public class IdeaRepository : IIdeaRepository
    {
        private readonly IYoghurtContext _context;

        public IdeaRepository(IYoghurtContext context) {
            _context = context;
        }
        public async Task<IdeaDetailsDTO> CreateAsync(IdeaCreateDTO idea)
        {
            //husk noget null-checking 
            var entity = new Idea
            {
                Creator = (Supervisor) await _context.Users.FindAsync(idea.CreatorId),
                Title = idea.Title,
                Subject = idea.Subject,
                Description = idea.Description,
                AmountOfCollaborators = idea.AmountOfCollaborators,
                Open = idea.Open,
                TimeToComplete = idea.TimeToComplete,
                StartDate = idea.StartDate, 
                Type = idea.Type
            };

            _context.Ideas.Add(entity);
            await _context.SaveChangesAsync();

            return new IdeaDetailsDTO
            {
                Id = entity.Id,
                CreatorId = entity.Creator.Id,
                Title = entity.Title,
                Subject = entity.Subject,
                Posted = entity.Posted,
                Description = entity.Description,
                AmountOfCollaborators = entity.AmountOfCollaborators,
                Open = entity.Open,
                TimeToComplete = entity.TimeToComplete,
                StartDate = entity.StartDate
            };
        }

        public async Task<IdeaDetailsDTO> UpdateAsync(int id, IdeaUpdateDTO update)
        {
            var entity = await _context.Ideas.FindAsync(id);
            if(entity == null)
            {
                return null; //RETURN A STATUS INSTEAD
            }

            
            entity.Title = update.Title != null ? update.Title : entity.Title;
            entity.Subject = update.Subject != null ? update.Subject : entity.Subject;
            entity.Description = update.Description != null ? update.Description : entity.Description;
            entity.AmountOfCollaborators = update.AmountOfCollaborators != null ? update.AmountOfCollaborators : entity.AmountOfCollaborators;
            entity.Open = update.Open != null ? update.Open : entity.Open;
            entity.TimeToComplete = update.TimeToComplete != null ? update.TimeToComplete : entity.TimeToComplete;
            entity.StartDate = update.StartDate != null ? update.StartDate : entity.StartDate;
            entity.Type = update.Type != null ? update.Type : entity.Type;

            await _context.SaveChangesAsync();
            return new IdeaDetailsDTO
            {
                Id = entity.Id,
                CreatorId = entity.Creator.Id,
                Title = entity.Title,
                Subject = entity.Subject,
                Posted = entity.Posted,
                Description = entity.Description,
                AmountOfCollaborators = entity.AmountOfCollaborators,
                Open = entity.Open,
                TimeToComplete = entity.TimeToComplete,
                StartDate = entity.StartDate
            };
        }
        
        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _context.Ideas.FindAsync(id);
            if(entity == null)
            {
                return -1; //needs to be changed! 
            }
            _context.Ideas.Remove(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        

        public async Task<IdeaDetailsDTO> FindIdeaDetailsAsync(int IdeaId)
        {
           var idea = await _context.Ideas.FindAsync(IdeaId);
           
           //improve this -> status codes? 
           if(idea == null)
           {
               return null;
           }

            return new IdeaDetailsDTO
            {
                Id = idea.Id,
                Title = idea.Title,
                Subject = idea.Subject,
                Posted = idea.Posted,
                Description = idea.Description,
                AmountOfCollaborators = idea.AmountOfCollaborators,
                Open = idea.Open,
                TimeToComplete = idea.TimeToComplete,
                StartDate = idea.StartDate,
                CreatorId = idea.Creator.Id
            };
        }

        public async Task<(HttpStatusCode code, IEnumerable<IdeaDTO> list)> FindIdeasBySupervisorIdAsync(int userId)
        {
            var supervisor = (Supervisor) _context.Users.Find(userId);

            if (supervisor == null) 
            {
                return (HttpStatusCode.NotFound, null);
            } else 
            {
            var ideas = await _context.Ideas.Where(i => i.Creator.Id == userId).Select(i =>
            new IdeaDTO {
                Id = i.Id,
                Title = i.Title,
                Subject = i.Subject,
                Type = i.Type
            }).ToListAsync();
            
            return (HttpStatusCode.Accepted, ideas);
            }
            
        }
    }
}
