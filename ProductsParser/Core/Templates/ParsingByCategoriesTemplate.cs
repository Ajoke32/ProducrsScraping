using System.Globalization;
using OpenQA.Selenium;
using ProductsParser.Abstraction.Templates;
using ProductsParser.Global;
using ProductsParser.Models;

namespace ProductsParser.Core.Templates;

public class ParsingByCategoriesTemplate:IParsingTemplate
{
    private readonly IWebDriver _driver;
    
    private readonly List<Product> _products;

    private readonly List<string> _categoryLinks;

    private int _pagesCount;
    public ParsingByCategoriesTemplate(IWebDriver driver)
    {
        _driver = driver;
        _products = new List<Product>();
        _categoryLinks = new List<string>();
        _pagesCount = 0;
    }

    public void StartParing()
    {
        CloseCookie();
        
        NavigateToCategories();
        
        for (var i = 0;i<_categoryLinks.Count;i++)
        {
            
            if (i == GlobalConstants.ParseLimit)
            {
                break;
            }

            var link = _categoryLinks[i];
            
            _driver.Navigate().GoToUrl(link);

            var subCategories = _driver.FindElements(By.CssSelector(".category"));

            if (subCategories.Any())
            {
                foreach (var subCategory in subCategories)
                {
                    var subLink = subCategory.FindElement(By.CssSelector("a"));
                    _categoryLinks.Add(subLink.GetAttribute("href"));
                }
                _pagesCount = 0;
                continue;
            }
            
            
            
            SetPagesCount();
            
            if (_pagesCount != 0)
            {
                for (var j = 0; j < _pagesCount; j++)
                {
                    ScrapProductsInfo();
                
                    MoveToNextPage();
                }
            }
            ScrapProductsInfo();
            _pagesCount = 0;
        }
        
    }

    private void CloseCookie()
    {
        var closeCookieButton = _driver
            .FindElement(By.CssSelector(".message.global.cookie"))
            .FindElement(By.CssSelector("#btn-cookie-allow"));
        
        closeCookieButton.Click();
    }
    private void MoveToNextPage()
    {
        var pagingWrapper = _driver.FindElement(By.CssSelector(".items.pages-items"));
        
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", pagingWrapper);

        var nextButton = pagingWrapper.FindElement(By.CssSelector(".action.next"));

        var href = nextButton.GetAttribute("href");
        
        _driver.Navigate().GoToUrl(href);
    }
    private void ScrapProductsInfo()
    {
        var products = _driver.FindElements(By.CssSelector(".item.product.product-item"));

        for(var i=0;i<products.Count;i++)
        {
            var product = products[i];
            
            var details = product.FindElement(By.CssSelector(".product.details.product-item-details"));
                
            var detailsItems = details.FindElements(By.XPath("./*"));

            decimal toDecimal = 0;
            
            /*selenium always throws this exception instead of returning null, that's why the code is so weird*/
            try
            {
                var price = product.FindElement(By.CssSelector(".price"));
                
                var culture = new CultureInfo("uk-UA");
                
                toDecimal = decimal.Parse(price.Text, NumberStyles.Currency, culture);
            }
            catch (NoSuchElementException e) { }
            
            _products.Add(new Product()
            {
                Title = detailsItems.ElementAtOrDefault(0)?.Text ?? "Not found",
                Manufacturer = detailsItems.ElementAtOrDefault(1)?.Text ?? "Not found",
                Code = detailsItems.ElementAtOrDefault(3)?.Text ?? "Not found",
                Price = toDecimal,
                Category = "Empty"
            });
            
            if ((i + 1) % GlobalConstants.ItemsPerPage == 0)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", product);
            }
        }
    }
    private void NavigateToCategories()
    {
        var menuElements = _driver.FindElements(By.CssSelector(".advancedmenu-link.level-top"));
        
        menuElements[1].Click();
        
        
        var categories = _driver.FindElements(By.CssSelector(".category"));

        foreach (var category in categories)
        {
            var categoryPageLink = category.
                FindElement(By.CssSelector("a"))
                .GetAttribute("href");
            
            _categoryLinks.Add(categoryPageLink);
        }
    }

    private void SetPagesCount()
    {
        /*I explained why above*/
        try
        {
            var pagingWrapper = _driver.FindElement(By.CssSelector(".items.pages-items"));

            var pagesCount = pagingWrapper
                .FindElement(By.CssSelector(".items-count"))
                .FindElement(By.CssSelector(".page"));
            
            _pagesCount = Convert.ToInt32(pagesCount.GetAttribute("textContent"));
        }
        catch (NoSuchElementException e) { }
    }
    
    public ICollection<Product> GetProducts()
    {
        return _products;
    }
    
}