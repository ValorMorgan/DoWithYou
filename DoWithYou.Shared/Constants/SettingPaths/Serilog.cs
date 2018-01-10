namespace DoWithYou.Shared.Constants.SettingPaths
{
    public static class Serilog
    {
        public static string Root => "Serilog";

        public static class MinimumLevel
        {
            public static string Root => $"{Serilog.Root}:MinimumLevel";

            public static string Default => $"{Root}:Default";
            public static class Override
            {
                public static string Root => $"{MinimumLevel.Root}:Override";

                public static string Microsoft => $"{Root}:Microsoft";

                public static string System => $"{Root}:System";
            }
        }

        public static class Enrich
        {
            public static string Root => $"{Serilog.Root}:Enrich";

            public static string Source(string name) => $"{Root}:{name}";
        }

        public static class WriteTo
        {
            public static string Root => $"{Serilog.Root}:WriteTo";

            public static string Source(string name) => $"{Root}:{name}";

            public static string Name(string source) => $"{Source(source)}:Name";

            public static class Args
            {
                private static string Root(string source) => $"{Source(source)}:Args";

                public static string PathFormat(string source) => $"{Root(source)}:pathFormat";

                public static string OutputTemplate(string source) => $"{Root(source)}:outputTemplate";
            }
        }

        public static class Template
        {
            public static string Root => $"{Serilog.Root}:Template";

            public static string Source(string name) => $"{Root}:{name}";

            public static string Name(string source) => $"{Source(source)}:Name";

            public static string Format(string source) => $"{Source(source)}:Format";
        }
    }
}
