using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
    public class MemberSectionRepository : IMemberSectionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MemberSectionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRangeAsync(IEnumerable<MemberSection> memberSections)
          => await _dbContext.AddRangeAsync(memberSections);



        public async Task<string[]> GetSectionsByMemberName(string lastName, string firstName)
        => await _dbContext.MemberSections
                           .Include(ms => ms.Member)
                           .ThenInclude(m => m.MemberSections)
                           .Where(ms => ms.Member.LastName.Equals(lastName) &&
                                  ms.Member.FirstName.Equals(firstName))
                           .Select(s => s.Section.Name)
                           .ToArrayAsync();


    }
}