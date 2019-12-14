﻿using System;
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

namespace BookCDDVDShop
{
    public partial class frmBookCDDVDShop : Form
    {
        private int addState;
        private int mode;
        ProductList pList;
        ProductDB productDB;
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
            //Check if the serializable file exists
            if (File.Exists("BookCDDVDShopData"))
            {
                SFManager.readFromFile(ref pList, "BookCDDVDShopData");
            }
            
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
                    if (!productTextCheck() || !bookTextCheck())
                    {
                        MessageBox.Show("One of the fields was not entered in a usable form. Please try again!");
                        break;
                    }
                    Book newBook = new Book();
                    newBook.Save(this);
                    MessageBox.Show(newBook.ToString());

                    //Adds to the database
                    productDB.InsertProduct(newBook.ProductUPC, newBook.ProductPrice, newBook.ProductTitle, newBook.ProductQuantity, newBook.GetType().Name);
                    productDB.InsertBook(newBook.ProductUPC, newBook.BookISBN, newBook.BookAuthor, newBook.BookPages);

                    //pList.addProduct(newBook);
                    break;
                case 1:
                    //Case is in: add a CIS Book
                    //Run checks and break on failure
                    if (!productTextCheck() || !bookCISTextCheck() || !bookTextCheck())
                    {
                        break;
                    }
                    BookCIS newBookCIS = new BookCIS();
                    newBookCIS.Save(this);

                    //Inserts a new CISBook into the database
                    productDB.InsertProduct(newBookCIS.ProductUPC, newBookCIS.ProductPrice, newBookCIS.ProductTitle, newBookCIS.ProductQuantity, newBookCIS.GetType().Name);
                    productDB.InsertBook(newBookCIS.ProductUPC, newBookCIS.BookISBN, newBookCIS.BookAuthor, newBookCIS.BookPages);
                    productDB.InsertBookCIS(newBookCIS.ProductUPC, newBookCIS.BookCISArea);
                    //pList.addProduct(newBookCIS);
                    break;
                case 2:
                    //Case is in: add a DVD
                    //Run checks and break on failure
                    if (!productTextCheck() || !DVDTextCheck())
                    {
                        break;
                    }
                    DVD newDVD = new DVD();
                    newDVD.Save(this);

                    //Inserts a new DVD and Product information into Database
                    productDB.InsertProduct(newDVD.ProductUPC, newDVD.ProductPrice, newDVD.ProductTitle, newDVD.ProductQuantity, newDVD.GetType().Name);
                    productDB.InsertDVD(newDVD.ProductUPC, newDVD.DVDActor, Convert.ToDateTime(newDVD.DVDReleaseDate), newDVD.DVDRunTime);
                    //pList.addProduct(newDVD);
                    break;
                case 3:
                    //Run checks and break on failure
                    if (!productTextCheck() || !CDClassicalTextCheck() || !CDOrchestralTextCheck())
                    {
                        break;
                    }
                    //Case is in: add a CD orchestra
                    CDOrchestral newCDOrchestral = new CDOrchestral();
                    newCDOrchestral.Save(this);

                    //Inserts a new classical and orcenstral entry into database
                    productDB.InsertCDClassical(newCDOrchestral.ProductUPC, newCDOrchestral.CDClassicalLabel, newCDOrchestral.CDClassicalArtists);
                    productDB.InsertCDOrchestra(newCDOrchestral.ProductUPC, newCDOrchestral.CDOrchestralConductor);

                    //pList.addProduct(newCDOrchestral);
                    break;
                case 4:
                    //Case is in: add a CD Chamber
                    //Run checks and break on failure
                    if (!productTextCheck()|| !CDClassicalTextCheck() || !CDChamberTextCheck())
                    {
                        break;
                    }
                    CDChamber newCDChamber = new CDChamber();
                    newCDChamber.Save(this);

