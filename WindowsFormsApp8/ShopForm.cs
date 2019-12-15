using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

/**
 * Form that controls the Addition / Edit / Delete Events
 * 
 * Produced by: John Novak & Anthony Zayas
 */


namespace BookCDDVDShop
{
    public partial class frmBookCDDVDShop : Form
    {
        //Global Attributes
        private int updateState;
        private int addState;
        private int mode;
        ProductList pList;
        ProductDB productDB;
        Validations newVal;

        //Constructor for form
        public frmBookCDDVDShop()
        {
               InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //initiate state monitoring integers
            addState = -1;
            mode = 0;
            pList = new ProductList();
            productDB = new ProductDB();
            FormController.resetForm(this);
            newVal = new Validations(this);
            //Check if the serializable file exists
           // if (File.Exists("BookCDDVDShopData"))
           // {
            //    SFManager.readFromFile(ref pList, "BookCDDVDShopData");
           // }
            
        }
        //Create Book bytton
        private void btnCreateBook_Click(object sender, EventArgs e)
        {
            FormController.deactivateAllButBook(this);
            FormController.activateBook(this);
            FormController.formAddMode(this);
            addState = 0;
            mode = 0;
        }

        //Create Book CIS button
        private void btnCreateBookCIS_Click(object sender, EventArgs e)
        {
            FormController.deactivateAllButBookCIS(this);
            FormController.activateBookCIS(this);
            FormController.formAddMode(this);
            addState = 1;
            mode = 0;
        }

        //Create DVD button
        private void btnCreateDVD_Click(object sender, EventArgs e)
        {
            FormController.deactivateAllButDVD(this);
            FormController.activateDVD(this);
            FormController.formAddMode(this);
            addState = 2;
            mode = 0;
        }

        //Create CD Orchestra Button
        private void btnCreateCDOrchestra_Click(object sender, EventArgs e)
        {
            FormController.deactivateAllButCDOrchestra(this);
            FormController.activateCDOrchestra(this);
            FormController.formAddMode(this);
            addState = 3;
            mode = 0;
        }

        //Create CD Chamber Button
        private void btnCreateCDChamber_Click(object sender, EventArgs e)
        {
            FormController.deactivateAllButCDChamber(this);
            FormController.activateCDChamber(this);
            FormController.formAddMode(this);
            addState = 4;
        }

        //Exit and save to serializable file
        private void btnExit_Click(object sender, EventArgs e)
        {
            //SFManager.writeToFile(pList, "BookCDDVDShopData");
            this.Close();
        }

