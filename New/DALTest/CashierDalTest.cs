using System;
using Xunit;
using DAL;
using Persistence;

namespace DALTest
{
    public class CashierDalTest
    {
        private CashierDal dal = new CashierDal();
       
        [Fact]
        public void LoginTest1()
        {
            Cashier cashier = new Cashier(){UserName="Clothes", Password="ShopClothes"};
            int expected = 1;
            int result = dal.Login(cashier);
            Assert.True(expected == result);
        }

        [Theory]
        [InlineData("Clothes", "ShopClothes", 1)]   
        [InlineData("Clothesss", "ShopClothesss", 0)]  
        [InlineData("Clothesss", "ShopClothes", 0)]
        [InlineData("Clothes", "ShopClothesss", 0)]
        [InlineData(" ", "ShopClothes", 0)]
        [InlineData("Clothes", " ", 0)]
        [InlineData(" ", " ", 0)]
        
        public void LoginTest2(string userName, string pass, int expected)
        {
            Cashier cashier = new Cashier(){UserName=userName, Password=pass};
            int result = dal.Login(cashier);
            Assert.True(expected == result);
        }
    }
}