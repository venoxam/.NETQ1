using ExamJanvier.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApplication1.ViewModels;

namespace ExamJanvier.ViewModels
{
    class ProductVM : INotifyPropertyChanged
    {
        private NorthwindContext dc = new NorthwindContext();
        private ObservableCollection<ProduitModel> _ProduitsList;
        private ProduitModel _selectedProduct;
        private List<ProductCountByCountry> _productCountByCountry;

        private DelegateCommand _deleteCommand;


        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }


        public ObservableCollection<ProduitModel> ProduitsList
        {
            get 
            {
                if (_ProduitsList == null)
                {
                    _ProduitsList = loadProduct();
                }

                return _ProduitsList;

            }
        }

        private ObservableCollection<ProduitModel> loadProduct()
        {
            ObservableCollection<ProduitModel> localCollection = new ObservableCollection<ProduitModel>();

            foreach (var item in dc.Products)
            {
                localCollection.Add(new ProduitModel(item));
            }

            return localCollection;
        }

        public List<ProductCountByCountry> ProductCountByCountry
        {
            get { return _productCountByCountry = _productCountByCountry ?? loadProductCountByCountry(); }
        }

        public List<ProductCountByCountry> loadProductCountByCountry()
        {
            List<ProductCountByCountry> productsCountByCountry = new List<ProductCountByCountry>();

            var productCountsByCountry = from orderDetail in dc.OrderDetails
                                         group orderDetail.ProductId by orderDetail.Product.Supplier.Country into countryGroup
                                         orderby countryGroup.Count() descending
                                         select new
                                         {
                                             Country = countryGroup.Key,
                                             ProductCount = countryGroup.Distinct().Count()
                                          };
                        
           foreach (var country in productCountsByCountry)
            {
                productsCountByCountry.Add(new ProductCountByCountry(country.Country, country.ProductCount));
            }

            return productsCountByCountry.ToList();
        }

        public ProduitModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged(nameof(SelectedProduct));
                }
            }
        }

        public DelegateCommand DeleteCommand
        {
            get { return _deleteCommand = _deleteCommand ?? new DelegateCommand(DeleteProduct); }
        }

      
         private void DeleteProduct()
         {
           SelectedProduct.MonProduit.Discontinued = true;
           dc.SaveChanges();
           ProduitsList.Remove(SelectedProduct);
           OnPropertyChanged(nameof(ProduitsList));
        }

       
    }
}
