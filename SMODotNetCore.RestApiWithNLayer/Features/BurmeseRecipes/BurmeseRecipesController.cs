using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace SMODotNetCore.RestApiWithNLayer.Features.BurmeseRecipes
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurmeseRecipesController : ControllerBase
    {
        private async Task<List<Class1>> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("BurmeseRecipes.json");
            var models = JsonConvert.DeserializeObject<List<Class1>>(jsonStr);
            return models!;
        }

        [HttpGet("name")]
        public async Task<IActionResult> Name()
        {
            var models = await GetDataAsync();
            if (models.Count > 0)
            {
                List<string> names = models.Select(m => m.Name).ToList();
                return Ok(names);
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Answser(String name)
        {
            var recipes = await GetDataAsync();
            if (recipes != null)
            {
                var recipe = recipes.FirstOrDefault(r => r.Name == name);
                if (recipe != null)
                {
                    return Ok(recipe);
                }
            }
            return NotFound();
        }
    }

    public class BurmeseRecipes
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string CookingInstructions { get; set; }
        public string UserType { get; set; }
    }

}