        //Button for saving new or edited products
        private void btnSaveEditUpdate_Click(object sender, EventArgs e)
        {
            switch(addState)
            {
                case 0:
                    //Case is in: add a Book
                    //Run checks and break on failure
                    if (!newVal.ProductTextCheck() || !newVal.BookTextCheck())
                    {
                        MessageBox.Show("One of the fields was not entered in a usable form. Please try again!");
                        break;
                    }
                    Book newBook = new Book();
                    newBook.Save(this);
                    MessageBox.Show("Product Added Successfully!");

                    //Adds to the database
                    productDB.InsertProduct(newBook.ProductUPC, newBook.ProductPrice, newBook.ProductTitle, newBook.ProductQuantity, newBook.GetType().Name);
                    productDB.InsertBook(newBook.ProductUPC, newBook.BookISBN, newBook.BookAuthor, newBook.BookPages);
                    //pList.addProduct(newBook);
                    break;
                case 1:
                    //Case is in: add a CIS Book
                    //Run checks and break on failure
                    if (!newVal.ProductTextCheck() || !newVal.BookCISTextCheck() || !newVal.BookTextCheck())
                    {
                        break;
                    }
                    BookCIS newBookCIS = new BookCIS();
                    newBookCIS.Save(this);
                    MessageBox.Show("Product Added Successfully!");


                    //Inserts a new CISBook into the database
                    productDB.InsertProduct(newBookCIS.ProductUPC, newBookCIS.ProductPrice, newBookCIS.ProductTitle, newBookCIS.ProductQuantity, newBookCIS.GetType().Name);
                    productDB.InsertBook(newBookCIS.ProductUPC, newBookCIS.BookISBN, newBookCIS.BookAuthor, newBookCIS.BookPages);
                    productDB.InsertBookCIS(newBookCIS.ProductUPC, newBookCIS.BookCISArea);
                    //pList.addProduct(newBookCIS);
                    break;
                case 2:
                    //Case is in: add a DVD
                    //Run checks and break on failure
                    if (!newVal.ProductTextCheck() || !newVal.DVDTextCheck())
                    {
                        break;
                    }
                    DVD newDVD = new DVD();
                    newDVD.Save(this);
                    MessageBox.Show("Product Added Successfully!");


                    //Inserts a new DVD and Product information into Database
                    productDB.InsertProduct(newDVD.ProductUPC, newDVD.ProductPrice, newDVD.ProductTitle, newDVD.ProductQuantity, newDVD.GetType().Name);
                    productDB.InsertDVD(newDVD.ProductUPC, newDVD.DVDActor, Convert.ToDateTime(newDVD.DVDReleaseDate), newDVD.DVDRunTime);
                    //pList.addProduct(newDVD);
                    break;
                case 3:
                    //Run checks and break on failure
                    if (!newVal.ProductTextCheck() || !newVal.CDClassicalTextCheck() || !newVal.CDOrchestralTextCheck())
                    {
                        break;
                    }
                    //Case is in: add a CD orchestra
                    CDOrchestral newCDOrchestral = new CDOrchestral();
                    newCDOrchestral.Save(this);
                    MessageBox.Show("Product Added Successfully!");


                    //Inserts a new classical and orcenstral entry into database
                    productDB.InsertProduct(newCDOrchestral.ProductUPC, newCDOrchestral.ProductPrice, newCDOrchestral.ProductTitle, newCDOrchestral.ProductQuantity, newCDOrchestral.GetType().Name);
                    productDB.InsertCDClassical(newCDOrchestral.ProductUPC, newCDOrchestral.CDClassicalLabel, newCDOrchestral.CDClassicalArtists);
                    productDB.InsertCDOrchestra(newCDOrchestral.ProductUPC, newCDOrchestral.CDOrchestralConductor);

                    //pList.addProduct(newCDOrchestral);
                    break;
                case 4:
                    //Case is in: add a CD Chamber
                    //Run checks and break on failure
                    if (!newVal.ProductTextCheck()|| !newVal.CDClassicalTextCheck() || !newVal.CDChamberTextCheck())
                    {
                        break;
                    }
                    CDChamber newCDChamber = new CDChamber();
                    newCDChamber.Save(this);
                    MessageBox.Show("Product Added Successfully!");


                    //Inserts a new classical and chamber entry into database
                    productDB.InsertProduct(newCDChamber.ProductUPC, newCDChamber.ProductPrice, newCDChamber.ProductTitle, newCDChamber.ProductQuantity, newCDChamber.GetType().Name);
                    productDB.InsertCDClassical(newCDChamber.ProductUPC, newCDChamber.CDClassicalLabel, newCDChamber.CDClassicalArtists);
                    productDB.InsertCDChamber(newCDChamber.ProductUPC, newCDChamber.CDChamberInstruments);
                    //pList.addProduct(newCDChamber);
                    break;
            }
        }
  
 

        //Enables find mode
        private void btnFind_Click(object sender, EventArgs e)
        {
            mode = 1;
            FormController.clear(this);
            FormController.resetForm(this);
            txtProductUPC.Enabled = true;
            btnClear.Enabled = true;
            btnEnterUPC.Enabled = true;
            MessageBox.Show("Please enter the UPC of the product you'd like to find in the UPC box and press the Enter UPC button");
        }

        //Clears the form and resets it
        private void btnClear_Click(object sender, EventArgs e)
        {
            FormController.clear(this);
            FormController.resetForm(this);
            addState = -1;
            mode = 0;
        }

        //Enables delete mode
        private void btnDelete_Click(object sender, EventArgs e)
        {
            mode = 2;
            FormController.clear(this);
            FormController.resetForm(this);
            txtProductUPC.Enabled = true;
            btnClear.Enabled = true;
            btnEnterUPC.Enabled = true;
            MessageBox.Show("Please enter the UPC of the product you'd like to delete in the UPC box and press the Enter UPC button");
        }

        //Enables edit mode
        private void btnEdit_Click(object sender, EventArgs e)
        {
            mode = 3;
            FormController.clear(this);
            FormController.resetForm(this);
            txtProductUPC.Enabled = true;
            btnClear.Enabled = true;
            btnEnterUPC.Enabled = true;
            MessageBox.Show("Please enter the UPC of the product you'd like to edit in the UPC box and press the Enter UPC button");
        }
        
