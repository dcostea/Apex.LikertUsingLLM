using Apex.LikertUsingLLM.Helpers;
using LLama.Common;
using LLama.Native;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;

var loggerFactory = new LoggerFactory()
    .AddSerilog(new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("logs/logs.txt",
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 15,
            outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss.fff} {Level:u3}] ({ThreadID}) {Message:lj}{NewLine}{Exception}",
            shared: true
        )
        .CreateLogger()
    );
var logger = loggerFactory.CreateLogger("");

var modelPaths = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>
{
    //{ @"c:\temp\LLMs\claude2-alpaca-13b.Q4_K_M.gguf", new() },
    //{ @"c:\temp\LLMs\claude2-alpaca-7b.Q4_K_M.gguf", new() },
    { @"c:\temp\LLMs\zephyr-7b-beta.Q5_K_M.gguf", new() },
    //{ @"c:\temp\LLMs\mistral-7b-v0.1.Q4_K_M.gguf", new() },
    //{ @"c:\temp\LLMs\yarn-mistral-7b-128k.Q4_K_M.gguf", new() },
    //{ @"c:\temp\LLMs\wizardlm-13b-v1.2.Q4_K_M.gguf", new() },
};

var elapsedTimes = new Dictionary<string, double>();

ExecutorHelper.InferenceParams = new InferenceParams()
{
    Temperature = 0.0f,
    //AntiPrompts = new List<string> { "Question", "Answer", "#", "\n", "." },
    MaxTokens = 1
};

ExecutorHelper.ModelParams = new ModelParams(modelPaths.First().Key)
{
    ContextSize = 512,
    Seed = 1337,
    GpuLayerCount = 10
};


var question = "What is a black hole?";
List<string> answers = [
    "Tic tac toe.",
    "gravity collapse black.",
    "A black dog is an animal.",
    "A black hole is like a normal hole.",
    "A black hole is a meteorite.",
    "A black hole is a an object.",
    "A black hole is a star.",
    "A black hole is a dead star.",
    "A black dog is a dead star.",
    "A black hole is a breed of dog.",
    "A black hole is an object which attracts any nearby object.",
    "A black hole is a dead star so heavy that it captures even the light.",
    "A black hole is like a one way road to hell.",
    "Black holes are formed by the gravitational collapse of massive stars, resulting in a singularity of infinite density.",
    "Black holes are not formed by the gravitational collapse of massive stars, resulting in a singularity of infinite density.",
    "Black horses are formed by the gravitational collapse of massive stars, resulting in a singularity of infinite density.",
    "Black holes: the final act in the drama of a massive star’s existence.",
    "Black holes are collapsed massive stars running out of fuel and collapsing under its own gravity. The singularity is forming, which is a point of infinite density where the laws of physics as we know them break down. The singularity means that nothing, not even light, can escape the black hole's gravity. This is what makes black holes \"black\" - they appear invisible because all the light and matter that falls into them is trapped forever.",
    "Black holes are formed when a massive star (at least eight times the mass of our sun) runs out of fuel and collapses under its own gravity. This collapse is so intense that it creates a singularity, which is a point of infinite density where the laws of physics as we know them break down. The singularity is surrounded by an event horizon, which is the point of no return beyond which nothing, not even light, can escape the black hole's gravity. This is what makes black holes \"black\" - they appear invisible because all the light and matter that falls into them is trapped forever."
];
var groundTruth = """
Black holes are formed when a massive star (at least eight times the mass of our sun) runs out of fuel and collapses under its own gravity. This collapse is so intense that it creates a singularity, which is a point of infinite density where the laws of physics as we know them break down. The singularity is surrounded by an event horizon, which is the point of no return beyond which nothing, not even light, can escape the black hole's gravity. This is what makes black holes "black" - they appear invisible because all the light and matter that falls into them is trapped forever.";
""";


foreach (var modelPath in modelPaths)
{
    var sw = new Stopwatch();
    sw.Start();

    Console.WriteLine(modelPath.Key.GetModelName().ToUpper());
    Log.Debug(modelPath.Key.GetModelName().ToUpper());

    var ex = ExecutorHelper.CreateExecutor(modelPath.Key);

    foreach (var answer in answers)
    {
        PrintHelper.PrintTitle(question, answer, groundTruth);

        Console.ForegroundColor = ConsoleColor.Gray;

        var metrics = new Dictionary<string, string>();

        var answerRelevancePrompt = PromptHelper.BuildAnswerRelevancePrompt(question, answer, groundTruth);
        Console.WriteLine($"Answer Relevance: ");
        var answerRelevanceResult = await ex.ExecuteWithSpinnerAsync(answerRelevancePrompt);
        metrics.Add("Relevance", answerRelevanceResult);

        var answerAccuracyPrompt = PromptHelper.BuildAnswerAccuracyPrompt(question, answer, groundTruth);
        Console.WriteLine($"Answer Accuracy: ");
        var answerAccuracyResult = await ex.ExecuteWithSpinnerAsync(answerAccuracyPrompt);
        metrics.Add("Accuracy", answerAccuracyResult);

        var answerCorrectnessPrompt = PromptHelper.BuildAnswerCorrectnessPrompt(question, answer, groundTruth);
        Console.WriteLine($"Answer Correctness: ");
        var answerCorrectnessResult = await ex.ExecuteWithSpinnerAsync(answerCorrectnessPrompt);
        metrics.Add("Correctness", answerCorrectnessResult);

        var answerCompletenessPrompt = PromptHelper.BuildAnswerCompletenessPrompt(question, answer, groundTruth);
        Console.WriteLine($"Answer Completeness: ");
        var answerCompletenessResult = await ex.ExecuteWithSpinnerAsync(answerCompletenessPrompt);
        metrics.Add("Completeness", answerCompletenessResult);

        var answerClarityPrompt = PromptHelper.BuildAnswerClarityPrompt(question, answer, groundTruth);
        Console.WriteLine($"Answer Clarity: ");
        var answerClarityResult = await ex.ExecuteWithSpinnerAsync(answerClarityPrompt);
        metrics.Add("Clarity", answerClarityResult);

        var answerDepthOfInsightPrompt = PromptHelper.BuildAnswerDepthOfInsightPrompt(question, answer, groundTruth);
        Console.WriteLine($"Answer Depth: ");
        var answerDepthOfInsightResult = await ex.ExecuteWithSpinnerAsync(answerDepthOfInsightPrompt);
        metrics.Add("Depth", answerDepthOfInsightResult);

        modelPath.Value.Add(answer, metrics);

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Elapsed time: {sw.Elapsed.TotalSeconds:#.##} seconds for {modelPath.Key.GetModelName()}");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    sw.Stop();
    elapsedTimes.Add(modelPath.Key.GetModelName(), sw.Elapsed.TotalSeconds);
    ex = null;

    NativeApi.llama_empty_call();
}


PrintHelper.PrintResults(question, groundTruth, modelPaths, elapsedTimes);
PrintHelper.PrintAnswers(answers);
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine("Done.");
