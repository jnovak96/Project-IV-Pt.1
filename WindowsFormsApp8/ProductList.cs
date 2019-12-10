using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookCDDVDShop
{
    [Serializable()]
    public class ProductList
    {
        List<Product> pList;
        public ProductList()
        {
            pList = new List<Product>();
        }

        public void addProduct(Product newProduct)
        {
            pList.Add(newProduct);
        }

        public void removeProduct(int givenUPC)
        {
            for (int i = 0; i < pList.Count; i++)
            {
                if (pList[i].ProductUPC == givenUPC)
                {
                    pList.RemoveAt(i);
                    MessageBox.Show("Entry Deleted Successfully");
                    return;
                }
            }
            MessageBox.Show("Failed to find a product with the given UPC code.");
        }

        public bool UPCUsed(int givenUPC)
        {
            for (int i = 0; i < pList.Count; i++)
            {
                if (pList[i].ProductUPC == givenUPC)
                    return true;
            }
            return false;
        }

        public Product displayProduct(int givenUPC)
        {
            for (int i = 0; i < pList.Count; i++)
            {
                if (pList[i].ProductUPC == givenUPC)
                    return pList[i];
            }
            return null;
        }

        public int Count()
        {
            return pList.Count;
        }

        public override string ToString()
        {
            StringBuilder pListString = new StringBuilder();

            for (int i = 0; i < pList.Count; i++)
            {
                pListString.Append(i.ToString() + ")\n"+ pList[i].ToString() + "\n\n");
            }
            return pListString.ToString();
        }



        /*
        public Product returnProduct(int UPC)
        {

        }
        */
    }
}
