using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CssReplacer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fileContent = File.ReadAllLines(args[0]);
            var newContent = new StringBuilder();

            foreach (var row in fileContent)
            {
                Console.WriteLine($"Row before modification - '{row}'");

                if (row.Contains("fixed"))
                {
                    Console.WriteLine($"Fixed shit - '{row}'");
                }

                var newRow = row;
                if (row.Contains("float:"))
                {
                    newRow = $"@include float({GetUnit(row)});";
                }
                else if (row.Contains("text-align:"))
                {
                    newRow = $"@include text-align({GetUnit(row)});";
                }
                else if (row.Contains("margin-left:"))
                {
                    newRow = $"@include margin-left({GetUnit(row)});";
                }
                else if (row.Contains("margin-right:"))
                {
                    newRow = $"@include margin-right({GetUnit(row)});";
                }
                else if (row.Contains("padding-left:"))
                {
                    newRow = $"@include padding-left({GetUnit(row)});";
                }
                else if (row.Contains("padding-right:"))
                {
                    newRow = $"@include padding-right({GetUnit(row)});";
                }
                else if (row.Trim().StartsWith("right:"))
                {
                    newRow = $"@include right({GetUnit(row)});";
                }
                else if (row.Trim().StartsWith("left:"))
                {
                    newRow = $"@include left({GetUnit(row)});";
                }
                else if (row.Trim().StartsWith("margin:"))
                {
                    var units = row.Split(':')[1].Split(new []{" "}, StringSplitOptions.RemoveEmptyEntries);

                    if (units.Length >= 4)
                    {
                        newRow = $"@include margin({units[0]}, {units[1]}, {units[2]}, {GetUnit(units[3])});";
                    }
                }
                else if (row.Contains("padding:"))
                {
                    var units = row.Split(':')[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    if (units.Length >= 4)
                    {
                        newRow = $"@include padding({units[0]}, {units[1]}, {units[2]}, {GetUnit(units[3])});";
                    }
                }


                Console.WriteLine($"Row after modification - '{newRow}'");
                newContent.AppendLine(newRow);
            }


            File.WriteAllLines(args[1], newContent.ToString().Split(new []{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries));
        }

        private static string GetUnit(string row)
        {
            return row.Split(':').Last().Split(';').First();
        }
    }
}
