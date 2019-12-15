using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCDDVDShop
{
    public class Validation
    {
        private frmBookCDDVDShop f;

        public Vallidation(frmBookCDDVDShop parentForm)
        {
            f = parentForm;
        }
         //Check for book CIS
        public bool bookCISTextCheck()
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
