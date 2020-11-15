using ClubAdministration.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace ClubAdministration.ImportConsole
{
    public class ImportController
    {
        const string FileName = "members.csv";

        public async static Task<MemberSection[]> ReadFromCsvAsync()
        {
            var matrix = await MyFile.ReadStringMatrixFromCsvAsync(FileName, false);

            var members = matrix.GroupBy(line => line[0] + ' ' + line[1])
                                  .Select(grp => new Member()
                                  {
                                      LastName = grp.Key.Split(' ')[0],
                                      FirstName = grp.Key.Split(' ')[1]
                                  })
                                  .ToArray();

            var sections = matrix.GroupBy(line => line[2])
                                 .Select(grp => new Section()
                                 {
                                     Name = grp.Key
                                 })
                                 .ToArray();


            var memberSections = matrix
                                .Select(line => new MemberSection()
                                {
                                    Member = members.Single(m => m.LastName.Equals(line[0]) && m.FirstName.Equals(line[1])),
                                    Section = sections.Single(s => s.Name.Equals(line[2]))
                                })
                                .ToArray();
            return memberSections;
        }

    }
}
