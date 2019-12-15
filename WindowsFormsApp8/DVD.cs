using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCDDVDShop
{
    class DVD : Product
    {
        private string hiddenRelease;
        private string hiddenActor;
        private int hiddenRunTime;

        public DVD()
        {
            hiddenActor = "";
            hiddenRelease = "";
            hiddenRunTime = 0;
        }

        public DVD(int UPC, decimal price, string title, int quantity, string releaseDate, string actor, int runTime) : base(UPC, price, title, quantity)
        {
            hiddenActor = actor;
            hiddenRelease = releaseDate;
            hiddenRunTime = runTime;
        }

        public string DVDActor
        {
            get
            {
                return hiddenActor;
            }
            set
            {
                hiddenActor = value;
            }
        }

        public string DVDReleaseDate
        {
            get
            {
                return hiddenRelease;
            }
            set
            {
                hiddenRelease = value;
            }
        }

        public int DVDRunTime
        {
            get
            {
                return hiddenRunTime;
            }
            set
            {
                hiddenRunTime = value;
            }
        }

        public override void Save(frmBookCDDVDShop f)
        {
            DateTime releaseDateParsed;
            base.Save(f);
            hiddenActor = f.txtDVDLeadActor.Text;
            hiddenRelease = f.txtDVDReleaseDate.Text;
            hiddenRunTime = Convert.ToInt32(f.txtDVDRunTime.Text);
        } // end Save

          // Display data in object on form
        public override void Display(frmBookCDDVDShop f)
        {
            base.Display(f);
            f.txtDVDLeadActor.Text = hiddenActor;
            f.txtDVDReleaseDate.Text = hiddenRelease.ToString();
            f.txtDVDRunTime.Text = hiddenRunTime.ToString();
        }  // end Display

        public override string ToString()
        {
            string s = base.ToString() + "\n";
            s += "Lead Actor: " + hiddenActor + "\nRelease Date: " + hiddenRelease + "\nRun Time: " +  hiddenRunTime;
            return s;
        }  // end ToString
    }
}
