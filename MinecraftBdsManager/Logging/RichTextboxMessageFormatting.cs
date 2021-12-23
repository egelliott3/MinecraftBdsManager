namespace MinecraftBdsManager.Logging
{
    internal class RichTextboxMessageFormatting
    {
        public RichTextboxMessageFormatting()
        {
            TextToMatch = string.Empty;
        }

        public string TextToMatch { get; set; }

        public Color Color { get; set; }

        public FontStyle FontStyle { get; set; }
    }
}
