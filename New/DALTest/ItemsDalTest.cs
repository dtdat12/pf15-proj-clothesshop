using System;
using Xunit;
using DAL;
using Persistence;
using System.Collections.Generic;

namespace DALTest
{
    public class ItemDalTest
    {
        private ItemDAL dal = new ItemDAL();
        
        [Theory]
        [InlineData("shorts", 1)]
        [InlineData("Three-Stripes shorts", 2)]
        [InlineData("pants", 3)]
        [InlineData("Yoga pants", 4)]
        [InlineData("jacket", 7)]
        [InlineData("Marathon jacket", 8)]
        [InlineData("hoodie", 9)]
        [InlineData("Hulk hoodie", 10)]
        [InlineData(" ", 0)]
        [InlineData("11111", 0)]
        [InlineData("Lo go", 0)]
        [InlineData("b ox 11111", 0)]
        
        public void GetNameTest(string _name, int expected)
        {
            List<Item> lstItem = new List<Item>();
            lstItem = dal.GetByName(_name);
            Assert.True(lstItem != null);
            // Assert.True(lstItem.Count > 0);
            foreach(Item i in lstItem)
            {
                Assert.True(i.ItemName.ToLower().Contains(_name.ToLower()));
            }
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        [InlineData(5, 5)]
        [InlineData(6, 6)]

        // [InlineData(-1, 0)]
        // [InlineData(11, 0)]
        // [InlineData(" ", 0)]
        // [InlineData("jacket", 0)]
        // [InlineData("11-1", 0)]
    
        public void GetIDTest(int _id, int expected)
        {
            Item result = dal.GetItemById(_id);
            Assert.True(result != null);
            Assert.True(result.ItemId == _id);
        }

        [Theory]
        [InlineData(50000, 450000, true)]
        [InlineData(-50000, 900000, true)]
        [InlineData(100000, 100000, true)]
        
        [InlineData(800000, 100000, true)]
        [InlineData(550000, -50000, true)]
        [InlineData(-300000, -900000, true)]

        public void GetPriceTest(int _min, int _max, bool found)
        {
            List<Item> lstItem = new List<Item>();
            lstItem = dal.GetByPriceRange(_min, _max);

        if (found)
        {
            Assert.True(lstItem != null);
            // Assert.True(lstItem.Count > 0);
            foreach(Item it in lstItem)
            {
                Assert.True(it.ItemPrice > _min);
                Assert.True(it.ItemPrice < _max);
            }
        }
            else
            {
                Assert.True(lstItem == null);
            }
        }
    }
}