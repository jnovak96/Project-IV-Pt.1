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
            string strSelectProduct = "SELECT * FROM Product WHERE product.fldUPC= " + ProductUPC ;

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

        //Returns the result from the Book table that matches the UPC
        public OleDbDataReader SelectBook(int BookUPC, out bool OKFlag)
        {
            // CURRENTLY NOT USED
            string strSelectBook = "SELECT * FROM Book WHERE Book.fldUPC=" + BookUPC;

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strSelectBook, myConnection);
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
        }  // end SelectBook


        //Returns the result from the BookCIS table that matches the UPC number
        public OleDbDataReader SelectBookCIS(int UPC, out bool OKFlag)
        {
            // CURRENTLY NOT USED
            string strSelectBookCIS = "SELECT * FROM BookCIS WHERE BookCIS.fldUPC= " + UPC;

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strSelectBookCIS, myConnection);
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
        }  // end SelectBookCIS

        //Returns the result from the DVD table that matches the UPC number
        public OleDbDataReader SelectDVD(int UPC, out bool OKFlag)
        {
            // CURRENTLY NOT USED
            string strSelectDVD = "SELECT * FROM DVD WHERE DVD.fldUPC= " + UPC;

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strSelectDVD, myConnection);
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
                MessageBox.Show("There was an Select DVD error: " + ex.Message,
                     "DVD Select Failed", MessageBoxButtons.OK);
                myConnection.Close();
                OKFlag = false; // returns false if Select was unsuccessful
                myDataReader = null;
            }

            return myDataReader;
        }  // end SelectDVD

        //Returns the result from the CDClassical table that matches the UPC number
        public OleDbDataReader SelectCDClassical(int UPC, out bool OKFlag)
        {
            // CURRENTLY NOT USED
            string strSelectCDClassical = "SELECT * FROM CDClassical WHERE CDClassical.fldUPC= " + UPC;

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strSelectCDClassical, myConnection);
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
                MessageBox.Show("There was an Select CDClassical error: " + ex.Message,
                     "CDClassical Select Failed", MessageBoxButtons.OK);
                myConnection.Close();
                OKFlag = false; // returns false if Select was unsuccessful
                myDataReader = null;
            }

            return myDataReader;
        }  // end SelectCDClassical

        //Returns the result from the CDChamber table that matches the UPC number
        public OleDbDataReader SelectCDChamber(int UPC, out bool OKFlag)
        {
            // CURRENTLY NOT USED
            string strSelectCDChamber = "SELECT * FROM CDChamber WHERE CDChamber.fldUPC= " + UPC;

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strSelectCDChamber, myConnection);
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
                MessageBox.Show("There was an Select CDChamber error: " + ex.Message,
                     "CDChamber Select Failed", MessageBoxButtons.OK);
                myConnection.Close();
                OKFlag = false; // returns false if Select was unsuccessful
                myDataReader = null;
            }

            return myDataReader;
        }   //End SelectCDChamber

        //Returns the result from the CDOrchestra table that matches the UPC number
        public OleDbDataReader SelectCDOrchestra(int UPC, out bool OKFlag)
        {
            // CURRENTLY NOT USED
            string strSelectCDOrchestra = "SELECT * FROM CDOrchestra WHERE CDOrchestra.fldUPC= " + UPC;

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strSelectCDOrchestra, myConnection);
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
                MessageBox.Show("There was an Select CDOrchestra error: " + ex.Message,
                     "CDOrchestra Select Failed", MessageBoxButtons.OK);
                myConnection.Close();
                OKFlag = false; // returns false if Select was unsuccessful
                myDataReader = null;
            }

            return myDataReader;
        }   //End SelectCDOrchestra

        public OleDbDataReader SelectAllProduct( out bool OKFlag)
        {
            // CURRENTLY NOT USED
            string strSelectProduct = "SELECT * FROM Product";

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
                     "Product Select ALL Failed", MessageBoxButtons.OK);
                myConnection.Close();
                OKFlag = false; // returns false if Select was unsuccessful
                myDataReader = null;
            }

            return myDataReader;
        }  // end SelectAllProduct


        //Updates The information for a product in the database
        public void UpdateProduct(int UPC, decimal price, string title, int quantity, string productType, out bool OKFlag)
        {
            // CURRENTLY NOT USED
            string strUpdateProduct = "UPDATE PRODUCT "
                    + "SET " + "fldPrice=" + price + ", fldTitle='"+title + "'" + ", fldQuantity="+quantity           
                    + " WHERE  fldUPC=" + UPC + ";";

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strUpdateProduct, myConnection);
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
                MessageBox.Show("There was an Update Product error: " + ex.Message,
                     "Product Update Failed", MessageBoxButtons.OK);
                myConnection.Close();
                OKFlag = false; // returns false if Select was unsuccessful
                myDataReader = null;
            }

            //return myDataReader;
        }  // end UpdateProduct


        // Updates record in the Book table with parameters UPC and ISBN, Author, Pages
        public bool UpdateBook(int UPC, int ISBN, string author, int pages)
        {
            string strUpdateBook = "UPDATE Book SET fldISBN=" + ISBN + ", fldAuthor='" + author + "'" +", fldPages=" + pages +
                " WHERE fldUPC=" + UPC + ";";

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strUpdateBook, myConnection);
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
                MessageBox.Show("There was an Update Book error: " + ex.Message,
                     "Book Update Failed", MessageBoxButtons.OK);
                return false;
            }
            finally
            {
                myConnection.Close();
            }

        }  // end UpdateBook


        //Updates a BookCIS within the database based off UPC
        public bool UpdateBookCIS(int UPC, string CISArea)
        {
            string strUpdateBookCIS = "UPDATE BookCIS SET fldCISArea='" + CISArea + "'"
                + " WHERE fldUPC=" + UPC + ";";

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strUpdateBookCIS, myConnection);
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
                MessageBox.Show("There was an Update BookCIS error: " + ex.Message,
                    "BookCIS Update Failed", MessageBoxButtons.OK);
                return false; // returns false if Insert was unsuccessful
            }
            finally
            {
                myConnection.Close();
            }

        }  // end UpdateBookCIS




        // Updates a record in the DVD table with parameters UPC and Lead Actor, Release Date and Run Time
        public bool UpdateDVD(int UPC, string lead, DateTime relDate, int runTime)
        {

            string strUpdateDVD = "UPDATE DVD SET "
                + "fldLeadActor='" + lead +"'" + ", fldReleaseDate=" + "'"+ relDate.Date +"'" + ", fldRunTime=" + runTime +
                " WHERE fldUPC=" + UPC + ";";

            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strUpdateDVD, myConnection);
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
                MessageBox.Show("There was an Update DVD error: " + ex.Message,
                   "DVD Update Failed", MessageBoxButtons.OK);
                return false; // returns false if Insert was unsuccessful
            }
            finally
            {
                myConnection.Close();
            }

        }  // end UpdateDVD


        // Updates a record in CDClassical table with parameters ProductUPC and 
        //     CDClassical Label and CDClassical Artists
        public bool UpdateCDClassical(int UPC, string label, string artists)
        {
            string strUpdateCDClassical = "Update CDCLASSICAL SET "
                + "fldLabel='" + label +"'" + ", fldArtists='" + artists +"'" +
                " WHERE fldUPC=" + UPC + ";";
            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strUpdateCDClassical, myConnection);
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
                MessageBox.Show("There was an Update CDCLassical error: " + ex.Message,
                    "CDClassical Update Failed", MessageBoxButtons.OK);
                return false; // returns false if Insert was unsuccessful
            }
            finally
            {
                myConnection.Close();
            }

        }  // end UpdateCDClassical


        //Updates a record in CD Chamber Music table with parameters ProductUPC and InstrumentList
        public bool UpdateCDChamber(int UPC, string instrumentList)
        {
            string strUpdateCDChamber = "UPDATE CDChamber SET fldInstrumentList='" + instrumentList + "'" +
                " WHERE fldUPC=" + UPC + ";";
            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strUpdateCDChamber, myConnection);
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
                MessageBox.Show("There was an Update CDChamber error: " + ex.Message,
                    "CDChamber Update Failed", MessageBoxButtons.OK);
                return false; // returns false if Insert was unsuccessful
            }
            finally
            {
                myConnection.Close();
            }

        }  // end UpdateCDChamber


        // Updates a record in CDOrchestra table with parameters ProductUPC and Conductor
        public bool UpdateCDOrchestra(int UPC, string Conductor)
        {
            string strUpdateCDOrchestra = "UPDATE CDOrchestra SET fldConductor='" + Conductor + "'" +
                " WHERE fldUPC=" + UPC + ";";
            OleDbConnection myConnection = new OleDbConnection(strConnection);
            OleDbCommand myCommand = new OleDbCommand(strUpdateCDOrchestra, myConnection);
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
                MessageBox.Show("There was an Update CDOrchestra error: " + ex.Message,
                     "CDOrchestral Update Failed", MessageBoxButtons.OK);
                return false; // returns false if Insert was unsuccessful
            }
            finally
            {
                myConnection.Close();
            }
        }  // end UpdateCDOrchestra










        // ********** DELETE Method ********** 
        // Deletes records all tables that have a matching ProductUPC 

        // Uses strConnection to open a connection with the database
        // Deletes Product with given ID from every table in the database
        // If a Product with the given ID is not in a table, the Delete command does nothing
        // Code written by Christopher Tither and Frank Branigan, CIS 3309 Section 1, April 2017
        // Updated by Frank Friedman June 2019
        public void Delete(int UPC)
        {
            using (OleDbConnection connection = new OleDbConnection(strConnection))
            {
                try
                {
                    connection.Open();

                    using (OleDbCommand command1 = new OleDbCommand("DELETE FROM Product WHERE fldUPC = " + UPC, connection))
                    {
                        OleDbDataReader reader = command1.ExecuteReader();
                    }
                    using (OleDbCommand command2 = new OleDbCommand("DELETE FROM Book WHERE fldUPC = " + UPC, connection))
                    {
                        OleDbDataReader reader = command2.ExecuteReader();
                    }
                    using (OleDbCommand command3 = new OleDbCommand("DELETE FROM BookCIS WHERE fldUPC = " + UPC, connection))
                    {
                        OleDbDataReader reader = command3.ExecuteReader();
                    }
                    using (OleDbCommand command4 = new OleDbCommand("DELETE FROM DVD WHERE fldUPC = " + UPC, connection))
                    {
                        OleDbDataReader reader = command4.ExecuteReader();
                    }
                    using (OleDbCommand command5 = new OleDbCommand("DELETE FROM CDClassical WHERE fldUPC = " + UPC, connection))
                    {
                        OleDbDataReader reader = command5.ExecuteReader();
                    }
                    using (OleDbCommand command6 = new OleDbCommand("DELETE FROM CDChamber WHERE fldUPC = " + UPC, connection))
                    {
                        OleDbDataReader reader = command6.ExecuteReader();
                    }
                    connection.Close();
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show("There was a Delete Database Entry error: " + ex.Message,
                        "Delete Database Entry Failed", MessageBoxButtons.OK);
                    connection.Close();
                }
            }  // end using block
            // FormController.clear(this);
        }  // end Delete



    } // end of Product class
} // end of namespace