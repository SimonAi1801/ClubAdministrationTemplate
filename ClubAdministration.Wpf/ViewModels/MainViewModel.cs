using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using ClubAdministration.Persistence;
using ClubAdministration.Wpf.Common;
using ClubAdministration.Wpf.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClubAdministration.Wpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Section> _sections;
        private ObservableCollection<MemberDto> _members;
        private MemberDto _selectedMember;
        private Section _selectedSection;

        public ObservableCollection<Section> Sections
        {
            get => _sections;
            set
            {
                _sections = value;
                OnPropertyChanged(nameof(Sections));
            }
        }

        public ObservableCollection<MemberDto> Members
        {
            get => _members;
            set
            {
                _members = value;
                OnPropertyChanged(nameof(Members));
            }
        }

        public MemberDto SelectedMember
        {
            get => _selectedMember;
            set
            {
                _selectedMember = value;
                OnPropertyChanged(nameof(SelectedMember));
            }
        }



        public Section SelectedSection
        {
            get => _selectedSection;
            set
            {
                _selectedSection = value;
                OnPropertyChanged(nameof(SelectedSection));
                ChangeSection().ContinueWith(_ => { });
            }
        }

        private async Task ChangeSection()
        {
            if (SelectedSection == null)
            {
                return;
            }

            using (IUnitOfWork uow = new UnitOfWork())
            {
                var members = await uow.MemberRepository.GetMembersBySectionIdAsync(SelectedSection.Id);
                Members = new ObservableCollection<MemberDto>(members);
                SelectedMember = members.FirstOrDefault();
            }
        }

        public MainViewModel(IWindowController windowController) : base(windowController)
        {
        }

        private async Task LoadDataAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                var sections = await uow.SectionRepository.GetAllAsync();
                Sections = new ObservableCollection<Section>(sections);

                SelectedSection = sections.FirstOrDefault();
                if (SelectedSection != null)
                {
                    var members = await uow.MemberRepository.GetMembersBySectionIdAsync(SelectedSection.Id);
                    Members = new ObservableCollection<MemberDto>(members);
                    SelectedMember = members.FirstOrDefault();
                }
            }

        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }

        public static async Task<MainViewModel> CreateAsync(IWindowController windowController)
        {
            var viewModel = new MainViewModel(windowController);
            await viewModel.LoadDataAsync();
            return viewModel;
        }
    }
}
