using System;
using System.Collections.Generic;
using Persistence;
using DAL;

namespace BL
{
    public class InvoiceBL
    {
        private InvoiceDAL idl = new InvoiceDAL();
        public long InsertInvoice(Invoice invoice)
        {
            return idl.InsertInvoice(invoice);
        }
    }
}