namespace NTier.UI.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidPrice(string text)
        {
            return double.TryParse(text, out var price) && price > 0;
        }

        public static bool IsValidStock(string text)
        {
            return int.TryParse(text, out var stock) && stock >= 0;
        }

        public static bool IsNotEmpty(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
    }
}
