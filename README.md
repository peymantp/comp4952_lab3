# comp4956_lab3

Exercise 1: Create a New Project

Create a new project
Observe that for MVC you can add folders and co-references (e.g., Web Forms)
Observe the controllers already created and their actions:
HomeController
Account
Run the application
Observe authentication
No/Individual/Organization accounts/Windows
Enable authentication with Google, Facebook, Twitter
Observe the responsive design
Exercise 1: Enable Authentication - Google Accounts

In App_Start in Startup.Auth.cs:
Uncomment the following code and add the values for ClientID and ClientSecret
app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
{
ClientId = "000000000000000.apps.googleusercontent.com",
ClientSecret = "000000000000000"
});
}
Run the application: observe login with Google account
Exercise 1: Add a Controller

Start with an MVC application
Notice the folders that have been crated: Content, Models, Scripts, Views
If you want to add a controller, just right click on “Controllers” and choose Add.
Add a new controller: DemoController
The new controller is a class (i.e. DemoController) and has an Index() action
When the user calls the DemoController from the browser, it will call the Index() function which returns a view
Controller Code

public class DemoController : Controller
{
//
// GET: Demo
public ActionResult Index()
{
return View();
}
}
Exercise 1: Add a Controller (2)

Run the Application:
http://localhost:9949/Demo
Error: there is no view
Add a view
Exercise 1: Add a View

Right click on the ActionResult in the DemoController and select: Add View
Explore the templates
Select Empty (without model) – we do not have a model!
The view has an Index.aspx page which will be called when DemoController will be called:
Run the Application:
http://localhost:9949/Demo/Index
We Need Models

Code First/Model First/ DB First
Add models to the MVC application.
A model is a class containing the business logic
Then, an object of that class will be instantiated in the controller
The object’s data will be exposed with the View
You need the following classes:
Step 1: Classes that represent the entity classes (products, categories, sub-categories, etc.)
Step 2: A context class derived from DbContext
using System.Data.Entity;
Step 3: An initializer class
Used for DB initialization
Code-First Approach (1)

(1)Understand the problem and design the model
(2) Create the model classes
(3) Assign the classes that need to be tables in the database: create the Context Class (e.g. StudentContext) that extends DbContext (class in Entity Framework) with all classes that represent tables
Enables CRUD
Note: Some model classes are not intended to represent database tables: they are only business logic classes!
Code-First Approach (2)

(4) Set the location and name of the database
Local DB:
In the application Web.Config make a copy of the connection string
Replace DefaultConnection with the name of the context class (e.g. StudentContext)
Replace the mdf file with the database name (e.g., Student.mdf)
Useful site: http://www.connectionstrings.com/
(5) Initialize the data: create a class (e.g. InitializerDB.cs) that inherits from the EF class DropCreateDatabaseIfModelChanges
Override the Seed method of DropCreateDatabaseIfModelChanges
Create lists of objects and add to the context
Code-First Approach (3)

(6) Set the database initializer in Global.asax in Application_Start()
Database.SetInitializer<StudentContext>(new InitializerDB());
(7) Add the navigation links to Views/Shared/Layout
<li>@Html.ActionLink("Programs", "Index", "Programs")</li>
<li>@Html.ActionLink("Students", "Index", "Students")</li>
(8) Create the Controllers using the appropriate model class and context
Rebuild the application
Create new controllers or you can write code in an existing controller
The Index method will call the appropriate view
return View(db.Programs.ToList());
(9) Display data in the View
Automatically the default view is created when the controller is created
Code-First Approach (4)

The database will be created when the application is run
Click the icon “Show All Files” and view the database
Include the database in the project
In Class Activity

Create anASP.NET MVC 5 application that
Has two pages: Product and Category.
A Category can have several Products
In Class Activity: Add Models

Right-click the Model folder and add classes:
Product class
Category class
Context class
Initializer class
Entity Class Category

using System.ComponentModel.DataAnnotations;
public class Category
{
[ScaffoldColumn(false)]
public int CategoryID { get; set; }
[Required, StringLength(200), Display(Name = "Name")]
public string CategoryName { get; set; }
[Display(Name = "Product Description")]
public string Description { get; set; }
// collection of products
public virtual ICollection<Product> Products { get; set; }
}
Entity Class Product

