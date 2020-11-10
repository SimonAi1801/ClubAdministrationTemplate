using ClubAdministration.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence.Validations
{
    public class DuplicateMemberValidation : ValidationAttribute
    {
        private readonly UnitOfWork _unitOfWork;

        public DuplicateMemberValidation(UnitOfWork unitOfWork) : base()
        {
            _unitOfWork = unitOfWork;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is Member member))
            {
                throw new ArgumentException("Value is not a member", nameof(value));
            }
            var memberInDb = _unitOfWork.MemberRepository.GetByIdAsync(member.Id).Result;

            if (memberInDb != null)
            {
                return new ValidationResult("Es existiert bereits ein Mitglied mit dem Namen");   
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
