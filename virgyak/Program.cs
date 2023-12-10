using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

class Program
{
    static List<string[]> questionAnswers = new List<string[]>();
    static Random random = new Random();

    static void Main()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

        LoadQuestions("adatok.xlsx");

        while (true)
        {
            Console.Clear();
            DisplayRandomQuestion();
            Console.ReadLine(); // Wait for the user to press Enter

            DisplayAnswer();
            Console.ReadLine(); // Wait for the user to press Enter
        }
    }

    static void LoadQuestions(string fileName)
    {
        // Combine the current directory with the file name
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            int rows = worksheet.Dimension.Rows;

            for (int i = 1; i <= rows; i += 2)
            {
                string question = worksheet.Cells[i, 1].Text;
                string answer = (i + 1 <= rows) ? worksheet.Cells[i + 1, 1].Text : "No answer available";
                questionAnswers.Add(new string[] { question, answer });
            }
        }
    }

    static void DisplayRandomQuestion()
    {
        int index = random.Next(questionAnswers.Count);
        Console.WriteLine(questionAnswers[index][0]);
    }

    static void DisplayAnswer()
    {
        int index = random.Next(questionAnswers.Count);
        Console.Write($"{questionAnswers[index][1]}");
    }
}
