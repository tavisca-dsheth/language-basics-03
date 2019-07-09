using System;
using System.Linq;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int FindMax(int[] arr, List<int> index)  {
            return index.Select(ind => arr[ind]).Max();
        }

        public static int FindMin(int[] arr, List<int> index)  {
            return index.Select(ind => arr[ind]).Min();
        }

        public static List<int> FindAllIndex(int[] arr, List<int> index, int elem)  {
            return index.Where(ind => arr[ind] == elem).ToList();
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int[] calorie = new int[protein.Length];

            for(int i=0;i<carbs.Length;i++)  {
                calorie[i] = ((protein[i] + carbs[i]) * 5) + (fat[i] * 9);
            }

            int[] meal = new int[dietPlans.Length];

            for(int i=0;i<dietPlans.Length;i++)  {

                List<int> index = new List<int>(protein.Length); 

                for(int j=0;j<protein.Length;j++)  {
                    index.Add(j);
                }

                char[] plan = dietPlans[i].ToCharArray();
                
                int minVal = 0, maxVal = 0;

                if(plan.Length == 0)  {
                    meal[i] = 0;
                    continue;
                }

                for(int j=0;j<plan.Length;j++)  {
                    switch(plan[j])  {
                        case 'P': maxVal = FindMax(protein, index);
                                  index = FindAllIndex(protein, index, maxVal);
                                  break;
                        case 'p': minVal = FindMin(protein, index);
                                  index = FindAllIndex(protein, index, minVal);
                                  break;
                        case 'F': maxVal = FindMax(fat, index);
                                  index = FindAllIndex(fat, index, maxVal);
                                  break;
                        case 'f': minVal = FindMin(fat, index);
                                  index = FindAllIndex(fat, index, minVal);
                                  break;
                        case 'C': maxVal = FindMax(carbs, index);
                                  index = FindAllIndex(carbs, index, maxVal);
                                  break;
                        case 'c': minVal = FindMin(carbs, index);
                                  index = FindAllIndex(carbs, index, minVal);
                                  break;
                        case 'T': maxVal = FindMax(calorie, index);
                                  index = FindAllIndex(calorie, index, maxVal);
                                  break;
                        case 't': minVal = FindMin(calorie, index);
                                  index = FindAllIndex(calorie, index, minVal);
                                  break;
                    }
                }
                meal[i] = index[0];
            }

            return (meal);
        }
    }
}
