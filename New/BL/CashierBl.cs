using System;
using Persistence;
using DAL;

namespace BL
{
    public class CashierBl
    {
<<<<<<< HEAD
        private CashierDAL dal = new CashierDAL();
=======
        private CashierDal dal = new CashierDal();
>>>>>>> a323dde2b37753a0b80afd4caebd54e46a9c496f
        public int Login(Cashier cashier)
        {
            return dal.Login(cashier);
        }
    }
}