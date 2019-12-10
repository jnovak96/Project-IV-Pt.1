using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCDDVDShop
{
    [Serializable()]

    class BookCIS : Book
    {
        private string hiddenArea;
        public BookCIS()
        {
            hiddenArea = "";
        }

        public BookCIS(int UPC, decimal price, string title, int quantity, string ISBN,
            string author, int pages) : base(UPC, price, title, quantity, ISBN, author, pages)
        {
            hiddenArea = "";
        }

        public string BookCISArea
        {
            get
            {
                return hiddenArea;
            }
            set
            {
                hiddenArea = value;
            }
        }

        public override void Save(frmBookCDDVDShop f)
        {
            base.Save(f);
            hiddenArea = f.txtBookCISArea.Text;
        } // end Save


        // Display data in object on form
        public override void Display(frmBookCDDVDShop f)
        {
            base.Display(f);
            f.txtBookCISArea.Text = hiddenArea;
        }  // end Display


        // This toString function overrides the Object toString
        //     function.  The base refers to Object because this class
        //     inherits Object by default.
        public override string ToString()
        {
            string s = base.ToString() + "\n";
            s += "BookCIS Info: " + hiddenArea;
            return s;
        }  // end ToString
    }
}
