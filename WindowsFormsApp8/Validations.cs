using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * This Class handles all the validations for the form. 
 * 
 * CREATED 12/14/2019
 * */

namespace BookCDDVDShop
{
    public class Validations
    {
        private frmBookCDDVDShop f;

        //Constructor
        public Validations(frmBookCDDVDShop parentForm)
        {
            f = parentForm;
        }

        //Checks the fields associated with product are filled in
        public bool ProductTextCheck()
        {
            int UPCParsed, quantityParsed;
            decimal priceParsed;
            //Check for blank text
            if (f.txtProductUPC.Text == "" || f.txtProductPrice.Text == "" || f.txtProductTitle.Text == "" || f.txtProductQuantity.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //check for int parsing
            else if (!int.TryParse(f.txtProductUPC.Text, out UPCParsed) || !int.TryParse(f.txtProductQuantity.Text, out quantityParsed))
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
            else if (!Decimal.TryParse(f.txtProductPrice.Text, out priceParsed))
                return false;
            //check if dollar value is a negative value
            else if (priceParsed < 0)
            {
                MessageBox.Show("Error! Product Price cannot be negative");
                return false;
            }
            return true;
        }


        //Validates all the fields for book are filled in
        public bool BookTextCheck()
        {
            int ISBNLeftParsed, ISBNRightParsed, PageParsed;
            //check for blank text
            if (f.txtBookAuthor.Text == "" || f.txtBookISBNLeft.Text == "" || f.txtBookISBNRight.Text == "" || f.txtBookPages.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Check for int values
            else if (!int.TryParse(f.txtBookISBNLeft.Text, out ISBNLeftParsed) || !int.TryParse(f.txtBookISBNRight.Text, out ISBNRightParsed) || !int.TryParse(f.txtBookPages.Text, out PageParsed))
            {
                MessageBox.Show("Error! Please enter valid numeric values for ISBN and page count");
                return false;
            }
            //Check for numeric accuracy (negatives, proper number of digits)
            else if (PageParsed < 1 || ISBNLeftParsed.ToString().Length != 4 || ISBNRightParsed.ToString().Length != 4)
            {
                MessageBox.Show("Error! Please do not use negative numbers for page count, and use 4-digit numbers for each part of ISBN");
                return false;
            }
            return true;
        }

        //Check for book CIS
        public bool BookCISTextCheck()
        {
            if (f.txtBookCISArea.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Regex to disallow special characters of any kind
            else if (!Regex.IsMatch(f.txtBookCISArea.Text, "[a-z ]+", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Error! Please do not use special characters in CIS Book Area field");
                return false;
            }
            return true;
        }

        //Check for CD Classical
        public bool CDClassicalTextCheck()
        {
            if (f.txtCDClassicalArtists.Text == "" || f.txtCDClassicalLabel.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Regex to allow only capital and lowercase letters and hyphens for people's names
            else if (!Regex.IsMatch(f.txtCDClassicalArtists.Text, "[a-z- ]+", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Error! Name fields can only contain letters and hyphens");
                return false;
            }
            return true;
        }

        //Check for CD Orchestral
        public bool CDOrchestralTextCheck()
        {
            if (f.txtCDOrchestraConductor.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Regex to allow only capital and lowercase letters and hyphens for people's names
            else if (!Regex.IsMatch(f.txtCDOrchestraConductor.Text, "[a-z- ]+", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Error! Name fields can only contain letters and hyphens");
                return false;
            }
            return true;
        }

        //Check for CD Chamber
        public bool CDChamberTextCheck()
        {
            //Check for blank instrument list
            if (f.txtCDChamberInstrumentList.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Regex to allow for only words seperated by commas, ignoring case
            else if (!Regex.IsMatch(f.txtCDChamberInstrumentList.Text, "[a-z, ]+", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Error! CD Chamber Instrument list must not include special characters or numbers and each instrument can only be seperated by a comma");
                return false;
            }
            return true;
        }

        //Check for DVD
        public bool DVDTextCheck()
        {
            if (f.txtDVDLeadActor.Text == "" || f.txtDVDReleaseDate.Text == "" || f.txtDVDRunTime.Text == "")
            {
                MessageBox.Show("Error! Necessary text boxes cannot be blank");
                return false;
            }
            //Regex to allow only capital and lowercase letters and hyphens for people's names
            else if (!Regex.IsMatch(f.txtDVDLeadActor.Text, "[a-z- ]+", RegexOptions.IgnoreCase))
                return false;
            //regex that checks for date in mm/dd/yyyy format
            else if (!Regex.IsMatch(f.txtDVDReleaseDate.Text, @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$"))
            {
                MessageBox.Show("Error! DVD Release Date must be in format mm/dd/yyyy");
                return false;
            }
            return true;
        }
    }
}
