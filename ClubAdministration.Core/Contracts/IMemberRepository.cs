﻿using ClubAdministration.Core.Entities;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
  public interface IMemberRepository
  {
        Task<Member> GetByIdAsync(int id);
  }
}