using Avalonia.Collections;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EquipmentMaintenance.ViewModels
{
    public abstract class ListViewBase<T> : ViewModelBase where T : class
    {
        public AvaloniaList<T> Items { get; } = [];

        private readonly List<T> _allItems = [];

        private string _searchText = string.Empty;

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public IRelayCommand AddCommand { get; } = null!;
        public IRelayCommand DeleteCommand { get; } = null!;
        public IRelayCommand RefreshCommand { get; } = null!;
        public IRelayCommand SearchCommand { get; } = null!;
        public IRelayCommand ClearSearchCommand { get; } = null!;

        protected ListViewBase() 
        {
            AddCommand = new AsyncRelayCommand(() => RunSafelyAsync(AddExecute));
            DeleteCommand = new AsyncRelayCommand(() => RunSafelyAsync(DeleteExecute));
            RefreshCommand = new AsyncRelayCommand(() => RunSafelyAsync(RefreshExecute));
            SearchCommand = new RelayCommand(ApplySearch);
            ClearSearchCommand = new RelayCommand(ClearSearch);
        }

        protected void SetItems(IEnumerable<T> items)
        {
            _allItems.Clear();
            _allItems.AddRange(items);
            ApplySearch();
        }

        protected void AddItem(T item)
        {
            _allItems.Add(item);
            ApplySearch();
        }

        protected void RemoveItem(T item)
        {
            _allItems.Remove(item);
            ApplySearch();
        }

        protected void ClearItems()
        {
            _allItems.Clear();
            Items.Clear();
        }

        protected virtual bool MatchesSearch(T item, string searchText) =>
            item.ToString()?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true;

        private void ApplySearch()
        {
            var searchText = SearchText.Trim();
            var filteredItems = string.IsNullOrEmpty(searchText)
                ? _allItems
                : _allItems.Where(item => MatchesSearch(item, searchText));

            Items.Clear();
            Items.AddRange(filteredItems);
        }

        private void ClearSearch()
        {
            SearchText = string.Empty;
            ApplySearch();
        }

        protected abstract Task AddExecute();
        protected abstract Task DeleteExecute();
        protected abstract Task RefreshExecute();
    }
}
