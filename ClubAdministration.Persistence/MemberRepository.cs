using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MemberRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string[]> GetAllAsync()
        => await _dbContext.Members
                           .OrderBy(m => m.LastName)
                           .ThenBy(m => m.FirstName)
                           .Select(m => $"{m.FirstName} {m.LastName}")
                           .ToArrayAsync();


        public async Task<Member> GetByIdAsync(int id)
        => await _dbContext.Members.SingleOrDefaultAsync(m => m.Id == id);

        public void Update(Member member)
        => _dbContext.Members.Update(member);
    }
}