                    //Inserts a new classical and chamber entry into database
                    productDB.InsertCDClassical(newCDChamber.ProductUPC, newCDChamber.CDClassicalLabel, newCDChamber.CDClassicalArtists);
                    productDB.InsertCDChamber(newCDChamber.ProductUPC, newCDChamber.CDChamberInstruments);
                    //pList.addProduct(newCDChamber);
                    break;
            }
        }
        private bool productTextCheck()
        {
            int UPCParsed, quantityParsed;
            decimal priceParsed;
            //Check for blank text
            if (txtProductUPC.Text == "" || txtProductPrice.Text == "" || txtProductTitle.Text == "" || txtProductQuantity.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //check for int parsing
            else if (!int.TryParse(txtProductUPC.Text, out UPCParsed) || !int.TryParse(txtProductQuantity.Text, out quantityParsed))
            {
                MessageBox.Show("Error! UPC field must contain a valid integer value");
                return false;
            }            //check for negative values
            else if (UPCParsed < 0 || quantityParsed < 0)
            {
                MessageBox.Show("Error! UPC field must contain a non-negative integer value or quantity must not be negative");
                return false;
            }
            //Regex to check for a dollar decimal value (cents optional) -Source: RegexBuddy.com
            //check if dollar value is a valid decimal in case regex fails
            else if (!Decimal.TryParse(txtProductPrice.Text, out priceParsed))
                return false;
            //check if dollar value is a negative value
            else if (priceParsed < 0)
            {
                MessageBox.Show("Error! Product Price cannot be negative");
                return false;
            }
            return true;
        }

        //VALIDATION CHECKS FOR ALL CATEGORIES

        //Check for Book
        private bool bookTextCheck()
        {
            int ISBNLeftParsed, ISBNRightParsed, PageParsed;
            //check for blank text
            if (txtBookAuthor.Text == "" || txtBookISBNLeft.Text == "" || txtBookISBNRight.Text == "" || txtBookPages.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Check for int values
            else if (!int.TryParse(txtBookISBNLeft.Text, out ISBNLeftParsed) || !int.TryParse(txtBookISBNRight.Text, out ISBNRightParsed) || !int.TryParse(txtBookPages.Text, out PageParsed))
            {
                MessageBox.Show("Error! Please enter valid numeric values for ISBN and page count");
                return false;
            }
            //Check for numeric accuracy (negatives, proper number of digits)
            else if (PageParsed < 1 || ISBNLeftParsed < 1000 || ISBNLeftParsed > 9999 || ISBNRightParsed < 1000 || ISBNRightParsed > 9999) 
            {
                MessageBox.Show("Error! Please do not use negative numbers for page count, and use 4-digit numbers for each part of ISBN");
                return false;
            }
            return true;
        }


        
        //Check for book CIS
        private bool bookCISTextCheck()
        {
            if (txtBookCISArea.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Regex to disallow special characters of any kind
            else if (!Regex.IsMatch(txtBookCISArea.Text, "[a-z ]+", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Error! Please do not use special characters in CIS Book Area field");
                return false;
            }
            return true;
        }

        //Check for CD Classical
        private bool CDClassicalTextCheck()
        {
            if (txtCDClassicalArtists.Text == "" || txtCDClassicalLabel.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Regex to allow only capital and lowercase letters and hyphens for people's names
            else if (!Regex.IsMatch(txtCDClassicalArtists.Text, "[a-z- ]+", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Error! Name fields can only contain letters and hyphens");
                return false;
            }
            return true;
        }

        //Check for CD Orchestral
        private bool CDOrchestralTextCheck()
        {
            if (txtCDOrchestraConductor.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Regex to allow only capital and lowercase letters and hyphens for people's names
            else if (!Regex.IsMatch(txtCDOrchestraConductor.Text, "[a-z- ]+", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Error! Name fields can only contain letters and hyphens");
                return false;
            }
            return true;
        }

        //Check for CD Chamber
        private bool CDChamberTextCheck()
        {
            //Check for blank instrument list
            if (txtCDChamberInstrumentList.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Regex to allow for only words seperated by commas, ignoring case
            else if (!Regex.IsMatch(txtCDChamberInstrumentList.Text, "[a-z, ]+", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Error! CD Chamber Instrument list must not include special characters or numbers and each instrument can only be seperated by a comma");
                return false;
            }
            return true;
        }

        //Check for DVD
        private bool DVDTextCheck()
        {
            if (txtDVDLeadActor.Text == "" || txtDVDReleaseDate.Text == "" || txtDVDRunTime.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Regex to allow only capital and lowercase letters and hyphens for people's names
            else if (!Regex.IsMatch(txtDVDLeadActor.Text, "[a-z- ]+", RegexOptions.IgnoreCase))
                return false;
            //regex that checks for date in mm/dd/yyyy format
            else if (!Regex.IsMatch(txtDVDReleaseDate.Text, @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$"))
            {
                MessageBox.Show("Error! DVD Release Date must be in format mm/dd/yyyy");
                return false;
            }
            return true;
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
                    //pList.displayProduct(UPCParsed).Display(this);
                    break;
                case 2:
                    //delete mode

                    productDB.Delete(UPCParsed);
                   // pList.removeProduct(UPCParsed);
                    break;
                case 3:
                    //edit mode


                    OKFlag = false;
                 
                    dbResult = productDB.SelectProduct(UPCParsed, out OKFlag);
                    
                    if (OKFlag)
                    {
                        while (dbResult.Read())
                        {
                            DBProduct tmp = new DBProduct(Convert.ToInt32(dbResult[0]), Convert.ToDecimal(dbResult[1]),
                            dbResult[2].ToString(), Convert.ToInt32(dbResult[3]));
                            tmp.Display(this);
                        }
                       // pList.addProduct(type(test.GetData(0), test.GetData(1), test.GetData(2), test.GetData(4)));
                      //  Product(test.GetData(0), test.GetData(1), test.GetData(2), test.GetData(4));
                    }

                   // pList.displayProduct(UPCParsed).Display(this);
                    break;

            }
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
    }    
}