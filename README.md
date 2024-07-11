# ProductAPI
Sure, here's a detailed README for your WebAPI project, outlining how to create the project, add a `ProductController` with CRUD operations using the Entity Framework Code First approach, and instructions for testing with Postman. 

### README.md

---

# ProductAPI WebAPI Project

## Introduction
This project demonstrates how to create a WebAPI using ASP.NET, with a `ProductController` implementing CRUD (Create, Read, Update, Delete) operations. The data is stored in a database using Entity Framework Code First approach.

## Requirements
- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
- SQL Server (LocalDB or other)
- Postman (for testing the API)
- GitHub account (for pushing the code)

## Project Setup

### Step 1: Create WebAPI Project
1. Open Visual Studio.
2. Create a new project.
3. Select **ASP.NET Web Application (.NET Framework)**.
4. Name the project `ProductAPI`.
5. Choose **Web API** template and click **Create**.

### Step 2: Install Entity Framework
1. Open **Package Manager Console** from Tools > NuGet Package Manager.
2. Run the following command:
   ```
   Install-Package EntityFramework
   ```

### Step 3: Add Product Model
Create a new folder named `Models` and add a `Product` class inside it:
```csharp
namespace ProductAPI.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
```

### Step 4: Create ProductContext
Create a new folder named `Context` and add a `ProductContext` class inside it:
```csharp
using System.Data.Entity;
using ProductAPI.Models;

namespace ProductAPI.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
```

### Step 5: Initialize the Database
Create an `Initializer` class in the `Context` folder:
```csharp
using System.Collections.Generic;
using System.Data.Entity;
using ProductAPI.Models;

namespace ProductAPI.Context
{
    public class Initializer : DropCreateDatabaseIfModelChanges<ProductContext>
    {
        protected override void Seed(ProductContext context)
        {
            base.Seed(context);
            var products = new List<Product>
            {
                new Product { Name = "Coke", Quantity = 100, Price = 3, Description = "Beverage" },
                new Product { Name = "Red Bull", Quantity = 100, Price = 3, Description = "Beverage" },
                new Product { Name = "Vodka", Quantity = 100, Price = 10, Description = "Beverage" }
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}
```

### Step 6: Configure Database Initializer
In `Global.asax`, set the database initializer:
```csharp
using System.Data.Entity;
using System.Web.Http;
using ProductAPI.Context;

namespace ProductAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer(new Initializer());
        }
    }
}
```

### Step 7: Add ProductController
Create a new `ProductController` in the `Controllers` folder:
```csharp
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ProductAPI.Context;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    public class ProductController : ApiController
    {
        private ProductContext db = new ProductContext();

        // GET: api/Product
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var result = db.Products.Select(product => new
                {
                    product.ID,
                    product.Name,
                    product.Price,
                    product.Quantity,
                    product.Description
                }).ToList();
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Product/5
        [HttpGet]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Product
        [HttpPost]
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = product.ID }, product);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Product/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ID)
            {
                return BadRequest();
            }

            try
            {
                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return InternalServerError(ex);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Product/5
        [HttpDelete]
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }

                db.Products.Remove(product);
                db.SaveChanges();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ID == id) > 0;
        }
    }
}
```

### Step 8: Configure Routing
Ensure your `WebApiConfig` has default routes configured in `App_Start/WebApiConfig.cs`:
```csharp
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
        config.MapHttpAttributeRoutes();

        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );
    }
}
```

### Step 9: Test Endpoints in Postman
1. **Get All Products**: `GET http://localhost:[port]/api/Product`
2. **Get Product by ID**: `GET http://localhost:[port]/api/Product/{id}`
3. **Create Product**: `POST http://localhost:[port]/api/Product`
   - Body (JSON):
     ```json
     {
         "Name": "Pepsi",
         "Quantity": 50,
         "Price": 2,
         "Description": "Beverage"
     }
     ```
4. **Update Product**: `PUT http://localhost:[port]/api/Product/{id}`
   - Body (JSON):
     ```json
     {
         "ID": 1,
         "Name": "Pepsi",
         "Quantity": 60,
         "Price": 2,
         "Description": "Soft Drink"
     }
     ```
5. **Delete Product**: `DELETE http://localhost:[port]/api/Product/{id}`


This README provides a comprehensive guide to setting up your WebAPI project, adding the `ProductController` with CRUD operations, testing with Postman, and pushing the code to GitHub. If you need further assistance or have any questions, feel free to ask!
