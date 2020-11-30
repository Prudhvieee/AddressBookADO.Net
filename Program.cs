using System;
using System.Collections.Generic;

namespace AddressBookADO.Net
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to address book DB");
            AddressRepo addressRepo = new AddressRepo();
            AddressBookModel addressBookModel = new AddressBookModel();
            addressBookModel.firstName = "tony";
            addressBookModel.secondName = "stark";
            addressBookModel.address = "mount road";
            addressBookModel.city = "Vegas";
            addressBookModel.state = "CA";
            addressBookModel.zip = 25410;
            addressBookModel.phoneNumber =789543210;
            addressBookModel.emailid = "tony@gmail.com";
            addressBookModel.contactType = "Professional";
            addressBookModel.addressBookName = "Office";
            addressRepo.AddContact(addressBookModel);
            ThreadOperations th = new ThreadOperations();
            List<AddressBookModel> listContacts = new List<AddressBookModel>();
            th.AddContactListToDBWithoutThread(listContacts);
            th.AddContactListToDBWithThread(listContacts);
        }
    }
}
