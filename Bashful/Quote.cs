using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bashful
{
    class Quote
    {
        public Quote(string quote, string number, string url, string upgradeLink, string downgradeLink, string reportLink)
        {
            _quote = quote;
            _number = number;
            _url = url;
            _upgradeLink = upgradeLink;
            _downgradeLink = downgradeLink;
            _reportLink = reportLink;
        }

        private string _quote;
        public string QuoteText
        {
            get
            {
                return _quote;
            }
        }

        private string _number;
        public string Number
        {
            get
            {
                return _number;
            }
        }

        private string _downgradeLink;
        private string _url;
        private string _upgradeLink;
        private string _reportLink;
    }
}
