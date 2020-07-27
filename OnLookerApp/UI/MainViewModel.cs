using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using OnLooker.Core;
using OnLooker.UI;
using OxyPlot;
using Prism.Commands;
using Prism.Mvvm;

namespace UI
{
    public class MainViewModel : BindableBase
    {
        readonly Presenter _presenter = new Presenter();
        public MainViewModel()
        {
            _presenter.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            Currencies =  new ObservableCollection<CurrencyInfo>();
            Countries = new ObservableCollection<CountryInfo>(); ;
           // Model = new PlotModel();
            PutRequest = new DelegateCommand(() =>
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                //.IsEnabled = false;
                var pair = new CCurrencyPair() { BaseCurrency = BaseCurrency, QuotedCurrency = QuotedCurrency };
                QueryInfo userQuery = new QueryInfo
                    {Country = Country, CurrencyPair = pair, Tag = new CTag {Value = QueryText}};
                BackgroundWorker worker = new BackgroundWorker();
                //worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += _presenter.GetReport;
                worker.RunWorkerCompleted += _presenter.RunWorkerCompleted;
                worker.RunWorkerAsync(userQuery);

                BackgroundWorker worker2 = new BackgroundWorker();
                worker2.WorkerReportsProgress = true;
                worker2.WorkerSupportsCancellation = true;
                worker2.DoWork += _presenter.SetCurrencyGraph;
                worker2.RunWorkerCompleted += _presenter.RunWorker2Completed;
                worker2.RunWorkerAsync(userQuery);
            });
            ResetData = new DelegateCommand(() =>
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                _presenter.ResetDataEvent();
            });

            
            foreach (var currency in _presenter.Currencies)
            {
                Currencies.Add(currency);
            }

            foreach (var c in _presenter.Countries)
            {
                Countries.Add(c);
            }
            NavigateCommand = new DelegateCommand<String>((str) =>
            {
                Uri site = new Uri(str);
                Process.Start(new ProcessStartInfo(site.AbsoluteUri));
            });
        }
        public CurrencyInfo BaseCurrency { get; set; }
        public CurrencyInfo QuotedCurrency { get; set; }
        public CountryInfo Country { get; set; }
        public String QueryText { get; set; }
        public ObservableCollection<CurrencyInfo> Currencies { get; }
        public ObservableCollection<CountryInfo> Countries { get; }
        public PlotModel Model => _presenter.Model;
        public DelegateCommand PutRequest { get; }
        public DelegateCommand ResetData { get; }
        public DelegateCommand<String> NavigateCommand { get; }
        public ReadOnlyObservableCollection<ArticleInfo> Articles => _presenter.Articles;
        public ArticleInfo SelectedArticle { get; set; }


    }
}
