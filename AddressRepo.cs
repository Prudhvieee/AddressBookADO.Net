﻿using System;
using System.Collections.Generic;
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
    }
}