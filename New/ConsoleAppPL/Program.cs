using System;
using Persistence;
using DAL;
using System.Collections.Generic;

namespace ConsoleAppPL
{
    class Program
    {
        static int loginID;
        static List<Item> SearchByName()
        {
            Console.Clear();
            Console.WriteLine("Input search items: ");
            String name = Console.ReadLine();
            ItemDAL itemDAL = new ItemDAL();
            List<Item> items = itemDAL.GetByName(name);
            int Count = items.Count;

            if(items.Count == 0)
            {
                Console.WriteLine("\n{0} results for keyword: \n" + name, Count);
                Console.WriteLine("\nNot found items!");
                Console.WriteLine("Press any keys to back search menu...");
                Console.ReadKey();
                Console.Clear();
                return items;
            }

            Console.Clear();
            Console.WriteLine("\n{0} results for keyword: \n" + name, Count);
            Console.WriteLine("+--------------------------------------------------------------------------------------------------------------------------------+");
            Console.WriteLine("| ID    | Item Name                   \t| Item Description                                              \t| Item Price \t |");
            Console.WriteLine("+--------------------------------------------------------------------------------------------------------------------------------+");
            
            foreach(Item i in items)
            {
                Console.WriteLine(i.ToString());
                Console.WriteLine("+--------------------------------------------------------------------------------------------------------------------------------+");
            }

            Console.WriteLine("\nPress any keys to back search menu...");
            Console.ReadKey();
            Console.Clear();
            return items;
        }
 
        static Item SearchItemByID()
        {
            Console.Clear();
            Console.WriteLine("Enter ID to search: ");
            int itemID = InputUtil.readINT(); 
            ItemDAL itemDAL = new ItemDAL();
            Item item = itemDAL.GetItemById(itemID);

            if(item == null)
            {
                Console.WriteLine("\n0 result\n");
                Console.WriteLine("Not found item!");
                Console.WriteLine("Press any keys to continue...");
                Console.ReadKey();
                Console.Clear();
                return null;
            }

            else
            {
                Console.WriteLine("\n1 result\n");
                Console.WriteLine("+--------------------------------------------------------------------------------------------------------------------------------+");
                Console.WriteLine("| ID    | Item Name                   \t| Item Description                                              \t| Item Price \t |");
                Console.WriteLine("+--------------------------------------------------------------------------------------------------------------------------------+");
                Console.WriteLine(item.ToString());
                Console.WriteLine("+--------------------------------------------------------------------------------------------------------------------------------+");
            }

            Console.WriteLine("\nPress any keys to continue...");
            Console.ReadKey();
            Console.Clear();
            return item;
        }

        static List<Item> SearchByPriceRange()
        {
            Console.Clear();
            Console.WriteLine("Min: ");
            int fromPrice = InputUtil.readINT();
            Console.WriteLine("Max: ");
            int toPrice = InputUtil.readINT();
            ItemDAL itemDAL = new ItemDAL();
            List<Item> items = itemDAL.GetByPriceRange(fromPrice,toPrice);
            int Count = items.Count;

            if(items.Count == 0)
            {
                Console.WriteLine("\n{0} results", Count);
                Console.WriteLine("\nNot found items!");
                Console.WriteLine("Press any keys to back search menu...");
                Console.ReadKey();
                Console.Clear();
                return items;
            }
                Console.WriteLine("\n{0} results\n", Count);
                Console.WriteLine("+--------------------------------------------------------------------------------------------------------------------------------+");
                Console.WriteLine("| ID    | Item Name                   \t| Item Description                                              \t| Item Price \t |");
                Console.WriteLine("+--------------------------------------------------------------------------------------------------------------------------------+");
            
            foreach(Item i in items)
            {
                Console.WriteLine(i.ToString());
                Console.WriteLine("+--------------------------------------------------------------------------------------------------------------------------------+");
            }

            Console.WriteLine("\nPress any keys to back search menu...");
            Console.ReadKey();
            Console.Clear();
            return items;
        }

