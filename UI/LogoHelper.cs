namespace VentasOnline.UI
{
    internal static class LogoHelper
    {
        private const string LogoFileName = "SivarPOSLogo.png";

        public static Image? LoadLogo()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Images", LogoFileName);
            if (!File.Exists(path))
                return null;

            using var source = Image.FromFile(path);
            return new Bitmap(source);
        }
    }
}
