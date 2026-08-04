namespace NTier.Business.Validators
{
    public static class ReadyRegexes
    {
        public static string NoNumberFormat { get; } = @"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$";
        public static string PhoneFormat { get; } = "(\\+?( |-|\\.)?\\d{1,2}( |-|\\.)?)?(\\(?\\d{3}\\)?|\\d{3})( |-|\\.)?(\\d{3}( |-|\\.)?\\d{4})";
        public static string OnlyNumberFormat { get; } = @"^\d+$";
    }
}