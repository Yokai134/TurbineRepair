﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TurbineRepair.Infrastructure;

namespace TurbineRepair.ViewModel
{
    internal class RequestVM : Base.ViewModel
    {

        #region Property
        private List<TurbineRequest> _requests;
        public List<TurbineRequest> Requests
        {
            get => _requests;
            set => Set(ref _requests, value);
        }

        private object _itemReuest;
        public object ItemReuest
        {
            get => _itemReuest;
            set => Set(ref _itemReuest, value);
        }

        private string failedAddOrUpdateContent;
        public string FailedAddOrUpdateContent { get => failedAddOrUpdateContent; set => Set(ref failedAddOrUpdateContent, value); }

        private decimal foregroundFailedMessage;
        public decimal ForegroundFailedMessage { get => foregroundFailedMessage; set => Set(ref foregroundFailedMessage, value); }

        private TurbineRequest? _selectRequest;
        public TurbineRequest? SelectRequest
        {
            get => _selectRequest;
            set => Set(ref _selectRequest, value);
        }
        #endregion

        #region Command
        public ICommand RequestAdd { get; }
        private bool CanRequestAddExecute(object parameter) => true;
        private void OnRequestAddExecute(object parametr)
        {
            MainWindowViewModel.main.UpdRequest = null;
            MainVM.mainVM.MainCurrentControl = new CreateOrUpdateRequestVM();
        }

        public ICommand RequestUpdate { get; }
        private bool CanRequestUpdateExecute(object parameter) => true;
        private void OnRequestUpdateExecute(object parametr) 
        { 
            if(SelectRequest != null)
            {
                MainWindowViewModel.main.UpdRequest = SelectRequest;
                MainVM.mainVM.MainCurrentControl = new CreateOrUpdateRequestVM();
            }
            else
            {
                FailedAddOrUpdateContent = "*Не выбранна заявка";
                ForegroundFailedMessage = -1;
            }
        }

        public ICommand RequestDelete { get; }
        private bool CanRequestDeleteExecute(object parameter) => true;
        private async void OnRequestDeleteExecute(object parameter)
        {
            if(SelectRequest != null)
            {
                MainWindowViewModel.context.TurbineRequests.Remove(SelectRequest);
                await MainWindowViewModel.context.SaveChangesAsync();
                await MainWindowViewModel.main.UpdateData();
                Requests = MainWindowViewModel.main.TurbineRequests.ToList();
                ItemReuest = Requests;

            }
            else
            {
                FailedAddOrUpdateContent = "*Не выбранна заявка";
                ForegroundFailedMessage = -1;
            }
        }
        #endregion

        public RequestVM() 
        {
            Requests = MainWindowViewModel.main.TurbineRequests;
            ItemReuest = Requests;

            RequestAdd = new LambdaCommand(OnRequestAddExecute, CanRequestAddExecute);
            RequestUpdate = new LambdaCommand(OnRequestUpdateExecute, CanRequestUpdateExecute);
        }

      
    }
}
