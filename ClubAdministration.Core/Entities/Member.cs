using GitStat.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClubAdministration.Core.Entities
{
    public class Member : EntityObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _firstName;
        private string _lastName;

        [Required]
        [MinLength(2, ErrorMessage = "FirstNames minimum length is 2")]
        public string FirstName
        {
            get => _firstName;
            set 
            { 
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        [Required]
        [MinLength(2, ErrorMessage = "FirstNames minimum length is 2")]
        public string LastName
        {
            get => _lastName;
            set 
            { 
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string FullName => $"{_firstName} {_lastName}";

        public ICollection<MemberSection> MemberSections { get; set; }

        public override string ToString() => $"Id: {Id}; LastName: {LastName}; FirstName: {FirstName}; MemberSections: {MemberSections?.Count}";

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
