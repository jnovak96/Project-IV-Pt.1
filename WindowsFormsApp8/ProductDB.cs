/*
* Product Database Class BEST TO DATE
* Authors: Nick Filauro & Erika Gepilano
* April 2016 * Version 1
* 
* Updated 11/18/2016 * Version 2 * Elliot Stoner
* Updated 06/17/2017 * Version 3 * Frank Friedman
* Updated 06/30/2018 * Version 4 * Frank Friedman
* Updated 06/20/2019 * Version 5 * Frank Friedman
* Updated 12/02/2019 * Version 6 * Frank Friedman  BEST VERSION TO DATE
* 
* Purpose: A class that interacts and performs database operations for Product
* in a Microsoft Access database using an OLEDB Data Reader.
* It will contain methods for CRUD (Create, Read, Update, Delete) operations.
* 
* !! Requirements !!
* You must have the Access Database Engine installed on the system you are running the program on.
* https://www.microsoft.com/en-us/download/details.aspx?id=13255
* 
* No constructors were written
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BookCDDVDShop
{
    class ProductDB
    {
        string dbProductType = "";  // type of record found in data base: Book, BookCIS, CDChamber, CDOrchestra, DVD
        string dbStringProduct = "";
        string fieldsFound = "";

        // Connection string for ProductDB (type: Microsoft Access) in the Resources folder
        string strConnection = "provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=../Debug/ProductDB-1.accdb";
        //string strConnection = "provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source= " +
        //    "L:\\ALL MY DATA\\Frank's Syllabus\\AAA CIS 3309 CSharp F19-S20\\CIS 3309 All Projects 2019-20\\BookCDDVD Project (adapted Jupin) DB Version\\ProductDB.accdb";

        // *********** INSERTION METHODS **********
        // 1 Inserts a new record for Product in the Product table with parameters UPC, Price, Title, Quantity, and
        //   productType
        public bool InsertProduct(int UPC, decimal price, string title, int quantity, string productType)
        {
            // SQL insert statement for Product
            // String dobStringTemp = ProductBirthdate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)

            string strInsertProduct = "INSERT INTO Product (fldUPC, fldPrice, fldTitle, fldQuantity, fldProductType) " +
                "VALUES(" + UPC + " , " + price + " , '" + title + "', " + quantity + ", '" + productType + "');";
            /*
            string strInsertBook = "INSERT INTO Book (fldUPC, fldISBN, fldauthor, fldpages) " +
                "VALUES(" + UPC + ", '" + ISBN + "', '" + author + "', " + pages + " );";

            */
            // Convert.ToDateTime(ProductBirthDate)
            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strInsertProduct, myConnection);
            OleDbDataReader myDataReader;

            try
            {
                myConnection.Open();
                myDataReader = myCommand.ExecuteReader();
                myDataReader.Close();
                return true; // returns true if Insert was successful
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("There was an Insert Product error: " + ex.Message,
                    "Product Insert Failed", MessageBoxButtons.OK);
                return false; // returns false if Insert was unsuccessful
            }
            finally
            {
                myConnection.Close();
            }

        }  // end InsertProduct


        // 2 Inserts a new record into Book table with parameters UPC and ISBN, Author, Pages
        public bool InsertBook(int UPC, int ISBN, string author, int pages)
        {
            string strInsertBook = "INSERT INTO Book (fldUPC, fldISBN, fldAuthor, fldPages) " +
                "VALUES(" + UPC + ", '" + ISBN + "', '" + author + "', " + pages + " );";

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strInsertBook, myConnection);
            OleDbDataReader myDataReader;

            try
            {
                myConnection.Open();
                myDataReader = myCommand.ExecuteReader();
                myDataReader.Close();
                return true; // returns true if Insert was successful
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("There was an Insert Book error: " + ex.Message,
                     "Book Insert Failed", MessageBoxButtons.OK);
                return false;
            }
            finally
            {
                myConnection.Close();
            }

        }  // end InsertBook


        // 3 Inserts a new record into BookCIS table with parameters UPC and CISArea
        public bool InsertBookCIS(int UPC, string CISArea)
        {
            string strInsertBookCIS = "INSERT INTO BookCIS (fldUPC, fldCISArea) " +
                "VALUES(" + UPC + ", '" + CISArea + "' );";

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strInsertBookCIS, myConnection);
            OleDbDataReader myDataReader;

            try
            {
                myConnection.Open();
                myDataReader = myCommand.ExecuteReader();
                myDataReader.Close();
                return true; // returns true if Insert was successful
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("There was an Insert BookCIS error: " + ex.Message,
                    "BookCIS Insert Failed", MessageBoxButtons.OK);
                return false; // returns false if Insert was unsuccessful
            }
            finally
            {
                myConnection.Close();
            }

        }  // end InsertBookCIS


        // 4 Inserts a new record into DVD table with parameters UPC and Lead Actor, Release Date and Run Time
        public bool InsertDVD(int UPC, string lead, DateTime relDate, int runTime)
        {
            string strInsertDVD = "INSERT INTO DVD (fldUPC, fldLeadActor, fldReleaseDate, fldRunTime) " +
                "VALUES(" + UPC + ", '" + lead + "', '" + relDate + "', " + runTime + " );";

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strInsertDVD, myConnection);
            OleDbDataReader myDataReader;

            try
            {
                myConnection.Open();
                myDataReader = myCommand.ExecuteReader();
                myDataReader.Close();
                return true; // returns true if Insert was successful
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("There was an Insert DVD error: " + ex.Message,
                   "DVD Insert Failed", MessageBoxButtons.OK);
                return false; // returns false if Insert was unsuccessful
            }
            finally
            {
                myConnection.Close();
            }

        }  // end InsertDVD



        // 5 Inserts a new record into CDClassical table with parameters ProductUPC and 
        //     CDClassical Label and CDClassical Artists
        public bool InsertCDClassical(int UPC, string label, string artists)
        {
            string strInsertCDClassical = "INSERT INTO CDCLASSICAL (fldUPC, fldLabel, fldArtists) " +
                "VALUES(" + UPC + ", '" + label + "', '" + artists + "' );";
            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strInsertCDClassical, myConnection);
            OleDbDataReader myDataReader;

            try
            {
                myConnection.Open();
                myDataReader = myCommand.ExecuteReader();
                myDataReader.Close();
                return true; // returns true if Insert was successful
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("There was an Insert CDCLassical error: " + ex.Message,
                    "CDClassical Insert Failed", MessageBoxButtons.OK);
                return false; // returns false if Insert was unsuccessful
            }
            finally
            {
                myConnection.Close();
            }

        }  // end InsertCDClassical



        // 6 Inserts a new record into CD Chamber Music table with parameters ProductUPC and InstrumentList
        public bool InsertCDChamber(int UPC, string instrumentList)
        {
            string strInsertCDChamber = "INSERT INTO CDChamber (fldUPC, fldInstrumentList) " +
                "VALUES(" + UPC + ", '" + instrumentList + "');";
            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strInsertCDChamber, myConnection);
            OleDbDataReader myDataReader;

            try
            {
                myConnection.Open();
                myDataReader = myCommand.ExecuteReader();
                myDataReader.Close();
                return true; // returns true if Insert was successful
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("There was an Insert CDChamber error: " + ex.Message,
                    "CDChamber Insert Failed", MessageBoxButtons.OK);
                return false; // returns false if Insert was unsuccessful
            }
            finally
            {
                myConnection.Close();
            }

        }  // end InsertCDChamber


        // 7 Inserts a new record into CDOrchestra table with parameters ProductUPC and Conductor
        public bool InsertCDOrchestra(int UPC, string Conductor)
        {
            string strInsertCDOrchestra = "INSERT INTO CDOrchestra (fldUPC, fldConductor) " +
                "VALUES(" + UPC + ", '" + Conductor + "') ;";
            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strInsertCDOrchestra, myConnection);
            OleDbDataReader myDataReader;

            try
            {
                myConnection.Open();
                myDataReader = myCommand.ExecuteReader();
                myDataReader.Close();
                return true; // returns true if Insert was successful
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("There was an Insert CDOrchestra error: " + ex.Message,
                     "CDOrchestral Insert Failed", MessageBoxButtons.OK);
                return false; // returns false if Insert was unsuccessful
            }
            finally
            {
                myConnection.Close();
            }
        }  // end InsertCDOrchestra

        // ********** End of INSERT methods **********t




        public OleDbDataReader SelectProduct(int ProductUPC, out bool OKFlag)
        {
            // CURRENTLY NOT USED
            string strSelectProduct = "SELECT * FROM Product WHERE product.fldUPC= " + ProductUPC;

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strSelectProduct, myConnection);
            OleDbDataReader myDataReader;

            try
            {
                myConnection.Open();
                myDataReader = myCommand.ExecuteReader();
                if (myDataReader.HasRows == false) OKFlag = false;
                else OKFlag = true; // returns true if Select was successful
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("There was an Select Product error: " + ex.Message,
                     "Product Select Failed", MessageBoxButtons.OK);
                myConnection.Close();
                OKFlag = false; // returns false if Select was unsuccessful
                myDataReader = null;
            }

            return myDataReader;
        }  // end SelectProduct


    } // end of Product class
} // end of namespace