using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Wpf.Common;
using ClubAdministration.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubAdministration.Wpf.ViewModels
{
    public class EditMemberViewModel : BaseViewModel
    {
        private MemberDto _member;
        private string _lastName;
        private string _firstName;
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
    }
}
