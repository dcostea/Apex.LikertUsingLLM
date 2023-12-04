using Newtonsoft.Json;
using Serilog;

namespace Apex.LikertUsingLLM.Helpers;

public static class PrintHelper
{
    public static void PrintTitle(string question, string answer, string ground_truth)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("========================================================================================================================================================================");
        Console.WriteLine($"Question: {question}");
        Console.WriteLine($"Answer: {answer}");
        Console.WriteLine($"Ground Truth: {ground_truth}");
        Console.WriteLine("========================================================================================================================================================================");
    }

    public static void PrintResults(string question, string groundTruth, Dictionary<string, Dictionary<string, Dictionary<string, string>>> modelPaths, Dictionary<string, double> elapsedTimes)
    {
        const int FirstColumnLength = 90;
        const int ColumnLength = 13;

        Console.ForegroundColor = ConsoleColor.Green;
        var answers = modelPaths.Values.First();

        foreach (var modelPath in modelPaths)
        {
            var modelName = modelPath.Key.GetModelName();
            Console.WriteLine("========================================================================================================================================================================");
            Console.WriteLine($"[{modelName.ToUpper()}] Elapsed time: {elapsedTimes[modelName]:#}s");
            Console.WriteLine($"Question: {question}");
            Console.WriteLine($"Best answer: {groundTruth}");
            Console.WriteLine("========================================================================================================================================================================");
            Console.Write($"{"Answer",FirstColumnLength}");
            foreach (var column in answers.First().Value.Keys)
            {
                Console.Write($"{column, ColumnLength}");
            }
            Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

            foreach (var answer in modelPath.Value)
            {
                Console.Write($"{(answer.Key.Length > FirstColumnLength ? answer.Key[..FirstColumnLength] : answer.Key), FirstColumnLength}");
                foreach (var rating in answer.Value)
                {
                    if (string.IsNullOrWhiteSpace(rating.Value))
                    {
                        Console.Write($"{"?",ColumnLength}");
                    }
                    else 
                    {
                        Console.Write($"{rating.Value,ColumnLength}");
                    }
                }
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
        }

        var json = JsonConvert.SerializeObject(modelPaths, Formatting.Indented);
        Log.Debug(json);
    }

    public static string GetModelName(this string modelPath)
    {
        return Path.GetFileNameWithoutExtension(modelPath).Split('.')[0];
    }

    internal static void PrintAnswers(List<string> answers)
    {
        Console.WriteLine("========================================================================================================================================================================");
        Console.WriteLine("Answers (complete length):");
        Console.WriteLine("========================================================================================================================================================================");

        foreach (var answer in answers)
        {
            Console.WriteLine(answer);
        }
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
    }
}
