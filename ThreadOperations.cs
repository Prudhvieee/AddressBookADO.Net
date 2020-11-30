using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace AddressBookADO.Net
{
    public class ThreadOperations
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBook_Service;Integrated Security=True";
        SqlConnection sqlconnection = new SqlConnection(connectionString);
        public bool AddContact(AddressBookModel model)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                AddressBookModel addressBookModel = new AddressBookModel();
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        public void AddContactListToDBWithoutThread(List<AddressBookModel> contactList)
        {

            contactList.ForEach(contact =>
            {
                Console.WriteLine("Contact being added: " + contact.firstName);
                this.AddContact(contact);
                Console.WriteLine("Contact added: " + contact.firstName);
            });
        }
        public void AddContactListToDBWithThread(List<AddressBookModel> contactList)
        {
            contactList.ForEach(contact =>
            {
                Thread thread = new Thread(() =>
                {
                    Console.WriteLine("Contact Being added" + contact.firstName);
                    this.AddContact(contact);
                    Console.WriteLine("Contact added: " + contact.firstName);
                });
                thread.Start();
                thread.Join();
            });
        }
    }
}
