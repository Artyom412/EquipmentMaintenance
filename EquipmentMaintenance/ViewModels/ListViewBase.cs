using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public abstract class ListViewBase<T> : ObservableObject where T : class
    {
        public AvaloniaList<T> Items { get; } = [];

        public IRelayCommand AddCommand { get; } = null!;
        public IRelayCommand DeleteCommand { get; } = null!;
        public IRelayCommand RefreshCommand { get; } = null!;

        protected ListViewBase() 
        {
            AddCommand = new AsyncRelayCommand(AddExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
            RefreshCommand = new RelayCommand(RefreshExecute);
        }

        protected abstract Task AddExecute();
        protected abstract void DeleteExecute();
        protected abstract void RefreshExecute();
    }
}
