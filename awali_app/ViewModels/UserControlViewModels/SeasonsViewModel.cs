using Airfare.Commands;
using Airfare.DataContext;
using Airfare.Models;
using Airfare.Servies;
using Airfare.ViewModels.DialogViewModels;
using Airfare.Views.Dialogs;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Airfare.ViewModels.UserControlViewModels
{
    public class SeasonsViewModel:BaseViewModel, IDisposable
    {
        #region Stylize
        private bool _LoadingCircle;

        public bool LoadingCircle
        {
            get { return _LoadingCircle; }
            set { _LoadingCircle = value;
                OnPropertyChanged(nameof(LoadingCircle));
            }
        }

        private bool _LoadingLine;

        public bool LoadingLine
        {
            get { return _LoadingLine; }
            set
            {
                _LoadingLine = value;
                OnPropertyChanged(nameof(LoadingLine));
            }
        }

        #endregion

        #region Properties

        private ObservableCollection<SeasonModel> _SeasonsList;

        public ObservableCollection<SeasonModel> SeasonsList
        {
            get { return _SeasonsList; }
            set { _SeasonsList = value;
                OnPropertyChanged(nameof(SeasonsList));
            }
        }

        private SeasonModel _SelectedSeason;

        public SeasonModel SelectedSeason
        {
            get { return _SelectedSeason; }
            set { _SelectedSeason = value;
                OnPropertyChanged(nameof(SelectedSeason));
            }
        }

        private SeasonModel _CurrentSeason;

        public SeasonModel CurrentSeason
        {
            get { return _CurrentSeason; }
            set { _CurrentSeason = value;
                OnPropertyChanged(nameof(CurrentSeason));
            }
        }

        #endregion

        #region Services
        private readonly SeasonServices seasonServices = new SeasonServices();
        #endregion

        #region Commands
        public RelayCommand AddSeasonCommand { get; set; }
        public RelayCommand RemoveSeasonCommand { get; set; }
        public RelayCommand ShowSeasonDialogCommand { get; set; }
        public RelayCommand EndSeasonCommand { get; set; }
        #endregion

        public SeasonsViewModel()
        {
            InitData();
            AddSeasonCommand = new(AddSeason);
            RemoveSeasonCommand = new(RemoveSeason);
            ShowSeasonDialogCommand = new(ShowSeasonDialog);
            EndSeasonCommand = new(EndSeason);
        }

        #region Methods
        private async void EndSeason()
        {
            try
            {
                LoadingLine = true;
                bool result = await Dialog.Show<YesNoDialog>().Initialize<YesNoDialogViewModel>(vm => vm.Description = "هل أنت متأكد أنك تريد ختم هذا الموسم؟").GetResultAsync<bool>();
                if (result)
                {
                    SelectedSeason.HasEnded = true;
                    SelectedSeason.EndDate = DateTime.Now;
                    await seasonServices.UpdateSeason(SelectedSeason);
                    if (!seasonServices.Error)
                    {
                        SeasonModel? createdSeason = await Dialog.Show<SeasonDialog>().Initialize<SeasonDialogViewModel>(vm => vm.Season = new()).GetResultAsync<SeasonModel>();
                        if (createdSeason != null)
                            InitData();
                        Growl.Success("تمت ختم الموسم بنجاح");
                    }
                    else
                    {
                        Growl.Error("\n:خطأ\n" + seasonServices.ErrorMessage);
                    }
                }
                LoadingLine = false;
            }
            catch (Exception)
            {
                LoadingLine = false;
                Growl.Error("An error occurred while ending season");
            }
           
        }

        private async void ShowSeasonDialog()
        {
            try
            {
                SeasonModel? result = await Dialog.Show<SeasonDialog>().Initialize<SeasonDialogViewModel>(vm => { vm.Season = SelectedSeason; vm.UpdateMode = true; }).GetResultAsync<SeasonModel>();

                SelectedSeason = result;
            }
            catch (Exception)
            {

                Growl.Error("An error occurred while updating season");
            }
           
        }

        private async void RemoveSeason()
        {
            try
            {
                LoadingLine = true;
                bool result = await Dialog.Show<YesNoDialog>().Initialize<YesNoDialogViewModel>(vm => vm.Description = "هل أنت متأكد أنك تريد حذف هذا الموسم؟").GetResultAsync<bool>();
                if (result)
                {
                    await seasonServices.RemoveSeason(SelectedSeason);
                    if (!seasonServices.Error)
                    {
                        InitData();
                        Growl.Success("تم حذف الموسم بنجاح");
                    }
                    else
                    {
                        Growl.Error("\n:خطأ\n" + seasonServices.ErrorMessage);
                    }
                }
                LoadingLine = false;
            }
            catch (Exception)
            {
                LoadingLine = false;
                Growl.Error("An error has occurred while trying to remove the season");
            }
           
        }

        private async void InitData()
        {
            try
            {
                LoadingLine = true;
                CurrentSeason = new SeasonModel();
                SeasonsList = new ObservableCollection<SeasonModel>(await seasonServices.GetAllSeasons());
                if (Configuration.CurrentSeason is null)
                {
                    SeasonModel? createdSeason = await Dialog.Show<SeasonDialog>().Initialize<SeasonDialogViewModel>(vm => vm.Season = new()).GetResultAsync<SeasonModel>();
                    if (createdSeason != null)
                    {
                        Configuration.CurrentSeason = createdSeason;
                        InitData();
                    }
                }
                LoadingLine = false;
            }
            catch (Exception)
            {
                LoadingLine = false;
                Growl.Error("An error has occurred while InitData of SeasonView");
            }
            
        }

        private async void AddSeason()
        {
            try
            {
                LoadingCircle = true;
                await seasonServices.AddSeason(CurrentSeason);
                if (!seasonServices.Error)
                {

                    SeasonsList.Add(CurrentSeason);
                    Configuration.CurrentSeason = CurrentSeason;
                    InitData();
                    Growl.Success("تمت إضافة الموسم بنجاح");
                }
                else
                {
                    Growl.Error("\n:خطأ\n" + seasonServices.ErrorMessage);
                }
                LoadingCircle = false;

            }
            catch (Exception)
            {
                LoadingCircle = false;
                Growl.Error("An error has occurred while trying to add a season");
            }
        }
        public void Dispose()
        {
            GC.Collect();
        }
        #endregion

    }
}
