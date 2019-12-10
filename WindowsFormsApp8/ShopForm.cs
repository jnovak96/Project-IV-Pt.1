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

namespace BookCDDVDShop
{
    public partial class frmBookCDDVDShop : Form
    {
        private int addState;
        private int mode;
        ProductList pList;
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
            SFManager.writeToFile(pList, "BookCDDVDShopData");
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
                    pList.addProduct(newBook);
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
                    pList.addProduct(newBookCIS);
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
                    pList.addProduct(newDVD);
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
                    pList.addProduct(newCDOrchestral);
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
                    pList.addProduct(newCDChamber);
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
                pList.displayProduct(UPCParsed);
            else
            {
                MessageBox.Show("Error! UPC must be a valid numeric value");
                return;
            }

            switch (mode)
            {
                case 1:
                    //find mode
                    pList.displayProduct(UPCParsed).Display(this);
                    break;
                case 2:
                    //delete mode
                    pList.removeProduct(UPCParsed);
                    break;
                case 3:
                    //edit mode
                    pList.displayProduct(UPCParsed).Display(this);
                    break;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pList.ToString());
        }
    }    
}
