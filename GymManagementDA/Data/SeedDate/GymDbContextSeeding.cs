using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;

namespace GymManagementDAL.Data.SeedDate
{
    public static class GymDbContextSeeding
    {
        public static bool SeedData(GymDbContext dbContext)
        {
            try
            {
                var hasPlans = dbContext.Plans.Any();
                var hasCategories = dbContext.Categories.Any();

                if (hasPlans && hasCategories)
                {
                    return false;
                }
                if (!hasPlans)
                {
                    var plans = LoadDateFromJson<Plan>("Plans.json");
                    if (plans.Any())
                    {
                        dbContext.AddRange(plans);
                    }
                }
                if (!hasCategories)
                {
                    var categories = LoadDateFromJson<Category>("Categories.json");
                    if (categories.Any())
                    {
                        dbContext.AddRange(categories);
                    }
                }

                return dbContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        private static List<T> LoadDateFromJson<T>(string fileName)
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot\\Files",
                fileName
            );
            if (!File.Exists(filePath))
            {
                return [];
            }
            var jsonData = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<T>>(jsonData, options) ?? new List<T>();
        }
    }
}
