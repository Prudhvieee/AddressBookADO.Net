using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookADO.Net
{
    public class AddressRepo
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBook_Service;Integrated Security=True";
        SqlConnection sqlconnection = new SqlConnection(connectionString);
        public void GetAllEmployee()
        {
            try
            {
                AddressBookModel addressBookModel = new AddressBookModel();
                using (this.sqlconnection)
                {
                    string query = @"select * from AddressBook_Table";
                    SqlCommand command = new SqlCommand(query, this.sqlconnection);
                    this.sqlconnection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            addressBookModel.firstName = dataReader.GetString(0);
                            addressBookModel.secondName = dataReader.GetString(1);
                            addressBookModel.address = dataReader.GetString(2);
                            addressBookModel.city = dataReader.GetString(3);
                            addressBookModel.state = dataReader.GetString(4);
                            addressBookModel.zip = dataReader.GetInt64(5);
                            addressBookModel.phoneNumber = dataReader.GetInt64(6);
                            addressBookModel.emailid = dataReader.GetString(7);
                            addressBookModel.contactType = dataReader.GetString(8);
                            addressBookModel.addressBookName = dataReader.GetString(9);
                            Console.WriteLine("\n");
                            Console.WriteLine(addressBookModel.firstName + " " + addressBookModel.secondName + " " + addressBookModel.address + " " + addressBookModel.city + " " +
                                addressBookModel.state + " " + addressBookModel.zip + " " + addressBookModel.phoneNumber + " " + addressBookModel.emailid
                                + " " + addressBookModel.contactType + " " + addressBookModel.addressBookName);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    dataReader.Close();
                    this.sqlconnection.Close();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }
        public bool UpdateTables()
        {
            string query = @"update AddressBook_Table set state = 'india' where firstname = 'tom'";
            using (this.sqlconnection)
            {
                try
                {
                    this.sqlconnection.Open();
                    SqlCommand command = new SqlCommand(query, this.sqlconnection);
                    int updatedRows = command.ExecuteNonQuery();
                    if (updatedRows != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
                finally
                {
                    this.sqlconnection.Close();
                }
            }
        }
        public List<string> GetDataInParticularRange()
        {
            AddressBookModel addressBookModel = new AddressBookModel();
            List<string> data = new List<string>();
            try
            {
                using (this.sqlconnection)
                {
                    string query = @"SELECT * FROM AddressBook_Table where addedDate between CAST('2018-02-01' AS DATE) AND SYSDATETIME()";
                    SqlCommand command = new SqlCommand(query, sqlconnection);
                    this.sqlconnection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            addressBookModel.firstName = dataReader.GetString(0);
                            addressBookModel.secondName = dataReader.GetString(1);
                            addressBookModel.address = dataReader.GetString(2);
                            addressBookModel.city = dataReader.GetString(3);
                            addressBookModel.state = dataReader.GetString(4);
                            addressBookModel.zip = dataReader.GetInt64(5);
                            addressBookModel.phoneNumber = dataReader.GetInt64(6);
                            addressBookModel.emailid = dataReader.GetString(7);
                            addressBookModel.contactType = dataReader.GetString(8);
                            addressBookModel.addressBookName = dataReader.GetString(9);
                            Console.WriteLine("\n");
                            data.Add(addressBookModel.firstName);
                            Console.WriteLine(addressBookModel.firstName);
                        }
                        dataReader.Close();
                        return data;
                    }
                    else
                    {
                        throw new Exception("No data found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }
        public void CountOfContacts()
        {
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            try
            {
                using (this.sqlconnection)
                {
                    string query = @"select contacttype, COUNT(contacttype) from AddressBook_Table group by contactType";
                    SqlCommand command = new SqlCommand(query, sqlconnection);
                    sqlconnection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Console.Write(dataReader.GetString(0) + "\t" + dataReader.GetInt32(1));
                            Console.WriteLine("\n");
                        }
                        sqlconnection.Close();
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }
        public bool AddContact(AddressBookModel addressBookModel)
        {
            try
            {
                using (this.sqlconnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddContact", this.sqlconnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Nfirstame", addressBookModel.firstName);
                    sqlCommand.Parameters.AddWithValue("@secondName", addressBookModel.secondName);
                    sqlCommand.Parameters.AddWithValue("@address", addressBookModel.address);
                    sqlCommand.Parameters.AddWithValue("@city", addressBookModel.city);
                    sqlCommand.Parameters.AddWithValue("@state", addressBookModel.state);
                    sqlCommand.Parameters.AddWithValue("@zip", addressBookModel.zip);
                    sqlCommand.Parameters.AddWithValue("@", addressBookModel.phoneNumber);
                    sqlCommand.Parameters.AddWithValue("@emailid", addressBookModel.emailid);
                    sqlCommand.Parameters.AddWithValue("@contactType", addressBookModel.contactType);
                    sqlCommand.Parameters.AddWithValue("@addressBookName", addressBookModel.addressBookName);
                    sqlCommand.Parameters.AddWithValue("@addedDate", DateTime.Now);
                    this.sqlconnection.Open();
                    var result = sqlCommand.ExecuteNonQuery();
                    this.sqlconnection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
            return false;
        }
    }
}