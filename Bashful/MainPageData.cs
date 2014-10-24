using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bashful
{
    class MainPageData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _quote = "";

        public string CurrentQuote
        {
            get
            {
                return _quote;
            }
            set
            {
                if (value != _quote)
                {
                    _quote = value;
                    NotifyPropertyChanged("CurrentQuote");
                }
            }
        }


        private string _number = "";

        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                if (value != _number)
                {
                    _number = value;
                    NotifyPropertyChanged("Number");
                }
            }
        }

        public string UpgradeLink { get; set; }
        public string DowngradeLink { get; set; }
        public string ReportLink { get; set; }

        private void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs((name)));
            }
        }
    }
}
