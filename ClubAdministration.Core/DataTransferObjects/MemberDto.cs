
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClubAdministration.Core.DataTransferObjects
{
    public class MemberDto : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;

        public int Id { get; set; }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public int CountSections { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString() => $"Id: {Id}; LastName: {LastName}; FirstName: {FirstName}; CountSections: {CountSections}";

    }
}
