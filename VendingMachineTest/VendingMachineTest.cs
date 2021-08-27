using NUnit.Framework;
using VendingMachineKata;

namespace VendingMachineTest
{
    public class Tests
    {
        //The vending machine will accept valid coins (5p, 10p, 20p, 50p, £1 and £2)  
        //When a valid coin is inserted the amount of the coin will be added to the current amount and the display will be updated
        [Test]
        public void InserCoin_AllowedCoin_Test()
        {
            VendingMachine vendingMachine = new VendingMachine();

            vendingMachine.InsertCoin(0.05m);

            var balance = vendingMachine.Balance;
            var displayBalance = vendingMachine.DisplayBalance;

            Assert.AreEqual(0.05m, balance);
            Assert.AreEqual(balance.ToString(), displayBalance);
        }
        [Test]
        public void InserCoin_AllowedCoins_Test()
        {
            VendingMachine vendingMachine = new VendingMachine();

            vendingMachine.InsertCoin(0.05m);
            vendingMachine.InsertCoin(0.10m);
            vendingMachine.InsertCoin(0.20m);
            vendingMachine.InsertCoin(0.50m);
            vendingMachine.InsertCoin(1.00m);
            vendingMachine.InsertCoin(2.00m);

            var balance = vendingMachine.Balance;
            var displayBalance = vendingMachine.DisplayBalance;

            Assert.AreEqual(3.85m, balance);
            Assert.AreEqual(balance.ToString(), displayBalance);
        }

        //The vending machine will reject invalid ones (1p and 2p). Rejected coins are placed in the coin return.
        [Test]
        public void InsertCoin_NotAllowed_Test()
        {
            VendingMachine vendingMachine = new VendingMachine();

            vendingMachine.InsertCoin(0.01m);
            vendingMachine.InsertCoin(0.02m);

            var balance = vendingMachine.Balance;
            var returned = vendingMachine.ReturnedCoins;

            Assert.AreEqual(0,balance);
            Assert.AreEqual(0.02m, returned);
            //Return = 0.02, it is assumed that coins are taken out of return when they are placed there
        }


        //When the respective button is pressed and enough money has been inserted, the product is dispensed 
        //and the machine displays THANK YOU. If the display is checked again, it will display INSERT COIN and 
        //the current amount will be set to £0.00. 
        [Test]
        public void SelectProduct_Success_Test()
        {
            VendingMachine vendingMachine = new VendingMachine();
            Product product = new Product("cola", 1.00m, 3);

            vendingMachine.InsertCoin(1.00m);
            vendingMachine.SelectProduct(product);

            var balance = vendingMachine.Balance;
            var returned = vendingMachine.ReturnedCoins;
            var heldMoney = vendingMachine.MachineCoins;
            var display = vendingMachine.Display;
            var displayBalance = vendingMachine.DisplayBalance;
            var stock = product.ProductStock;

            Assert.AreEqual(0,balance);
            Assert.AreEqual(0, returned);
            Assert.AreEqual(2, stock);
            Assert.AreEqual(2.55m, heldMoney);
            Assert.AreEqual("THANK YOU", display);
            Assert.AreEqual(0, displayBalance);
        }

        //If there is not enough money inserted then the machine displays PRICE 
        //and the price of the item and subsequent checks of the display will display 
        //either INSERT COIN or the current amount as appropriate.
        [Test]
        public void SelectProduct_MoreCoins_Test()
        {
            VendingMachine vendingMachine = new VendingMachine();
            Product product = new Product("cola", 1.00m, 3);

            vendingMachine.InsertCoin(0.50m);
            vendingMachine.SelectProduct(product);

            var balance = vendingMachine.Balance;
            var returned = vendingMachine.ReturnedCoins;
            var heldMoney = vendingMachine.MachineCoins;
            var display = vendingMachine.Display;
            var stock = product.ProductStock;

            Assert.AreEqual(0.50, balance);
            Assert.AreEqual(0, returned);
            Assert.AreEqual(3, stock);
            Assert.AreEqual(1.55m, heldMoney);
            Assert.AreEqual("PRICE :1.00", display);
        }

        //When the item selected by the customer is out of stock, the machine displays SOLD OUT.
        //If the display is checked again, it will display the amount of money remaining in the machine or INSERT COIN if there is no money in the machine.
        [Test]
        public void SelectProduct_OutOfStock_Test()
        {
            VendingMachine vendingMachine = new VendingMachine();
            Product product = new Product("cola", 1.00m, 0);

            vendingMachine.InsertCoin(1.00m);
            vendingMachine.SelectProduct(product);

            var balance = vendingMachine.Balance;
            var returned = vendingMachine.ReturnedCoins;
            var heldMoney = vendingMachine.MachineCoins;
            var display = vendingMachine.Display;
            var displayBalance = vendingMachine.DisplayBalance;
            var stock = product.ProductStock;

            Assert.AreEqual(1.00m, balance);
            Assert.AreEqual(0, returned);
            Assert.AreEqual(0, stock);
            Assert.AreEqual(1.55m, heldMoney);
            Assert.AreEqual("OUT OF STOCK", display);
            Assert.AreEqual(balance.ToString(), displayBalance);
        }

        //When a product is selected that costs less than the amount of money in the machine, then the remaining amount is placed in the coin return.
        [Test]
        public void SelectProduct_GiveChange_Test()
        {
            VendingMachine vendingMachine = new VendingMachine();
            Product product = new Product("cola", 1.00m, 3);

            vendingMachine.InsertCoin(2.00m);
            vendingMachine.SelectProduct(product);

            var balance = vendingMachine.Balance;
            var returned = vendingMachine.ReturnedCoins;
            var heldMoney = vendingMachine.MachineCoins;
            var display = vendingMachine.Display;
            var displayBalance = vendingMachine.DisplayBalance;
            var stock = product.ProductStock;

            Assert.AreEqual(0, balance);
            Assert.AreEqual(1.00m, returned);
            Assert.AreEqual(2, stock);
            Assert.AreEqual(2.55m, heldMoney);
            Assert.AreEqual("THANK YOU", display);
            Assert.AreEqual(balance.ToString(), displayBalance);
        }

        //When the return coins button is pressed, the money the customer has placed in the machine is returned and the display shows INSERT COIN.
        [Test]
        public void ReturnCoins_Test()
        {
            VendingMachine vendingMachine = new VendingMachine();

            vendingMachine.InsertCoin(0.50m);

            vendingMachine.ReturnCoins();

            var balance = vendingMachine.Balance;
            var returned = vendingMachine.ReturnedCoins;
            var display = vendingMachine.Display;
            var displayBalance = vendingMachine.DisplayBalance;

            Assert.AreEqual(0, balance);
            Assert.AreEqual(0.50m, returned);
            Assert.AreEqual("INSERT COIN", display);
            Assert.AreEqual("0",displayBalance);
        }
    }
}