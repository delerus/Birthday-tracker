namespace Birthday_tracker.Utils
{
    public static class ImageValidator
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private static readonly string[] AllowedMimeTypes = { "image/jpeg", "image/png", "image/gif", "image/webp" };

        public static bool IsImage(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
                return false;

            if (!AllowedMimeTypes.Contains(file.ContentType))
                return false;

            return true;
        }
    }
}
