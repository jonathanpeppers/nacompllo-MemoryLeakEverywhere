using MemoryLeakEverywhere.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MemoryLeakEverywhere.ViewModels
{
    public class CollectionViewSampleViewModel : BaseViewModel
    {
        private bool randomBool = true;
        public CollectionViewSampleViewModel()
        {
            RandomItems = LoadData();
        }

        public ICommand ChangeCategoryCommand => new Command(() => ChangeCategory());

        private ObservableCollection<RandomItem> _randomItems = new ObservableCollection<RandomItem>();
        public ObservableCollection<RandomItem> RandomItems
        {
            get => _randomItems;
            set
            {
                _randomItems = value;
                RaiseOnPropertyChanged();
            }
        }

        private ObservableCollection<Category> _categories = new ObservableCollection<Category>
        {
            new Category
            {
                Name = "Refresh ItemsSource"
            }
        };
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                RaiseOnPropertyChanged();
            }
        }

        private void ChangeCategory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            RandomItems = LoadData();
        }

        private ObservableCollection<RandomItem> LoadData()
        {
            var items = new ObservableCollection<RandomItem>();
            var rnd = new Random();
            var itemsNumber = rnd.Next(25, 50);

            for (var i = 0; i < itemsNumber; i++)
            {
                randomBool = !randomBool;
                var randomImage = i % 2 == 0 && randomBool ? "dotnet_bot" : "monkey";
                items.Add(new RandomItem
                {
                    Name = $"Item {i}",
                    Image = randomImage
                });
            }
            return items;
        }
    }
}