using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCDDVDShop
{
    class FormController
    {
        private frmBookCDDVDShop f;

        // Parametrized Constructor
        public FormController(frmBookCDDVDShop parentForm)
        {
            f = parentForm;
        }  // end Parameterized Constructor

        // Resets form to initial state after form is loaded or
        //    an add operation is performed

        public static void resetForm(frmBookCDDVDShop f)
        {
            //  Reset button components
            f.btnClear.Enabled = true;
            f.btnDelete.Enabled = true;
            f.btnEdit.Enabled = true;
            f.btnFind.Enabled = true;
            f.btnExit.Enabled = true;
            f.btnUpdateProduct.Enabled = false;
            f.btnSaveProduct.Enabled = false;
            f.btnEnterUPC.Enabled = true;
            f.btnCreateBook.Enabled = true;
            f.btnCreateBook.Text = "Create Book";
            f.btnCreateBookCIS.Enabled = true;
            f.btnCreateBookCIS.Text = "Create Book CIS";
            f.btnCreateCDOrchestra.Enabled = true;
            f.btnCreateCDOrchestra.Text = "Create CD Orchestra";
            f.btnCreateCDChamber.Enabled = true;
            f.btnCreateCDChamber.Text = "Create CD Chamber";
            f.btnCreateDVD.Enabled = true;
            f.btnCreateDVD.Text = "Create DVD";

            // Reset group components
            f.grpProduct.Enabled = true;
            f.grpProduct.BackColor = Color.Gainsboro;
            f.grpCDClassical.Enabled = false;
            f.grpCDClassical.BackColor = Color.Gainsboro;
            f.grpBook.Enabled = false;
            f.grpBook.BackColor = Color.Gainsboro;
            f.grpCDChamber.Enabled = false;
            f.grpCDChamber.BackColor = Color.Gainsboro;
            f.grpCDOrchestra.Enabled = false;
            f.grpCDOrchestra.BackColor = Color.Gainsboro;
            f.grpBookCIS.BackColor = Color.Gainsboro;
            f.grpBookCIS.Enabled = false;
            f.grpDVD.BackColor = Color.Gainsboro;
            f.grpDVD.Enabled = false;


            // Reset Text boxes
            // f.txtProductUPC.Focus();
            f.txtProductUPC.Enabled = false;
            f.txtProductPrice.Enabled = false;
            f.txtProductTitle.Enabled = false;
            f.txtProductQuantity.Enabled = false;
            f.txtBookISBNLeft.Enabled = false;
            f.txtBookISBNRight.Enabled = false;
            f.txtBookAuthor.Enabled = false;
            f.txtBookPages.Enabled = false;
            f.txtDVDLeadActor.Enabled = false;
            f.txtDVDReleaseDate.Enabled = false;
            f.txtDVDRunTime.Enabled = false;
            f.txtCDClassicalLabel.Enabled = false;
            f.txtCDClassicalArtists.Enabled = false;
            f.txtCDChamberInstrumentList.Enabled = false;
            f.txtCDOrchestraConductor.Enabled = false;
            f.txtBookCISArea.Enabled = false;
        } // end resetForm


        // Code to deactivate form secions for the entities in the Product Hierarchy

        // Deactivate all but Book
        public static void deactivateAllButBook(frmBookCDDVDShop f)
        {
            FormController.activateBook(f);
            FormController.deactivateCDOrchestra(f);
            FormController.deactivateBookCIS(f);
            FormController.deactivateCDChamber(f);
            FormController.deactivateDVD(f);
            FormController.deactivateAddButtons(f);
            FormController.deactivateCDClassical(f);
        }  // end deactivateAllButBook

        // Deactivate all but BookCIS
        public static void deactivateAllButBookCIS(frmBookCDDVDShop f)
        {
            FormController.activateBookCIS(f);
            FormController.deactivateBook(f);
            FormController.deactivateCDOrchestra(f);
            FormController.deactivateCDChamber(f);
            FormController.deactivateDVD(f);
            FormController.deactivateAddButtons(f);
            FormController.deactivateCDClassical(f);

        }  // end deactivateAllButBookCIS

        // Deactivtae all but DVD
        public static void deactivateAllButDVD(frmBookCDDVDShop f)
        {
            FormController.activateDVD(f);
            FormController.deactivateBook(f);
            FormController.deactivateBookCIS(f);
            FormController.deactivateCDChamber(f);
            FormController.deactivateCDOrchestra(f);
            FormController.deactivateCDClassical(f);
            FormController.deactivateAddButtons(f);
        }  // end deactivateAllButDVD

        // Deactivate all but CDOrchestra
        public static void deactivateAllButCDOrchestra(frmBookCDDVDShop f)
        {
            FormController.activateCDOrchestra(f);
            FormController.deactivateBook(f);
            FormController.deactivateBookCIS(f);
            FormController.deactivateCDChamber(f);
            FormController.deactivateDVD(f);
            FormController.deactivateAddButtons(f);
        }  // end deactivateAllButCDChamber


        // Deactivate all but CDChamber
        public static void deactivateAllButCDChamber(frmBookCDDVDShop f)
        {
            FormController.activateCDChamber(f);
            FormController.deactivateBook(f);
            FormController.deactivateBookCIS(f);
            FormController.deactivateCDOrchestra(f);
            FormController.deactivateDVD(f);
            FormController.deactivateAddButtons(f);
        }  // end deactivateAllButCDOrchestra

        // Deactivate all but update product essential buttons
        public static void enableUpdateProduct(frmBookCDDVDShop f)
        {
            f.btnSaveProduct.Enabled = false;
            f.btnUpdateProduct.Enabled = true;
            f.txtProductUPC.Enabled = false;
        }


        // Activates and deactivates necessary form buttons
        //    when in add mode
        public static void formAddMode(frmBookCDDVDShop f)
        {
            f.btnClear.Enabled = true;
            f.btnDelete.Enabled = false;
            f.btnEdit.Enabled = false;
            f.btnFind.Enabled = false;
        }  // end formAddMode


        // Enable/disable buttons when not in edit mode
        public static void activateAddButtons(frmBookCDDVDShop f)
        {
            f.btnCreateCDOrchestra.Enabled = true;
            f.btnCreateBookCIS.Enabled = true;
            f.btnCreateCDChamber.Enabled = true;
            f.btnCreateBook.Enabled = true;
            f.btnCreateDVD.Enabled = true;
        }  // end activateAddButtons


        // Enable/disable buttons when not in edit mode
        public static void deactivateAddButtons(frmBookCDDVDShop f)
        {
            f.btnCreateCDOrchestra.Enabled = false;
            f.btnCreateDVD.Enabled = false;
            f.btnCreateCDChamber.Enabled = false;
            f.btnCreateBook.Enabled = false;
            f.btnCreateBookCIS.Enabled = false;
        }  // end deactivateAddButtons


        //  Enables Product textboxes and highlights the Product groupbox
        public static void activateProduct(frmBookCDDVDShop f)
        {
            f.grpProduct.Enabled = true;
            f.grpProduct.BackColor = Color.LimeGreen;
            f.txtProductUPC.Enabled = true;
            f.txtProductUPC.Enabled = true;
            f.txtProductTitle.Enabled = true;
            f.txtProductQuantity.Enabled = true;
            f.txtProductPrice.Enabled = true;
            f.btnEnterUPC.Enabled = false;
            f.btnSaveProduct.Enabled = true;

        }  // end activateProduct


        //  Enables CDClassical textboxes and highlights the CDClassical groupbox
        public static void activateCDClassical(frmBookCDDVDShop f)
        {
            activateProduct(f);
            f.grpCDClassical.Enabled = true;
            f.grpCDClassical.BackColor = Color.LimeGreen;
            f.grpBook.BackColor = Color.Red;
            f.grpDVD.BackColor = Color.Red;
            f.txtCDClassicalLabel.Enabled = true;
            f.txtCDClassicalArtists.Enabled = true;
            f.btnEnterUPC.Enabled = false;

        }  // end ActivateCDCLassical


        // Enables Book textboxes and highlights the Book groupbox
        public static void activateBook(frmBookCDDVDShop f)
        {
            activateProduct(f);
            f.grpBook.Enabled = true;
            f.grpBook.BackColor = Color.LimeGreen;
            f.grpCDClassical.BackColor = Color.Red;
            f.grpDVD.BackColor = Color.Red;
            f.txtBookISBNLeft.Enabled = true;
            f.txtBookISBNRight.Enabled = true;
            f.txtBookAuthor.Enabled = true;
            f.txtBookPages.Enabled = true;

        }  // end activateBook


        // Enables DVD textboxes and highlights the DVD groupbox
        public static void activateDVD(frmBookCDDVDShop f)
        {
            activateProduct(f);
            f.grpDVD.Enabled = true;
            f.grpDVD.BackColor = Color.LimeGreen;
            f.grpCDClassical.BackColor = Color.Red;
            f.grpBook.BackColor = Color.Red;
            f.txtDVDLeadActor.Enabled = true;
            f.txtDVDReleaseDate.Enabled = true;
            f.txtDVDRunTime.Enabled = true;

        }  // end activateBook


        // Enables CDCl Chamber Nusic textboxes and highlights the CDCl Chamber Music groupbox
        public static void activateCDChamber(frmBookCDDVDShop f)
        {
            activateCDClassical(f);   // CD Classical must be activated too
            f.grpCDChamber.Enabled = true;
            f.grpCDChamber.BackColor = Color.LimeGreen;
            f.txtCDChamberInstrumentList.Enabled = true;
        }  // end activateCDCLChamber


        // Enables Worker textboxes and highlights the Graduate Student groupbox
        public static void activateCDOrchestra(frmBookCDDVDShop f)
        {
            activateCDClassical(f);  // CDClassical must be activated too
            f.grpCDOrchestra.Enabled = true;
            f.grpCDOrchestra.BackColor = Color.LimeGreen;
            f.txtCDOrchestraConductor.Enabled = true;
        }  // end activateCDClOrch


        // Enables BookCIS textboxes and highlights the BookCIS groupbox
        public static void activateBookCIS(frmBookCDDVDShop f)
        {
            activateBook(f);  // Book must be activated too
            f.grpBookCIS.Enabled = true;
            f.grpBookCIS.BackColor = Color.LimeGreen;
            f.txtBookCISArea.Enabled = true;
        }  // end activateWorker

        // ***** Disables Product textboxes and highlights the Product groupbox
        public static void deactivateProduct(frmBookCDDVDShop f)
        {
            deactivateCDClassical(f);
            deactivateBook(f);
            deactivateDVD(f);
            f.grpProduct.Enabled = false;
            f.grpProduct.BackColor = Color.Red;
        }  // end deactivateProduct



        // Disables CDClassical textboxes and groupbox
        public static void deactivateCDClassical(frmBookCDDVDShop f)
        {
            deactivateCDChamber(f);
            deactivateCDOrchestra(f);
            f.grpCDClassical.Enabled = false;
            f.grpCDClassical.BackColor = Color.Red;
        }  // end deactivateCDClassical


        // Disables Book textboxes and groupbox
        public static void deactivateBook(frmBookCDDVDShop f)
        {
            deactivateBookCIS(f);
            f.grpBook.Enabled = false;
            f.grpBook.BackColor = Color.Red;
        }  // end deactivateBook


        // Disables CDClChamber textboxes and highlights the CD Chamber groupbox
        public static void deactivateCDChamber(frmBookCDDVDShop f)
        {
            f.grpCDChamber.Enabled = false;
            f.grpCDChamber.BackColor = Color.Red;
        }  // end deactivateCDChamber


        // Disables CD Orchestra textboxes and the CD Orchestra groupbox
        public static void deactivateCDOrchestra(frmBookCDDVDShop f)
        {
            f.grpCDOrchestra.Enabled = false;
            f.grpCDOrchestra.BackColor = Color.Red;
        }  // end deativateCDClOrch


        // Disables textboxes groupbox
        public static void deactivateBookCIS(frmBookCDDVDShop f)
        {
            f.grpBookCIS.Enabled = false;
            f.grpBookCIS.BackColor = Color.Red;
        }  // end deativateBookCIS


        // Disables  textboxes groupbox
        public static void deactivateDVD(frmBookCDDVDShop f)
        {
            f.grpDVD.Enabled = false;
            f.grpDVD.BackColor = Color.Red;
        }  // end deativateBookCIS


        // Clear all textboxes on the form
        public static void clear(frmBookCDDVDShop f)
        {
            f.txtProductUPC.Text = "";
            f.txtProductPrice.Text = "";
            f.txtProductTitle.Text = "";
            f.txtProductQuantity.Text = "";
            f.txtBookISBNLeft.Text = "";
            f.txtBookISBNRight.Text = "";
            f.txtBookAuthor.Text = "";
            f.txtBookPages.Text = "";
            f.txtCDClassicalLabel.Text = "";
            f.txtCDClassicalArtists.Text = "";
            f.txtCDChamberInstrumentList.Text = "";
            f.txtCDOrchestraConductor.Text = "";
            f.txtBookCISArea.Text = "";
            f.txtDVDLeadActor.Text = "";
            f.txtDVDReleaseDate.Text = "";
            f.txtDVDRunTime.Text = "";
            resetForm(f);
        } // end Clear

    }  // end FormController class
}
