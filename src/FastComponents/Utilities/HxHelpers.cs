namespace FastComponents;

public static class Hx
{
    public static class Swap
    {
        public const string InnerHtml = "innerHTML";
        public const string OuterHtml = "outerHTML";
        public const string BeforeBegin = "beforebegin";
        public const string AfterBegin = "afterbegin";
        public const string BeforeEnd = "beforeend";
        public const string AfterEnd = "afterend";
        public const string Delete = "delete";
        public const string None = "none";
    }

    public static string TargetId(string id)
        => $"#{id}";
}