using System.Text.RegularExpressions;

namespace OOP_6Lab_Exceptions_2block
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "ProgramObjects");

            Regex regexExtForImage = new Regex("^((bmp)|(gif)|(tiff?)|(jpe?g)|(png))$",
                RegexOptions.IgnoreCase);

            string[] files = Directory.GetFiles(folder);

            foreach (string fileName in files)
            {
                string ext = Path.GetExtension(fileName).TrimStart('.');

                try
                {
                    Bitmap image = new Bitmap(fileName);

                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);

                    string nameOnly = Path.GetFileNameWithoutExtension(fileName);
                    string newFile = Path.Combine(folder, nameOnly + "-mirrored.gif");

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