        //Performs a function with the UPC depending on the current mode
        private void btnEnterUPC_Click(object sender, EventArgs e)
        {
            int UPCParsed;
            if (int.TryParse(txtProductUPC.Text, out UPCParsed))
            {

                bool noMatches = false;
                OleDbDataReader test = productDB.SelectProduct(UPCParsed, out noMatches);
                while (test.Read())
                {
                    MessageBox.Show(test.GetValue(4).ToString());
                   
                }
            }
            //pList.displayProduct(UPCParsed);
            else
            {
                MessageBox.Show("Error! UPC must be a valid numeric value");
                return;
            }

            switch (mode)
            {
                case 1:
                    //find mode
                    bool OKFlag = false;
                    OleDbDataReader dbResult = productDB.SelectProduct(UPCParsed, out OKFlag);
                   
                    while (dbResult.Read())
                    {
                        MessageBox.Show("UPC : " + dbResult[0] + " , " + "Price : " + dbResult[1] + ", " + "Title : " + dbResult[2] + ", " + "Quantity : " + dbResult[3]);
                    }
                    if (!OKFlag)
                    {
                        MessageBox.Show("Error, could not be found!");
                    }

                    //pList.displayProduct(UPCParsed).Display(this);
                    break;
                case 2:
                    //delete mode

                    productDB.Delete(UPCParsed, out OKFlag);
                    if (OKFlag)
                    {

                        MessageBox.Show("Error, UPC Does not Exist!");
                    }
                   // pList.removeProduct(UPCParsed);
                    break;
                case 3:
                    //edit mode
                    OKFlag = false;
                 
                    //dbResult contains the result from the SQL query to database
                    dbResult = productDB.SelectProduct(UPCParsed, out OKFlag);
                    
                    if (OKFlag)
                    {
                        DBProduct tmp = new DBProduct();
                        String typeOfProduct = "";
                        while (dbResult.Read())
                        {
                            tmp = new DBProduct(Convert.ToInt32(dbResult[0]), Convert.ToDecimal(dbResult[1]),
                            dbResult[2].ToString(), Convert.ToInt32(dbResult[3]));
                            tmp.Display(this);

                            //Object that holds the type of Product
                            typeOfProduct = dbResult[4].ToString();

                        }
                      

                        //A switch statement can be created to enable the correct buttons etc. 
                        switch (typeOfProduct.ToLower())
                        {
                            case ("book"):
                                //Create a Book Object and call the display 
                                {
                                    FormController.deactivateAllButBook(this);

                                    //Calls DB to get PRoduct
                                    OleDbDataReader dbBook = productDB.SelectBook(UPCParsed, out OKFlag);
                                    FormController.enableUpdateProduct(this);
                                    Book newBook = new Book();
                                    while (dbBook.Read())
                                    {
                                        newBook = new Book(tmp.ProductUPC, tmp.ProductPrice, tmp.ProductTitle, tmp.ProductQuantity,
                                            Convert.ToInt32(dbBook[1]), dbBook[2].ToString(), Convert.ToInt32(dbBook[3]));
                                    }
                                    newBook.Display(this);
                                }
                                updateState = 0;
                                break;

                            case ("bookcis"):
                                {
                                    Book newBook = new Book();
                                    FormController.deactivateAllButBookCIS(this);
                                    FormController.enableUpdateProduct(this);

                                    //Calls DB to get Book
                                    OleDbDataReader dbBook = productDB.SelectBook(UPCParsed, out OKFlag);
                                    //while reading the book from database
                                    while (dbBook.Read())
                                    {
                                        newBook = new Book(tmp.ProductUPC, tmp.ProductPrice, tmp.ProductTitle, tmp.ProductQuantity,
                                            Convert.ToInt32(dbBook[1]), dbBook[2].ToString(), Convert.ToInt32(dbBook[3]));
                                    }
                                    //Calls DB to get CISBook
                                    OleDbDataReader dbBookCIS = productDB.SelectBookCIS(UPCParsed, out OKFlag);
                                    BookCIS newBookCIS = new BookCIS();
                                    //while 
                                    while (dbBookCIS.Read())
                                    {
                                        newBookCIS = new BookCIS(tmp.ProductUPC, tmp.ProductPrice, tmp.ProductTitle, tmp.ProductQuantity,
                                            newBook.BookISBN, newBook.BookAuthor, newBook.BookPages, dbBookCIS[1].ToString());
                                        newBookCIS.Display(this);
                                    }
                                }
                                updateState = 1;
                                break;

                            case ("dvd"):
                                DVD newDVD = new DVD();
                                FormController.deactivateAllButDVD(this);
                                FormController.enableUpdateProduct(this);

                                //Calls DB to get DVD
                                OleDbDataReader dbDVD = productDB.SelectDVD(UPCParsed, out OKFlag);
                                while (dbDVD.Read())
                                {
                                    newDVD = new DVD(tmp.ProductUPC, tmp.ProductPrice, tmp.ProductTitle, tmp.ProductQuantity, dbDVD[2].ToString().Split(' ')[0], dbDVD[1].ToString(), Convert.ToInt32(dbDVD[3]));  
                                }
                                newDVD.Display(this);
                                updateState = 2;
                                break;
                           
                            case ("cdorchestra"):
                                {
                                    CDClassical newCDClassical = new CDClassical();
                                    FormController.deactivateAllButCDOrchestra(this);
                                    FormController.enableUpdateProduct(this);

                                    //Calls DB to get CDClassical entry
                                    OleDbDataReader dbCDClassical = productDB.SelectCDClassical(UPCParsed,out OKFlag);
                                    while (dbCDClassical.Read())
                                    {
                                        newCDClassical = new CDClassical(tmp.ProductUPC, tmp.ProductPrice, tmp.ProductTitle, tmp.ProductQuantity,
                                            dbCDClassical[1].ToString(), dbCDClassical[2].ToString());
                                    }
                                    //Calls DB to get CDOrchestra entry
                                    OleDbDataReader dbCDOrchestra = productDB.SelectCDOrchestra(UPCParsed, out OKFlag);
                                    CDOrchestral newCDOrchestra = new CDOrchestral();
                                    while (dbCDOrchestra.Read())
                                    {
                                        newCDOrchestra = new CDOrchestral(newCDClassical.ProductUPC, newCDClassical.ProductPrice, newCDClassical.ProductTitle, newCDClassical.ProductQuantity,
                                            newCDClassical.CDClassicalLabel, newCDClassical.CDClassicalArtists, dbCDOrchestra[1].ToString());
                                    }
                                    newCDOrchestra.Display(this);
                                }
                                updateState = 3;
                                break;

                            case ("cdchamber"):
                                {
                                    CDClassical newCDClassical = new CDClassical();
                                    FormController.deactivateAllButCDChamber(this);
                                    FormController.enableUpdateProduct(this);

                                    //Calls DB to get CDClassical Entry
                                    OleDbDataReader dbCDClassical = productDB.SelectCDClassical(UPCParsed, out OKFlag);
                                    while (dbCDClassical.Read())
                                    {
                                        newCDClassical = new CDClassical(tmp.ProductUPC, tmp.ProductPrice, tmp.ProductTitle, tmp.ProductQuantity,
                                            dbCDClassical[1].ToString(), dbCDClassical[2].ToString());
                                    }

                                    //Calls DB to get CDChamer Entry
                                    OleDbDataReader dbCDChamber = productDB.SelectCDChamber(UPCParsed, out OKFlag);
                                    CDChamber newCDChamber = new CDChamber();
                                    while (dbCDChamber.Read())
                                    {
                                        newCDChamber = new CDChamber(newCDClassical.ProductUPC, newCDClassical.ProductPrice, newCDClassical.ProductTitle, newCDClassical.ProductQuantity,
                                            newCDClassical.CDClassicalLabel, newCDClassical.CDClassicalArtists, dbCDChamber[1].ToString());
                                    }
                                    newCDChamber.Display(this);
                                }
                                updateState = 4;
                                break;
                        }
                        
                    }
                    if(!OKFlag)
                        {
                            MessageBox.Show("Error, UPC Does not Exist!");
                        }
                    break;

            }

                   // pList.displayProduct(UPCParsed).Display(this);
                   //

        }
   

