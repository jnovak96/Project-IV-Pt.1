using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCDDVDShop
{
    [Serializable()]

    class CDOrchestral : CDClassical
    {
        private string hiddenConductor;

        public CDOrchestral()
        {
            hiddenConductor = "";
        }

        public CDOrchestral(int UPC, decimal price, string title, int quantity,
            string label, string artists, string conductor) : base (UPC, price, title, quantity, label, artists)
        {
            hiddenConductor = conductor;
        }

        public string CDOrchestralConductor
        {
            get
            {
                return hiddenConductor;
            }
            set
            {
                hiddenConductor = value;
            }
        }

        public override void Save(frmBookCDDVDShop f)
        {
            base.Save(f);
            hiddenConductor = f.txtCDOrchestraConductor.Text;
        } // end Save


        // Display data in object on form
        public override void Display(frmBookCDDVDShop f)
        {
            base.Display(f);
            f.txtCDOrchestraConductor.Text = hiddenConductor;
        }  // end Display

        public override string ToString()
        {
            string s = base.ToString() + "\n";
            s += "\nConductor: " + hiddenConductor;
            return s;
        }  // end ToString
    }
}
