using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using EquipmentMaintenance.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentMaintenance.Services
{
    internal class NavigationService(IServiceProvider serviceProvider) : INavigationService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public ObservableObject NavigateTo<TViewModel>() where TViewModel : ObservableObject
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
            return viewModel;
        }
    }
}
