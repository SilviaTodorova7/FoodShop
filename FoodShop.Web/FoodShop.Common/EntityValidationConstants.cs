namespace FoodShop.Common
{
    public static class EntityValidationConstants
    {
        public static class Comment
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 50;

            public const int ContentMinLength = 2;
            public const int ContentMaxLength = 500;
        }

        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 150;
        }

        public static class Product
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 200;

            public const int DescriptionMinLength = 2;
            public const int DescriptionMaxLength = 500;

            public const int PictureUrlMinLength = 2;
            public const int PictureUrlMaxLength = 2048;

            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "100";

            public const int QuantityMinValue = 0;
            public const int QuantityMaxValue = 1000;
        }

        public static class Type
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 100;
        }

        public static class TradeMark
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 200;
        }
    }
}
