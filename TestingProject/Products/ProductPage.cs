using AventStack.ExtentReports;
using courseproject.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace courseproject.Products
{
    public class ProductsPage : BasePage
    {
        #region Locators

        public static By ProductsTitleLBL = By.ClassName("title");
        public static By CartBadgeLBL = By.ClassName("shopping_cart_badge");
        public static By CartIcon = By.ClassName("shopping_cart_link");

        public static By AddBackpackBTN = By.Id("add-to-cart-sauce-labs-backpack");
        public static By RemoveBackpackBTN = By.Id("remove-sauce-labs-backpack");

        public static By AddBikeLightBTN = By.Id("add-to-cart-sauce-labs-bike-light");
        public static By AddBoltTShirtBTN = By.Id("add-to-cart-sauce-labs-bolt-t-shirt");

        public static By SortDropdown = By.ClassName("product_sort_container");

        #endregion

        #region Getters

        public string GetProductsTitle()
        {
            return Driver.FindElement(ProductsTitleLBL).Text;
        }

        public string GetCartCount()
        {
            Thread.Sleep(500); 

            var badges = Driver.FindElements(CartBadgeLBL);
            if (badges.Count == 0)
            {
                return "0";
            }

            return badges[0].Text;
        }

        public bool IsCartEmpty()
        {
            return Driver.FindElements(CartBadgeLBL).Count == 0;
        }

        public string GetSelectedSortOption()
        {
            SelectElement select =
                new SelectElement(Driver.FindElement(SortDropdown));
            return select.SelectedOption.Text;
        }

        #endregion

        #region Actions

        
        public void AddBackpackToCart()
        {
            Driver.FindElement(AddBackpackBTN).Click();
            TakeScreenShot(Status.Pass, "Backpack added");
        }

        public void EnsureBackpackAdded()
        {
            
            if (Driver.FindElements(AddBackpackBTN).Count > 0)

            {
                Driver.FindElement(AddBackpackBTN).Click();
                Thread.Sleep(1000);
                TakeScreenShot(Status.Pass, "Backpack added safely");
                return;
            }

            
            if (Driver.FindElements(RemoveBackpackBTN).Count > 0)
            {
                TakeScreenShot(Status.Info, "Backpack already present in cart");
                return;
            }

            throw new NoSuchElementException("Backpack Add/Remove button not found");
        }


        public void AddBikeLightToCart()
        {
            Driver.FindElement(AddBikeLightBTN).Click();
            TakeScreenShot(Status.Pass, "Bike light added");
        }

        public void AddBoltTShirtToCart()
        {
            Driver.FindElement(AddBoltTShirtBTN).Click();
            TakeScreenShot(Status.Pass, "Bolt T-Shirt added");
        }

        public void AddMultipleProducts()
        {
            AddBackpackToCart();
            Thread.Sleep(1000);
            AddBikeLightToCart();
            Thread.Sleep(1000);
            AddBoltTShirtToCart();
            Thread.Sleep(1000);
        }

        public void RemoveBackpack()
        {
            Driver.FindElement(RemoveBackpackBTN).Click();
            TakeScreenShot(Status.Pass, "Backpack removed");
            Thread.Sleep(500);
        }

        public void OpenCart()
        {
            Driver.FindElement(CartIcon).Click();
            TakeScreenShot(Status.Pass, "Cart opened");
        }

        public void SortProducts(int index)
        {
            SelectElement select =
                new SelectElement(Driver.FindElement(SortDropdown));
            select.SelectByIndex(index);
            TakeScreenShot(Status.Pass, "Products sorted");
        }

        #endregion
    }
}