        static void SearchMenu()
        {
            int choose = 0;

            do
            {
                string logo = @"===================================================================
   ____ _       _   _     _               ____  _                 
  / ___| | ___ | |_| |__ (_)_ __   __ _  / ___|| |__   ___  _ __  
 | |   | |/ _ \| __| '_ \| | '_ \ / _` | \___ \| '_ \ / _ \| '_ \ 
 | |___| | (_) | |_| | | | | | | | (_| |  ___) | | | | (_) | |_) |
  \____|_|\___/ \__|_| |_|_|_| |_|\__, | |____/|_| |_|\___/| .__/ 
                                  |___/                    |_|";
                Console.WriteLine(logo);
                Console.WriteLine("\t");
                string line = "===================================================================";
                Console.WriteLine(line);
                Console.WriteLine("\t");
                Console.WriteLine("+-----------------------------+");
                Console.WriteLine("|        SEARCH ITEMS         |");
                Console.WriteLine("+-----------------------------+");
                Console.WriteLine("| 1. Search by Name           |");
                Console.WriteLine("| 2. Search by Price          |");
                Console.WriteLine("| 3. Search by ID             |");
                Console.WriteLine("| 4. Exit Search              |");
                Console.WriteLine("+-----------------------------+");
                Console.WriteLine("Your choice: ");
                choose = InputUtil.readINT();

                switch(choose)
                {
                    case 1:
                        SearchByName();
                        break;
                    case 2:
                        SearchByPriceRange();
                        break;
                    case 3:
                        SearchItemByID();
                        break;
                    case 4:
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("\nIncorrect choice! Please re-enter choice from 1-4!");
                        Console.WriteLine("Press any keys to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }while(choose != 4); 
        }

        static bool checkItemExistInvoice(InvoiceDetail invoiceDetail, List<InvoiceDetail> invoiceDetails)
        {
            foreach(InvoiceDetail iv in invoiceDetails)
            {
                if(iv.itemID == invoiceDetail.itemID)
                {
                    return true;
                }
            }
            return false;
        }

        static void CreateInvoices()
        {
            start:
            Console.Clear();
            Console.WriteLine("+------------------------------------+");
            Console.WriteLine("|     ENTER CUSTOMER INFORMATION     |");
            Console.WriteLine("+------------------------------------+");
            Customer customer = new Customer();
            Console.WriteLine("\nCustomer name: ");
            customer.CustomerName = Console.ReadLine();
            Console.WriteLine("Address: ");
            customer.CustomerAddress = Console.ReadLine();
            Console.WriteLine("Telephone: ");
            customer.Telephone = Console.ReadLine();
            Console.WriteLine("\nAdd successful customer information!");
            Console.WriteLine("Press any keys to continue...");
            Console.ReadKey();
            List<InvoiceDetail> invoiceDetails = new List<InvoiceDetail>();
            int choose = 0;
            cInvoice:
            InvoiceDetail invoiceDetail = new InvoiceDetail();
            Item item = SearchItemByID();

            if(item != null)
            {
                invoiceDetail.itemID = item.ItemId;
                invoiceDetail.itemPrice = item.ItemPrice;
                
                if(checkItemExistInvoice(invoiceDetail,invoiceDetails))
                {
                    Console.WriteLine("\nItem included in the invoice!");
                    Console.WriteLine("Press any keys to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else{
                    ItemDAL itemmDAL = new ItemDAL();
                    List<ItemDetails> itemDetails = itemmDAL.GetItemDetailByItemID(invoiceDetail.itemID);
                    Console.WriteLine("+-------------------------------------------------------+");
                    Console.WriteLine("|            LIST ITEMS COLOR AND ITEMS SIZE            |");
                    int i = 0;
                    Console.WriteLine("+-------------------------------------------------------+");
                    Console.WriteLine("|Ordinal   |Color      \t   |Size   \t|Quantity  \t|");
                    Console.WriteLine("+-------------------------------------------------------+");

                    foreach(ItemDetails itemDetails1 in itemDetails)
                    {
                        Console.WriteLine(String.Format("|{0,-10}|{1,-15}|{2,-12}|{3,-15}|",i,itemDetails1.ColorName,itemDetails1.SizeName,itemDetails1.Quantity));
                        Console.WriteLine("+-------------------------------------------------------+");
                        i++;
                    }

                    Console.WriteLine("\nEnter ordinal numbers: ");
                    int c;
                    chooseDetail:
                    c = InputUtil.readINT();
                    if(c >= itemDetails.Count || c < 0)
                    {
                        Console.WriteLine("\nIncorrect choice!");
                        goto chooseDetail;
                    }
                    invoiceDetail.itemDetail = itemDetails[c];
                    invoiceDetails.Add(invoiceDetail);

                    Console.WriteLine("\nEnter item quantity: ");
                    inputQuantity:
                    invoiceDetail.quantity = InputUtil.readINT();
                    if (invoiceDetail.quantity > itemDetails[c].Quantity || invoiceDetail.quantity < 0)
                    {
                        Console.WriteLine("\nInvalid item number!");
                        goto inputQuantity;
                    }
                    Console.WriteLine("\nAdd successfully! Items already in the invoice!");
                }
            }else{
                goto cInvoice;
            }
            Console.WriteLine("Do you want add another items to invoice? (0-No/1-Yes)");
            choose = InputUtil.readINT();

            if(choose==1)
            {
                goto cInvoice;
            }

            Console.Clear();
            Console.WriteLine("                       +---------------------------+");
            Console.WriteLine("                       |     CUSTOMER INFO ADD     |");
            Console.WriteLine("                       +---------------------------+");
            Console.WriteLine("\t");
            Console.WriteLine(customer.ToString());
            Console.WriteLine("\t");
            Console.WriteLine("+--------------------------------------------------+");
            Console.WriteLine("|                  ITEMS INFO ADD                  |");
            Console.WriteLine("+--------------------------------------------------+");
            Console.WriteLine("|ID     |Color\t        |Size  \t|Price \t           |");
            Console.WriteLine("+--------------------------------------------------+");

            foreach(InvoiceDetail i in invoiceDetails)
            {
                Console.WriteLine(i.ToString());
                Console.WriteLine("+--------------------------------------------------+");
            }
            Console.WriteLine("\nDo you want to create invoice? (0-No/1-Yes)");
            choose = InputUtil.readINT();

            if(choose==0)
            {
                goto start;
            }

            CustomerDAL customerDAL = new CustomerDAL();
            long cusID = customerDAL.InsertCustomer(customer);
            InvoiceDAL invoiceDAL = new InvoiceDAL();
            Invoice invoice = new Invoice();
            invoice.CashierID = loginID;
            invoice.CustomerID = cusID;
            invoice.invoiceDate = DateTime.Now;
            long invoiceID = invoiceDAL.InsertInvoice(invoice);
            invoiceDetailDAL invoiceDetailDAL = new invoiceDetailDAL();

            foreach(InvoiceDetail invoiceDetail1 in invoiceDetails)
            {
                invoiceDetail1.invoicesNO = invoiceID;
                invoiceDetailDAL.InsertInvoiceDetail(invoiceDetail1);
                Console.WriteLine(String.Format("{0:0,0 VND}",invoiceDetail.itemPrice));
            }
            Console.Clear();
            string logo = @"                     
          ____ _       _   _     _               ____  _                 
         / ___| | ___ | |_| |__ (_)_ __   __ _  / ___|| |__   ___  _ __  
        | |   | |/ _ \| __| '_ \| | '_ \ / _` | \___ \| '_ \ / _ \| '_ \ 
        | |___| | (_) | |_| | | | | | | | (_| |  ___) | | | | (_) | |_) |
         \____|_|\___/ \__|_| |_|_|_| |_|\__, | |____/|_| |_|\___/| .__/ 
                                         |___/                    |_|";
            Console.WriteLine(logo);
            Console.WriteLine("\n               DC: 18 Tam Trinh, Minh Khai, Hai Ba Trung, Ha Noi");
            Console.WriteLine("                              DT: 012.3400.0056");
            Console.WriteLine("\n                                BILL OF SALE\n");
            Console.WriteLine(invoice.ToString());
            Console.WriteLine("Seller: ClothingShop                                   Cashier Name: dtdat\n");
            Console.WriteLine("-------------------------------------------------------------------------------------------\n");
            Console.WriteLine(customer.ToString()); 
            Console.WriteLine("\t");
            Console.WriteLine("-------------------------------------------------------------------------------------------\n");
            Console.WriteLine("+------------------------------------------------------------------------------------------+");
            Console.WriteLine(String.Format("|{0,-5}|{1,-10}|{2,-30}|{3,10}|{4,10}|{5,20}|","No","ItemID","ItemName","ItemPrice","Quantity","Amount"));
            Console.WriteLine("+------------------------------------------------------------------------------------------+");
            decimal total = 0;
            int stt = 0;
            ItemDAL itemDAL = new ItemDAL();
            
            foreach(InvoiceDetail invoiceDetail1 in invoiceDetails)
            {
                Console.WriteLine(String.Format("|{0,-5}|{1,-10}|{2,-30}|{3,10}|{4,10}|{5,20}|",stt++,invoiceDetail1.itemID
                ,itemDAL.GetItemById(invoiceDetail1.itemID).ItemName,String.Format("{0:0,0 VND}",invoiceDetail.itemPrice),invoiceDetail1.quantity,invoiceDetail1.itemPrice*invoiceDetail1.quantity));
                total += invoiceDetail.itemPrice*invoiceDetail1.quantity;
                Console.WriteLine("+------------------------------------------------------------------------------------------+");
            }
            
            Console.WriteLine("                                                                Total amount: "+String.Format("{0:0,0 VND}", total));
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("\n                              THANK YOU AND SEE YOU AGAIN\n");
            Console.WriteLine("                     Hotline: 1900333999     Website: clothingshop.com");
            Console.WriteLine("\n\nPress any keys to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        static void menu()
        {
            int choose = 0;
            do
            {
                string logo = @"===================================================================
   ____ _       _   _     _               ____  _                 
  / ___| | ___ | |_| |__ (_)_ __   __ _  / ___|| |__   ___  _ __  
 | |   | |/ _ \| __| '_ \| | '_ \ / _` | \___ \| '_ \ / _ \| '_ \ 
 | |___| | (_) | |_| | | | | | | | (_| |  ___) | | | | (_) | |_) |
  \____|_|\___/ \__|_| |_|_|_| |_|\__, | |____/|_| |_|\___/| .__/ 
                                  |___/                    |_|";
                Console.WriteLine(logo);
                Console.WriteLine("\t");
                string line = "===================================================================";
                Console.WriteLine(line);
                Console.WriteLine("\t");
                Console.WriteLine("+-----------------------------+");
                Console.WriteLine("|          MENU SHOP          |");
                Console.WriteLine("+-----------------------------+");
                Console.WriteLine("| 1. Search items             |");
                Console.WriteLine("| 2. Create invoice           |");
                Console.WriteLine("| 3. Exit shop                |");
                Console.WriteLine("+-----------------------------+");
                Console.WriteLine("Your choice: ");
                choose = InputUtil.readINT();

                switch(choose)
                {
                    case 1:
                        Console.Clear();
                        SearchMenu();
                        break;
                    case 2:
                        CreateInvoices();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("\t");
                        Console.WriteLine("Incorrect choice! Please re-enter choice from 1-3!");
                        Console.WriteLine("Press any keys to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }while(choose != 3);
        }

        static void Main(string[] args)
        {
            startLogin:
            string logo = @"===================================================================
   ____ _       _   _     _               ____  _                 
  / ___| | ___ | |_| |__ (_)_ __   __ _  / ___|| |__   ___  _ __  
 | |   | |/ _ \| __| '_ \| | '_ \ / _` | \___ \| '_ \ / _ \| '_ \ 
 | |___| | (_) | |_| | | | | | | | (_| |  ___) | | | | (_) | |_) |
  \____|_|\___/ \__|_| |_|_|_| |_|\__, | |____/|_| |_|\___/| .__/ 
                                  |___/                    |_|";
            Console.WriteLine(logo);
            Console.WriteLine("\t");
            string line = "===================================================================";
            Console.WriteLine(line);
            Console.WriteLine("\t");
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("|      LOGIN TO THE SHOP      |");
            Console.WriteLine("+-----------------------------+");
            Console.WriteLine("\t");           
            Console.Write("UserName: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string pass = GetPassword();
            Console.WriteLine();
            Cashier cashier = new Cashier(){UserName=userName, Password=pass};
            int login = (new CashierDAL()).Login(cashier);
            
            if(login <= 0)
            {
                Console.Clear();
                Console.WriteLine("Your UserName or Password entered is incorrect?");
                Console.WriteLine("Press any keys to log in again...");
                Console.ReadKey();
                Console.Clear();
                goto startLogin;
            }else{
                loginID = login; 
                Console.Clear();
                menu();
            }
        }

        static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }

                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }

            } while (key != ConsoleKey.Enter);
            return pass;
        }

        static string NumberToWords(int number)
        {
            if (number == 0)
            return "zero";

            if (number < 0)
            return "minus" + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += " and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                words += unitsMap[number];

                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }
            return words;
        }
    }
}