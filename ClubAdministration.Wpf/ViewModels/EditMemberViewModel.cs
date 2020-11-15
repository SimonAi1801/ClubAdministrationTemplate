using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using ClubAdministration.Persistence;
using ClubAdministration.Wpf.Common;
using ClubAdministration.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClubAdministration.Wpf.ViewModels
{
    public class EditMemberViewModel : BaseViewModel
    {
        private MemberDto _member;
        private string _lastName;
        private string _firstName;
        private ICommand _cmdSaveMember;

        public EditMemberViewModel(IWindowController controller, MemberDto member) : base(controller)
        {
            _member = member;
            _firstName = member.FirstName;
            _lastName = member.LastName;
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (LastName.Length < 2)
            {
                yield return new ValidationResult("Lastname must be at last two characters long", new string[] { nameof(LastName) });
            }
        }

        public ICommand CmdSaveMember
        {
            get 
            {
                if (_cmdSaveMember == null)
                {
                    _cmdSaveMember = new RelayCommand(
                        execute: async _ => await SaveMemberAsync(),
                        canExecute: _ => IsValid);
                }
                return _cmdSaveMember; 
            }
        }

        private async Task SaveMemberAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    Member memberInDb = await uow.MemberRepository.GetByIdAsync(_member.Id);
                    memberInDb.FirstName = _firstName;
                    memberInDb.LastName = _lastName;

                    uow.MemberRepository.Update(memberInDb);
                    await uow.SaveChangesAsync();
                }
                catch (ValidationException ex)
                {
                    if (ex.Value is IEnumerable<string> properties)
                    {
                        foreach (var propertyName in properties)
                        {
                            AddErrorsToProperty(propertyName, new List<string> { ex.ValidationResult.ErrorMessage});
                        }
                    }
                    else
                    {
                        DbError = ex.ValidationResult.ToString();
                    }
                }

            }
        }
    }
}
