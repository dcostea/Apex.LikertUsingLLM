namespace Apex.LikertUsingLLM.Helpers;

public static class PromptHelper
{
    private const string PromptTemplate = """
Sample1: "{{ $question }}"
Sample2: "{{ $answer }}"
Sample3: "{{ $ground_truth }}"
""";

    public static string BuildAnswerRelevancePrompt(string question, string answer, string ground_truth)
    {
        string promptFormat = """
On a scale of 1 to 5, how relevant is the provided Sample2 compared to the provided Sample3 in the context of the given Sample1? (1 - Not relevant, 5 - Highly relevant)
Relevancy refers to the degree to which something is related or useful to what is happening or being talked about.
Please respond ONLY with one if these values: 1, 2, 3, 4, or 5.
Answer relevance: 
""";

        var prompt = PromptTemplate
            .Replace("{{ $question }}", question)
            .Replace("{{ $answer }}", answer)
            .Replace("{{ $ground_truth }}", ground_truth);

        return $"{prompt}\n{promptFormat}";
    }

    public static string BuildAnswerAccuracyPrompt(string question, string answer, string ground_truth)
    {
        string promptFormat = """
On a scale of 1 to 5, how accurate is the provided Sample2 compared to the provided Sample3 in the context of the given Sample1? (1 - Not accurate, 5 - Highly accurate)
Accuracy refers to the degree to which something is correct or precise.
Please respond ONLY with one if these values: 1, 2, 3, 4, or 5.
Answer accuracy: 
""";

        var prompt = PromptTemplate
            .Replace("{{ $question }}", question)
            .Replace("{{ $answer }}", answer)
            .Replace("{{ $ground_truth }}", ground_truth);

        return $"{prompt}\n{promptFormat}";
    }

    public static string BuildAnswerCorrectnessPrompt(string question, string answer, string ground_truth)
    {
        string promptFormat = """
On a scale of 1 to 5, how correct is the provided Sample2 compared to the provided Sample3 in the context of the given Sample1? (1 - Not correct, 5 - Highly correct)
Correctness refers to the degree to which something is accurate or precise.
Please respond ONLY with one if these values: 1, 2, 3, 4, or 5.
Answer correctness: 
""";

        var prompt = PromptTemplate
            .Replace("{{ $question }}", question)
            .Replace("{{ $answer }}", answer)
            .Replace("{{ $ground_truth }}", ground_truth);

        return $"{prompt}\n{promptFormat}";
    }

    public static string BuildAnswerCompletenessPrompt(string question, string answer, string ground_truth)
    {
        string promptFormat = """
On a scale of 1 to 5, how complete is the provided Sample2 compared to the provided Sample3 in the context of the given Sample1? (1 - Not complete, 5 - Highly complete)
Completeness refers to the degree to which something is whole, perfect, and having nothing missing.
Please respond ONLY with one if these values: 1, 2, 3, 4, or 5.
Answer completeness: 
""";

        var prompt = PromptTemplate
            .Replace("{{ $question }}", question)
            .Replace("{{ $answer }}", answer)
            .Replace("{{ $ground_truth }}", ground_truth);

        return $"{prompt}\n{promptFormat}";
    }

    public static string BuildAnswerClarityPrompt(string question, string answer, string ground_truth)
    {
        string promptFormat = """
On a scale of 1 to 5, how clear or opaque is the provided Sample2 compared to the provided Sample3 in the context of the given Sample1? (1 - Not clear, 5 - Highly clear)
Clarity refers to the degree to which something is easy to understand or is it opaque.
Please respond ONLY with one if these values: 1, 2, 3, 4, or 5.
Answer clarity: 
""";

        var prompt = PromptTemplate
            .Replace("{{ $question }}", question)
            .Replace("{{ $answer }}", answer)
            .Replace("{{ $ground_truth }}", ground_truth);

        return $"{prompt}\n{promptFormat}";
    }

    public static string BuildAnswerDepthOfInsightPrompt(string question, string answer, string ground_truth)
    {
        string promptFormat = """
On a scale of 1 to 5, how depth of insight or superficial is the provided Sample2 compared to the provided Sample3 in the context of the given Sample1? (1 - Superficial, 5 - Deep insights)
Deep of insights refers to if it does offer deep insights or is it superficial.
Please respond ONLY with one if these values: 1, 2, 3, 4, or 5.
Answer depth of insight: 
""";

        var prompt = PromptTemplate
            .Replace("{{ $question }}", question)
            .Replace("{{ $answer }}", answer)
            .Replace("{{ $ground_truth }}", ground_truth);

        return $"{prompt}\n{promptFormat}";
    }
}
