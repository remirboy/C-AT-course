﻿using NUnit.Framework;
using address_book_web.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;
using address_book_web.DBModels;

namespace address_book_web.Tests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {

        private static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json"));
        }

        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTest(GroupData group)
        {

            app.NavigationHelper.OpenGroupsPage();
            List<GroupData> oldGroups = app.GroupHelper.GetGroupsList();
            app.GroupHelper.Create(group);

            List<GroupData> newGroups = app.GroupHelper.GetGroupsList();
            app.NavigationHelper.OpenHomePage();

            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);

            oldGroups.Add(group);
            newGroups.Sort();
            oldGroups.Sort();
            
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void GroupDeleteTest()
        {
            app.NavigationHelper.OpenGroupsPage();

            List<GroupData> oldGroups = app.GroupHelper.GetGroupsList();
            app.GroupHelper.Delete(1);

            List<GroupData> newGroups = app.GroupHelper.GetGroupsList();
            app.NavigationHelper.OpenHomePage();

            Assert.AreEqual(oldGroups.Count - 1, newGroups.Count);
        }

        [Test]
        public void GroupNameUpdateTest()
        {
            GroupData group = new GroupData();
            group.GroupName = "Name2";
           
            app.NavigationHelper.OpenGroupsPage();
            List<GroupData> oldGroups = app.GroupHelper.GetGroupsList();
            app.GroupHelper.UpdateName(group);

            List<GroupData> newGroups = app.GroupHelper.GetGroupsList();
            app.NavigationHelper.OpenHomePage();

            Assert.AreEqual(oldGroups.Count, newGroups.Count);
        }

        [Test]
        public void UiAndDbGroupsEqualsTest()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUI = app.GroupHelper.GetGroupsList();
            DateTime end = DateTime.Now;
            Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupData> fromDB = GroupData.GetAll();
            end = DateTime.Now;
            Console.Out.WriteLine(end.Subtract(start));
            fromDB.Sort();
            fromUI.Sort();
            Assert.AreEqual(fromUI, fromDB);
        }
    }
}