using System.ComponentModel.DataAnnotations;
public class Product
{
[ScaffoldColumn(false)]
public int ProductID { get; set; }
[Required, StringLength(100), Display(Name = "Name")]
public string ProductName { get; set; }
[Required, StringLength(10000), Display(Name = "Product Description"), DataType(DataType.MultilineText)]
public string Description { get; set; }
public string ImagePath { get; set; }
[Display(Name = "Price")]
public double? UnitPrice { get; set; }
public int? CategoryID { get; set; }
// virtual property for the product
public virtual Category Category { get; set; }
}
}
Data Annotations

Data annotation attributes describe:
how to validate user input for that member
specify formatting
specify how the member is modeled
[ScaffoldColumn(false)]
False for columns like ID
Specifies whether a class or data column uses scaffolding
Scaffolding is the mechanism for generating web page templates (views) based on database schemas
[Required, StringLength(100), Display(Name = "Name")]
[Display(Name = "Product Description")]
Context Class

Manages the entity classes and provides data access to the database
public class ProductContext : DbContext
{
public ProductContext()
: base("ArtStore") { }
public DbSet<Category> Categories { get; set; }
public DbSet<Product> Products { get; set; }
}
Modify Web.Config

Set the location and name of the database
Local DB:
In the application Web.Config make a copy of the connection string
Replace DefaultConnection with the name of the context class (e.g. StudentContext)
Replace the mdf file with the database name (e.g., Student.mdf)
Useful site: http://www.connectionstrings.com/
DB Initializer Class

Custom logic to initialize the database for the first time when the context is used
Contains the Seed() class and static methods for content
Inherits DropCreateDatabaseIfModelChanges
To recognize if the model (schema) has changed before resetting the seed data
If no changes are made to the entity classes, the database will not be reinitialized with the seed data
Initializer Class (2)

using System.Data.Entity;
namespace ArtSupplyStore.Models
{
public class ProductDatabaseInitializer: DropCreateDatabaseIfModelChanges<ProductContext>
{
protected override void Seed(ProductContext context)
{
base.Seed(context);
}
private static List<Category> GetCategories()
{
var categories = new List<Category>
{
};
return categories;
}
private static List<Product> GetProducts()
{
var products = new List<Product>
{
};
return products;
}
}
}
Initializer Class (3)

using System.Data.Entity;
public class ProductDatabaseInitializer : DropCreateDatabaseIfModelChanges<ProductContext>
{
protected override void Seed(ProductContext context)
{
GetCategories().ForEach(c => context.Categories.Add(c));
GetProducts().ForEach(p => context.Products.Add(p));
}
private static List<Category> GetCategories()
{
var categories = new List<Category>
{
new Category
{
CategoryID = 1,
CategoryName = "Media"
}, ….
}
Add Controllers (2)

Add two Controllers: CategoriesController and ProductsController
Select MVC 5 Controllers with views using Entity Framework
Select the models that were created
Connect to the product context
in Data context class select: ProductContext (ArtStore.Models)
Asynchronous Controller

The AsyncController class enables developers to write asynchronous action methods
Asynchronous controllers avoid blocking the Web server from performing work while the request is being processed
A typical use for the AsyncController class is long-running Web service calls
Modify Global.asax

Set the database initializer in Global.asax in Application_Start()
Database.SetInitializer<StudentContext>(new InitializerDB());
Add links to the New Controllers

Add the navigation links to Views/Shared/Layout
<li>@Html.ActionLink("Categories", "Index", "Categories")</li>
<li>@Html.ActionLink("Products", "Index", "Products")</li>
Add Data

Add data using Create New
Observe the database content
Enable Code First Migrations

If data model changes, the model changes and gets out of sync with the database
Configure the Entity Framework to automatically drop and re-create the database each time you change the data model
When entity classes are added, removed, or changed, or the DbContext class is changed, the next time the application runs it automatically deletes the existing database, creates a new one, and seeds it with test data
http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/migrations-and-deployment-with-the-entity-framework-in-an-asp-net-mvc-application
Enable Code First Migrations (2)

Use the Tools/NuGet Package Manager/ Package Manager Console
PM> enable-migrations
PM> update-database
You might need to set in Migrations/Configuration.cs:
AutomaticMigrationsEnabled = true;
AutomaticMigrationDataLossAllowed = true;
