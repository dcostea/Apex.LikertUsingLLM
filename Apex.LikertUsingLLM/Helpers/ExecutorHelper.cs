using LLama.Common;
using LLama;
using Microsoft.Extensions.Logging;
using LLama.Native;

namespace Apex.LikertUsingLLM.Helpers;

public static class ExecutorHelper
{
    public static InferenceParams? InferenceParams { get; set; }
    public static ModelParams? ModelParams { get; set; }

    public static StatelessExecutor CreateExecutor(string modelPath)
    {
        ModelParams!.ModelPath = modelPath;
        var weights = LLamaWeights.LoadFromFile(ModelParams!);
        //var context = weights.CreateContext(ModelParams!);

        var ex = new StatelessExecutor(weights, ModelParams!);

        return ex;
    }

    public static async Task<string> ExecuteWithSpinnerAsync(this StatelessExecutor ex, string prompt)
    {
        var result = string.Empty;

        await foreach (var text in ex.InferAsync(prompt, InferenceParams).Spinner())
        {
            Console.Write(text);
            result += text;
        }

        Console.WriteLine();

        return result;
    }

    public static async IAsyncEnumerable<string> Spinner(this IAsyncEnumerable<string> source)
    {
        var enumerator = source.GetAsyncEnumerator();

        var characters = new[] { '|', '/', '-', '\\' };

        while (true)
        {
            var next = enumerator.MoveNextAsync();

            var (Left, Top) = Console.GetCursorPosition();

            // Keep showing the next spinner character while waiting for "MoveNextAsync" to finish
            var count = 0;
            while (!next.IsCompleted)
            {
                count = (count + 1) % characters.Length;
                Console.SetCursorPosition(Left, Top);
                Console.Write(characters[count]);
                await Task.Delay(100);
            }

            // Clear the spinner character
            Console.SetCursorPosition(Left, Top);
            Console.Write(" ");
            Console.SetCursorPosition(Left, Top);

            if (!next.Result)
                break;
            yield return enumerator.Current;
        }
    }
}
