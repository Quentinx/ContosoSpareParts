﻿using SpareParts.Mobile.Common;
using SpareParts.Mobile.Services;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using GalaSoft.MvvmLight.Command;
using SpareParts.ApiModel.Vehicles;
using CustomVisionClient;
using SpareParts.Mobile.Models;
using CustomVisionClient.Models;

namespace SpareParts.Mobile.ViewModels
{
    public class AddHistoryViewModel : ViewModelBase
    {
        private readonly IContosoService contosoService;
        private readonly IMediaService mediaService;

        private GetVehicle vehicle;
        public GetVehicle Vehicle
        {
            get => vehicle;
            set => Set(ref vehicle, value);
        }

        private Recognition recognition;
        public Recognition Recognition
        {
            get => recognition;
            set => Set(ref recognition, value);
        }

        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set => Set(ref imagePath, value);
        }

        public AddHistoryViewModel(IContosoService contosoService, IMediaService mediaService)
        {
            this.contosoService = contosoService;
            this.mediaService = mediaService;

            CreateCommands();
        }

        private void CreateCommands()
        {
        }

        public override void Activate(object parameter)
        {
            var data = parameter as HistoryData;

            ImagePath = data.File.Path;
            Vehicle = data.Vehicle;

            base.Activate(parameter);
        }

        private async Task RecognizeAsync(MediaFile file, bool goBackOnError = false)
        {
            IsBusy = true;

            try
            {
                Recognition = null;
                //Recognition = await recognitionService.RecognizeAsync(file);

                file.Dispose();
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message, ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
