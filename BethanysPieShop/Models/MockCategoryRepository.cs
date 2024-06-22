
namespace BethanysPieShop.Models
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> AllCategories => new List<Category>
        { 
          new Category{CategoryId=1,CategoryName="Fruit Pies",Description="All-fruity pies"},
          new Category{CategoryId=2,CategoryName="Cheese cakes",Description="cheesy-all the way"},
          new Category{CategoryId=3,CategoryName="seasonal pies",Description="get "}
        };
    }
}
