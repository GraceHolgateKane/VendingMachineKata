using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        public decimal Balance { get; set; }
        public decimal ReturnedCoins { get; set; }
        public decimal MachineCoins { get; set; }
        public string Display { get; set; }
        public string DisplayBalance { get; set; }
        public VendingMachine()
        {
            Balance = 0;
            ReturnedCoins = 0;
            MachineCoins = 1.55m;
            Display = "INSERT COIN";
            DisplayBalance = Balance.ToString();
        }
        public void InsertCoin(decimal coin)
        {
            switch (coin)
            {
                case (0.05m):
                    Balance += 0.05m;
                    break;
                case (0.10m):
                    Balance += 0.10m;
                    break;
                case (0.20m):
                    Balance += 0.20m;
                    break;
                case (0.50m):
                    Balance += 0.50m;
                    break;
                case (1.00m):
                    Balance += 1.00m;
                    break;
                case (2.00m):
                    Balance += 2.00m;
                    break;

                default:
                    ReturnedCoins = coin;
                    break;
            }
            UpdateBalanceDisplay(Balance);
        }
        private void UpdateBalanceDisplay(decimal balance)
        {
            Display = balance == 0 ? "INSERT COIN" : "CURRENT BALANCE";
            DisplayBalance = balance.ToString();
        }

        public void SelectProduct(Product product)
        {
            if(Balance == 0)
            {
                Display = "INSERT COIN";
            }
            else if (Balance >= product.ProductPrice)
            {
                DispenceProduct(product);
            }
            else if (Balance < product.ProductPrice)
            {
                Display = "PRICE :" + product.ProductPrice.ToString();
            }
        }

        public void ReturnCoins()
        {
            GiveChange(Balance);
            Display = "INSERT COIN";
        }

        private void DispenceProduct(Product product)
        {
            if(product.ProductStock >0)
            {
                product.ProductStock--;
                Balance -= product.ProductPrice;
                MachineCoins += product.ProductPrice;
                Display = "THANK YOU";
                GiveChange(Balance);
            }
            else
            {
                Display = "OUT OF STOCK";
                if (Balance > 0)
                {
                    DisplayBalance = Balance.ToString();
                }
                else
                {
                    Display = "INSERT COIN";
                }
            }
        }

        private void GiveChange(decimal balance)
        {
            ReturnedCoins = balance;
            Balance = 0;
            DisplayBalance = Balance.ToString();
        }
    }
}
