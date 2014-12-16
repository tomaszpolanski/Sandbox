using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox.Linq
{
    public static class LinqSandbox
    {
        public static void Basics()
        {
            List<int> normalList = new List<int> { 1, 2, 3, 4, 5 };

            IEnumerable<int> aBitDifferentList = new List<int> { 1, 2, 3, 4, 5 };
            int element1 = normalList[0];
            //int element2 = aBitDifferentList[0];
        }

        public static IEnumerable<int> LinqBasics()
        {
            var items = new List<int> { 1, 2, 3, 4, 5 };

            var oddItems = items.Where(number => number % 2 == 1)
                                .Select(number => number * number)
                                .Take(3)
                                .OrderByDescending(number => number);

            return oddItems;
        }

        public static IEnumerable<int> LinqLessBasic()
        {
            var items = Enumerable.Range(0, int.MaxValue);

            int whereCount = 0;
            int selectCount = 0;

            var oddItems = items.Where(number => { whereCount++; return number % 2 == 1; })
                                .Select(number => { selectCount++; return number * number; })
                                .Take(3)
                                .OrderByDescending(number => number)
                                .ToList();

            return oddItems;
        }


        public static string FoldTextToOneLine(string s, int maxLines = 8, string foldSeperator = ", ")
        {
            // no null check is intentional
            List<string> lines = new List<string>();
            string[] strings = s.Split('\n', ',', ';'); 
            foreach (string line in strings)
            {
                string trimmedLine = line.Trim();
                if (trimmedLine.Length > 0)
                {
                    lines.Add(trimmedLine);
                    if (lines.Count >= maxLines)
                    {
                        break;
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lines.Count; i++)
            {
                sb.Append(lines[i]);
                if (i < lines.Count - 1)
                {
                    sb.Append(foldSeperator);
                }
            }

            return sb.ToString();
        }

        public static string FoldTextToOneLineLinq(string multiLineString, int maxLines = 8, string foldSeperator = ", ")
        {
            // no null check is intentional
            return multiLineString.Split('\n', ',', ';')
                                  .Select(line => line.Trim())
                                  .Where(trimmedLine => trimmedLine.Length > 0)
                                  .Take(maxLines)
                                  .Aggregate((first, second) => first + foldSeperator + second); 
        }
    }
}
