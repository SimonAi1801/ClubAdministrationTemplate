using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
    public interface IMemberRepository
    {
        Task<Member> GetByIdAsync(int id);

        void Update(Member member);

        Task<string[]> GetAllNamesAsync();

        Task<Member[]> GetAllAsync();

        Task<MemberDto[]> GetMembersBySectionIdAsync(int id);


    }
}
