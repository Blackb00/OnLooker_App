using OnLooker;
using OnLooker.Core;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OxyPlot.Series;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OxyPlot.Axes;
using Prism.Mvvm;

namespace OnLooker.UI
{
    internal class Presenter : BindableBase
    {
        private readonly COutputPort _outputPort = new COutputPort();
        private Controller _dataInputController;
        private readonly ObservableCollection<ArticleInfo> _articles = new ObservableCollection<ArticleInfo>();
        public readonly ReadOnlyObservableCollection<ArticleInfo> Articles;
        private readonly PlotModel _model = new PlotModel() { Title = "Currencies Graph", Subtitle = "using OxyPlot" };
        public readonly PlotModel Model;

        private readonly ProgressBar _pbCalculationProgress = new ProgressBar();

        public readonly ProgressBar PbCalculationProgress;
        public CountryInfo[] Countries { get; set; }
        public CurrencyInfo[] Currencies { get; set; }
       
        public Presenter()
        {
            Articles = new ReadOnlyObservableCollection<ArticleInfo>(_articles);
            PbCalculationProgress = _pbCalculationProgress;
            Model = _model;
            Countries = GetCountries();
            Currencies = GetCurrencyTypes();
        }

        public void GetReport(object sender, DoWorkEventArgs e)         
        {
            var q = (QueryInfo) e.Argument;
            _dataInputController = new Controller(q.CurrencyPair, q.Tag.Value, q.Country);

            SetArticles(sender, e);
            
        }
        public void RunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Произошла ошибка");
            }
            else
            {

                foreach (var article in (List<ArticleInfo>)e.Result)
                {
                    _articles.Add(article);
                }
                RaisePropertyChanged("Articles");
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
            }
        }
        public void RunWorker2Completed(Object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Произошла ошибка");
            }
            else
            {
                var resultCurrencies = (CCurrencyPairTimePrint[])e.Result;

                var series1 = new LineSeries { Title = "Series 1", MarkerType = MarkerType.Star };
                DateTime start = new DateTime(2000, 01, 01);
                series1.Points.Add(new DataPoint(0, 0));
      
                foreach (var t in resultCurrencies)
                {
                    Double x = (t.Time - start).TotalHours;
                    series1.Points.Add(new DataPoint(x, t.Rate));
                }

                _model.Series.Add(series1);
                _model.Axes.Add(new LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, Minimum = 0, Maximum = 10000 });
                _model.Axes.Add(new LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, Minimum = 0, Maximum = 2 });
               
                RaisePropertyChanged("Model");
                _model.InvalidatePlot(true);
            
            }
        }
        public void SetArticles(object sender, DoWorkEventArgs e)
        {
            ArticleInfo[] resultArticles = _dataInputController.GetArticles();

            List<ArticleInfo>articleInfos = new List<ArticleInfo>();
            foreach (var article in resultArticles)
            {
                articleInfos.Add(article);
            }

            e.Result = articleInfos;
           
        }
       
        public void SetCurrencyGraph(object sender, DoWorkEventArgs e)
        {
            var q = (QueryInfo)e.Argument;
            _dataInputController = new Controller(q.CurrencyPair, q.Tag.Value, q.Country);
            CCurrencyPairTimePrint[] resultCurrencies = _dataInputController.GeTimePrints();
            
            e.Result = resultCurrencies;
        }

        public void ResetDataEvent()
        {
            _articles.Clear();
            _model.Series.Clear();
            _model.InvalidatePlot(true);
            RaisePropertyChanged("Model");
            
            RaisePropertyChanged("Articles");
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;

        }
        
        public void GoToUrl(object sender, EventArgs e)
        {

        }
        public CountryInfo[] GetCountries()
        {
            return _outputPort.Countries;
        }
        public CurrencyInfo[] GetCurrencyTypes()
        {
            return _outputPort.Currencies;
        }

    }
}