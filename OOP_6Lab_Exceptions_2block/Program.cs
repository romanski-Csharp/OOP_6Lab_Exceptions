using System.Text.RegularExpressions;

namespace OOP_6Lab_Exceptions_2block
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            String folder = Directory.GetCurrentDirectory();

            Regex regexExtForImage = new Regex("^((bmp)|(gif)|(tiff?)|(jpe?g)|(png))$",
                RegexOptions.IgnoreCase);

            String[] files = Directory.GetFiles(folder);

            foreach (String fileName in files)
            {
                String ext = Path.GetExtension(fileName).TrimStart('.');

                try
                {
                    Bitmap image = new Bitmap(fileName);

                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);

                    String nameOnly = Path.GetFileNameWithoutExtension(fileName);
                    String newFile = Path.Combine(folder, nameOnly + "-mirrored.gif");

                    image.Save(newFile, System.Drawing.Imaging.ImageFormat.Gif);

                    image.Dispose();
                }
                catch
                {
                    if (regexExtForImage.IsMatch(ext))
                    {
                        MessageBox.Show(
                            "Файл \"" + fileName + "\" має графічне розширення,\n" +
                            "але не містить картинки.",
                            "Помилка читання",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }
            }

            MessageBox.Show("Обробку завершено!");
        }
    }
}
