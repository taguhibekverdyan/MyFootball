﻿using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.Helpers;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace MyFootballAdmin.Main.Views.Notifications
{
    public class NotificationViewModel:BindableBase, INavigationAware, IRegionManagerAware
    {
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;

        public NotificationViewModel(IShellService shellService, IEventAggregator eventAggregator)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NotificationEvent>().Subscribe(NotificationEventHandler);
        }

        #region Types

        private string _message = "You logged in as Admin.";

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _colour = "Blue";

        public string Colour
        {
            get { return _colour; }
            set { SetProperty(ref _colour, value); }
        }

        #endregion

        #region Navigation

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<NotificationEvent>().Unsubscribe(NotificationEventHandler);
        }
 
        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        #endregion

        private void NotificationEventHandler(NotificationEventArgs args)
        {
            Message = args.Notification.Message;
            Colour = args.Notification.Colour;
        }

        public IRegionManager RegionManager { get; set; }
    }
}