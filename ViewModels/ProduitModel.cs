using ExamJanvier.Models;
using System;
using System.ComponentModel;

namespace ExamJanvier.ViewModels
{
    class ProduitModel : INotifyPropertyChanged
    {
        private readonly Product _monProduit;

        public Product MonProduit
        {
            get { return _monProduit; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ProduitModel(Product current)
        {
            this._monProduit = current;
        }

        public int ProductId
        {
            get { return _monProduit.ProductId; }
        }

        public string ProductName
        {
            get { return _monProduit.ProductName; }
        }

        // Add Category and Supplier properties
        public string Category
        {
            get { return _monProduit.Category.CategoryName; }
        }

        public string SupplierContactName
        {
            get { return _monProduit.Supplier.ContactName; }
        }
    }
}
