using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PedaliDemExam.Context.Product;

namespace PedaliDemExam.Context
{
    public class PickUpPoint
    {
        public PickUpPoint() 
        {
            Address = string.Empty;
        }
        public PickUpPoint(string address)
        {
            Address = address;
        }
        public int Id { get; set; }
        public string Address { get; set; }
    }
    public class ProductType
    {
        public ProductType()
        {
            Title = string.Empty;
        }
        public ProductType(string title)
        {
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class Manufacturer
    {
        public Manufacturer()
        {
            Title = string.Empty;
        }
        public Manufacturer(string title)
        {
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class Provider
    {
        public Provider()
        {
            Title = string.Empty;
        }
        public Provider(string title)
        {
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class User
    {
        public User()
        {
            EmployeeRole = Role.Client;
            FullName = string.Empty;
            Login = string.Empty;
            Password = string.Empty;
        }
        public User(Role employeeRole, string fullName, string login, string password)
        {
            EmployeeRole = employeeRole;
            FullName = fullName;
            Login = login;
            Password = password;
        }
        public enum Role
        {
            Admin, Manager, Client
        }
        public int Id { get; set; }
        public Role EmployeeRole { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RoleText => EmployeeRole switch
        {
            Role.Admin => "Администратор",
            Role.Manager => "Менеджер",
            Role.Client => "Авторизированный клиент",
        };
    }
    public class Product
    {
        public Product()
        {
            Article = string.Empty;
            ProductTypes = null!;
            UnitOfMeasurement = string.Empty;
            Price = 0;
            Provider = null!;
            Manufacturer = null!;
            ProductCategory = Category.Woman;
            Discount = 0;
            CountOnStorage = 0;
            Description = string.Empty;
            Image = null;
        }
        public Product(string article, ProductType productTypes, string unitOfMeasurement, decimal price, Provider provider, Manufacturer manufacturer, Category productCategory, int discount, int countOnStorage, string description, string? image)
        {
            Article = article;
            ProductTypes = productTypes;
            UnitOfMeasurement = unitOfMeasurement;
            Price = price;
            Provider = provider;
            Manufacturer = manufacturer;
            ProductCategory = productCategory;
            Discount = discount;
            CountOnStorage = countOnStorage;
            Description = description;
            Image = image;
        }

        public enum Category
        {
            Woman, Man
        }
        public int Id { get; set; }
        public string Article { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductTypes { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal Price { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Category ProductCategory { get; set; }
        public int Discount { get; set; }
        public int CountOnStorage { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string CategoryText => ProductCategory switch
        {
            Category.Woman => "Женская обувь",
            Category.Man => "Мужская обувь",
        };

        public decimal DiscountPrice => Price - Price * ((Decimal)Discount / 100m);
        public string CategoryAndTitle => CategoryText + " | " + ProductTypes.Title;
        public string ImageUrl => string.IsNullOrEmpty(Image) ? "/Assets/picture.png" : $"/Assets/{Image}";

        public bool IsDiscountGreaterThen0 => Discount > 0;
        public bool IsDiscountGreaterThen15 => Discount > 15;
    }
    public class Order
    {
        public Order()
        {
            Number = 0;
            Product = null!;
            Count = 0;
            DateOfOrder = DateTime.Now;
            DateOfDelivery = null;
            PickupPoint = null!;
            User = null!;
            Code = string.Empty;
            IsCompleted = false;
        }
        public Order(int number, Product product, int count, DateTime dateOfOrder, DateTime? dateOfDelivery, PickUpPoint pickupPoint, User user, string code, bool isCompleted)
        {
            Number = number;
            Product = product;
            Count = count;
            DateOfOrder = dateOfOrder;
            DateOfDelivery = dateOfDelivery;
            PickupPoint = pickupPoint;
            User = user;
            Code = code;
            IsCompleted = isCompleted;
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime? DateOfDelivery { get; set; }
        public int PickUpPointId { get; set; }
        public PickUpPoint PickupPoint { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Code { get; set; }
        public bool IsCompleted { get; set; }
    }
}
