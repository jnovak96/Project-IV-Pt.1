using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//test
namespace BookCDDVDShop
{
    [Serializable()]

    class DBProduct : Product
    {

        //default parameterless constructor
        public DBProduct()
        {
            
        }

        //parameterized constructor
        public DBProduct(int UPC, decimal price, string title, int quantity) : base(UPC, price, title, quantity)
        {
            
        }

   

        public override void Save(frmBookCDDVDShop f)
        {
            base.Save(f);
        } // end Save


        // Display data in object on form
        public override void Display(frmBookCDDVDShop f)
        {
            base.Display(f);
        }  // end Display


        // This toString function overrides the Object toString
        //     function.  The base refers to Object because this class
        //     inherits Object by default.
        public override string ToString()
        {
            string s = base.ToString() + "\n";
          
            return s;
        }  // end ToString
    }
}
