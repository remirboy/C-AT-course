﻿using address_book_web.Models;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace address_book_web.DBModels 
{
    public class AddressBookDB : LinqToDB.Data.DataConnection
    {
        public AddressBookDB() : base("AddressBook") { }
        
        public ITable<GroupData> Groups { get { return GetTable<GroupData>(); } }

        public ITable<Contact> Contacts { get { return GetTable<Contact>(); } }

        public ITable<ContactsInGroup> ContactsInGroup { get { return GetTable<ContactsInGroup>(); } }
    }
}