        //Prints out a product list from the database
        private void button1_Click(object sender, EventArgs e)
        {
            bool OKFlag = false;

            //Gets the results from a database
            OleDbDataReader results = productDB.SelectAllProduct( out OKFlag);
            if (OKFlag)
            {
                List<String> resultList = new List<string>();
                System.Collections.IEnumerator ienum = results.GetEnumerator();
                String s = "";         
                while (results.Read())
                {               
                    s += "UPC : " +  results[0] + " , " + "Price : " + results[1] + ", " + "Title : "  + results[2] + ", " + "Quantity : " + results[3];
                    resultList.Add(s);
                    s = "";
                }

                var message = string.Join(Environment.NewLine, resultList.ToArray());
                MessageBox.Show(message);
            }
            else
            {
                MessageBox.Show("There are no items in the Database!");
            }
          
        }

        //Updates Information in the database based off the type of Product 
        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            bool OKFlag = true;
            switch(updateState)
            {
                case 0:
                    //Case is in: add a Book
                    //Run checks and break on failure
                    if (!newVal.ProductTextCheck() || !newVal.BookTextCheck())
                    {
                        MessageBox.Show("One of the fields was not entered in a usable form. Please try again!");
                        break;
                    }
                    Book newBook = new Book();
                    newBook.Save(this);
                    MessageBox.Show(newBook.ToString());

                    //Adds to the database
                    productDB.UpdateProduct(newBook.ProductUPC, newBook.ProductPrice, newBook.ProductTitle, newBook.ProductQuantity, newBook.GetType().Name, out OKFlag);
                    productDB.UpdateBook(newBook.ProductUPC, newBook.BookISBN, newBook.BookAuthor, newBook.BookPages);
                    MessageBox.Show("The update was successful!");
                    //pList.addProduct(newBook);
                    break;
                case 1:
                    //Case is in: add a CIS Book
                    //Run checks and break on failure
                    if (!newVal.ProductTextCheck() || !newVal.BookCISTextCheck() || !newVal.BookTextCheck())
                    {
                        break;
                    }
                    BookCIS newBookCIS = new BookCIS();
                    newBookCIS.Save(this);

                    //Inserts a new CISBook into the database
                    productDB.UpdateProduct(newBookCIS.ProductUPC, newBookCIS.ProductPrice, newBookCIS.ProductTitle, newBookCIS.ProductQuantity, newBookCIS.GetType().Name, out OKFlag);
                    productDB.UpdateBook(newBookCIS.ProductUPC, newBookCIS.BookISBN, newBookCIS.BookAuthor, newBookCIS.BookPages);
                    productDB.UpdateBookCIS(newBookCIS.ProductUPC, newBookCIS.BookCISArea);
                    MessageBox.Show("The update was successful!");
                    //pList.addProduct(newBookCIS);
                    break;
                case 2:
                    //Case is in: add a DVD
                    //Run checks and break on failure
                    if (!newVal.ProductTextCheck() || !newVal.DVDTextCheck())
                    {
                        break;
                    }
                    DVD newDVD = new DVD();
                    newDVD.Save(this);

                    //Inserts a new DVD and Product information into Database
                    productDB.UpdateProduct(newDVD.ProductUPC, newDVD.ProductPrice, newDVD.ProductTitle, newDVD.ProductQuantity, newDVD.GetType().Name, out OKFlag);
                    productDB.UpdateDVD(newDVD.ProductUPC, newDVD.DVDActor, Convert.ToDateTime(newDVD.DVDReleaseDate), newDVD.DVDRunTime);
                    MessageBox.Show("The update was successful!");
                    //pList.addProduct(newDVD);
                    break;
                case 3:
                    //Run checks and break on failure
                    if (!newVal.ProductTextCheck() || !newVal.CDClassicalTextCheck() || !newVal.CDOrchestralTextCheck())
                    {
                        break;
                    }
                    //Case is in: add a CD orchestra
                    CDOrchestral newCDOrchestral = new CDOrchestral();
                    newCDOrchestral.Save(this);

                    //Inserts a new classical and orcenstral entry into database
                    productDB.UpdateProduct(newCDOrchestral.ProductUPC, newCDOrchestral.ProductPrice, newCDOrchestral.ProductTitle, newCDOrchestral.ProductQuantity, newCDOrchestral.GetType().Name, out OKFlag);
                    productDB.UpdateCDClassical(newCDOrchestral.ProductUPC, newCDOrchestral.CDClassicalLabel, newCDOrchestral.CDClassicalArtists);
                    productDB.UpdateCDOrchestra(newCDOrchestral.ProductUPC, newCDOrchestral.CDOrchestralConductor);

                    MessageBox.Show("The update was successful!");
                    //pList.addProduct(newCDOrchestral);
                    break;
                case 4:
                    //Case is in: add a CD Chamber
                    //Run checks and break on failure
                    if (!newVal.ProductTextCheck()|| !newVal.CDClassicalTextCheck() || !newVal.CDChamberTextCheck())
                    {
                        break;
                    }
                    CDChamber newCDChamber = new CDChamber();
                    newCDChamber.Save(this);

                    //Inserts a new classical and chamber entry into database
                    productDB.UpdateProduct(newCDChamber.ProductUPC, newCDChamber.ProductPrice, newCDChamber.ProductTitle, newCDChamber.ProductQuantity, newCDChamber.GetType().Name, out OKFlag);
                    productDB.UpdateCDClassical(newCDChamber.ProductUPC, newCDChamber.CDClassicalLabel, newCDChamber.CDClassicalArtists);
                    productDB.UpdateCDChamber(newCDChamber.ProductUPC, newCDChamber.CDChamberInstruments);                    
                    MessageBox.Show("The update was successful!");
                    //pList.addProduct(newCDChamber);
                    break;
            }
        }
    }    
}
