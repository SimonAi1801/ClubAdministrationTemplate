using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
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

        public async Task<Member[]> GetAllAsync()
        => await _dbContext.Members.ToArrayAsync();

        public async Task<string[]> GetAllNamesAsync()
        => await _dbContext.Members
                           .OrderBy(m => m.LastName)
                           .ThenBy(m => m.FirstName)
                           .Select(m => $"{m.FirstName} {m.LastName}")
                           .ToArrayAsync();


        public async Task<Member> GetByIdAsync(int id)
        => await _dbContext.Members.SingleOrDefaultAsync(m => m.Id == id);

        public void Update(Member member)
        => _dbContext.Members.Update(member);

        public async Task<MemberDto[]> GetMembersBySectionIdAsync(int id)
        => (await _dbContext.MemberSections
                   .Where(ms => ms.SectionId == id)
                   .Include(m => m.Member)
                   .ToArrayAsync())
                   .GroupBy(m => m.Member)
                   .Select((_, idx) => new MemberDto
                   {
                       FirstName = _.Key.FirstName,
                       LastName = _.Key.LastName,
                       CountSections = _.Key.MemberSections.Where(ms => ms.MemberId == _.Key.Id).Count()
                   })
                   .OrderBy(m => m.LastName)
                   .ThenBy(m => m.FirstName)
                   .ToArray();
    }
}