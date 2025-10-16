using Domain.Enums;

namespace Data.Helpers
{
    public static class EnumHelper
    {
        private static readonly Random _random = new Random();

        public static Gender ParseGender(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                var genders = Enum.GetValues<Gender>();
                return genders[_random.Next(genders.Length)];
            }
            return value.ToLower() switch
            {
                "male" => Gender.Male,
                "female" => Gender.Female,
                _ => Enum.GetValues<Gender>()[_random.Next(Enum.GetValues<Gender>().Length)]
            };
        }

        public static ProductCategory ParseProductCategory(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                var categories = Enum.GetValues<ProductCategory>();
                return categories[_random.Next(categories.Length)];
            }
            return value.ToLower() switch
            {
                "bronze" => ProductCategory.Bronze,
                "silver" => ProductCategory.Silver,
                "gold" => ProductCategory.Gold,
                _ => Enum.GetValues<ProductCategory>()[_random.Next(Enum.GetValues<ProductCategory>().Length)]
            };
        }

        // Парсю якість сервісу з тексту або числа (конвертую числові оцінки 1-10 у Low/Medium/High)
        public static ServiceQuality ParseServiceQuality(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                var qualities = Enum.GetValues<ServiceQuality>();
                return qualities[_random.Next(qualities.Length)];
            }
            
            if (decimal.TryParse(value, out decimal numValue))
            {
                if (numValue <= 3.0m) return ServiceQuality.Low;
                if (numValue <= 7.0m) return ServiceQuality.Medium;
                return ServiceQuality.High;
            }
            
            return value.ToLower() switch
            {
                "low" => ServiceQuality.Low,
                "medium" => ServiceQuality.Medium,
                "high" => ServiceQuality.High,
                _ => Enum.GetValues<ServiceQuality>()[_random.Next(Enum.GetValues<ServiceQuality>().Length)]
            };
        }

        // Парсю рівень лояльності з тексту або числа (конвертую 1-10 у Low/Medium/High)
        public static LoyaltyLevel ParseLoyaltyLevel(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                var levels = Enum.GetValues<LoyaltyLevel>();
                return levels[_random.Next(levels.Length)];
            }
            
            if (int.TryParse(value, out int numValue))
            {
                if (numValue <= 3) return LoyaltyLevel.Low;
                if (numValue <= 7) return LoyaltyLevel.Medium;
                return LoyaltyLevel.High;
            }
            
            return value.ToLower() switch
            {
                "low" => LoyaltyLevel.Low,
                "medium" => LoyaltyLevel.Medium,
                "high" => LoyaltyLevel.High,
                _ => Enum.GetValues<LoyaltyLevel>()[_random.Next(Enum.GetValues<LoyaltyLevel>().Length)]
            };
        }

        public static bool ParseBool(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            var lowerValue = value.ToLower();
            
            if (lowerValue.Contains("yes") || lowerValue.Contains("true") || lowerValue == "1" ||
                lowerValue.Contains("high") || lowerValue.Contains("medium") || lowerValue.Contains("low") ||
                lowerValue.Contains("sensitive"))
                return true;
            
            if (lowerValue.Contains("no") || lowerValue.Contains("false") || lowerValue == "0" ||
                lowerValue.Contains("none") || lowerValue.Contains("not"))
                return false;
            
            return false;
        }

        // Конвертую текстовий ID у число (якщо текст - використовую хеш-код)
        public static int ParseCustomerId(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return 0;
            
            if (int.TryParse(value, out int numericId))
                return numericId;
            
            return Math.Abs(value.GetHashCode());
        }

        // Парсю грошові значення (прибираю символи валют $, €, £)
        public static decimal ParseCurrency(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return 0;
            
            value = value.Trim()
                        .Replace("$", "")
                        .Replace("€", "")
                        .Replace("£", "")
                        .Replace(",", "")
                        .Trim();
            
            if (decimal.TryParse(value, System.Globalization.NumberStyles.Any, 
                System.Globalization.CultureInfo.InvariantCulture, out decimal result))
                return result;
            
            return 0;
        }
    }
}
