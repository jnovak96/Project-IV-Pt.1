using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//test
namespace BookCDDVDShop
{
    [Serializable()]

    class Book : Product
    {
        private int hiddenISBN;
        private string hiddenAuthor;
        private int hiddenPages;

        //default parameterless constructor
        public Book()
        {
            hiddenAuthor = "";
            hiddenISBN = 0;
            hiddenPages = 0;
        }

        //parameterized constructor
        public Book(int UPC, decimal price, string title, int quantity, int ISBN,
            string author, int pages) : base(UPC, price, title, quantity)
        {
            hiddenAuthor = "";
            hiddenISBN = 0;
            hiddenPages = 0;
        }

        public int BookISBN
        {
            get
            {
                return hiddenISBN;
            }
            set
            {
                hiddenISBN = value;
            }
        }

        public string BookAuthor
        {
            get
            {
                return hiddenAuthor;
            }
            set
            {
                hiddenAuthor = value;
            }
        }

        public int BookPages
        {
            get
            {
                return hiddenPages;
            }
            set
            {
                hiddenPages = value;
            }
        }

       
         public override void Save(frmBookCDDVDShop f)
        {
            base.Save(f);
            hiddenAuthor = f.txtBookAuthor.Text;
            hiddenISBN = Convert.ToInt32(f.txtBookISBNLeft.Text +  f.txtBookISBNRight.Text);
            hiddenPages = Convert.ToInt32(f.txtBookPages.Text);
        } // end Save


        // Display data in object on form
        public override void Display(frmBookCDDVDShop f)
        {
            base.Display(f);
            f.txtBookAuthor.Text = hiddenAuthor;
            f.txtBookISBNLeft.Text = hiddenISBN.ToString(). Substring(0, 4);
            f.txtBookISBNRight.Text = hiddenISBN.ToString().Substring(5, 4);
            f.txtBookPages.Text = hiddenPages.ToString();
        }  // end Display


        // This toString function overrides the Object toString
        //     function.  The base refers to Object because this class
        //     inherits Object by default.
        public override string ToString()
        {
            string s = base.ToString() + "\n";
            s += "\nAuthor: " + hiddenAuthor + "\nISBN: " + hiddenISBN + "\nPage Count: " + hiddenPages;
            return s;
        }  // end ToString
    }
}
