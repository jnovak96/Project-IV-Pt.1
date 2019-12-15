using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCDDVDShop
{
    [Serializable()]

    class CDChamber : CDClassical
    {
        private string hiddenInstruments;

        public CDChamber()
        {
            hiddenInstruments = "";
        }

        public CDChamber(int UPC, decimal price, string title, int quantity,
            string label, string artists, string conductor, string instruments) : base(UPC, price, title, quantity, label, artists)
        {
            hiddenInstruments = instruments;
        }

        public string CDChamberInstruments
        {
            get
            {
                return hiddenInstruments;
            }
            set
            {
                hiddenInstruments = value;
            }
        }

        public override void Save(frmBookCDDVDShop f)
        {
            base.Save(f);
            hiddenInstruments = f.txtCDChamberInstrumentList.Text;
        } // end Save


        // Display data in object on form
        public override void Display(frmBookCDDVDShop f)
        {
            base.Display(f);
            f.txtCDChamberInstrumentList.Text = hiddenInstruments;
        }  // end Display

        public override string ToString()
        {
            string s = base.ToString() + "\n";
            s += "Artist: " + hiddenInstruments;
            return s;
        }  // end ToString
    }
}
