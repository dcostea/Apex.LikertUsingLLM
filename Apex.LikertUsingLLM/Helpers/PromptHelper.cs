using static LLama.LLamaContext;
using System.Net.NetworkInformation;

namespace Apex.LikertUsingLLM.Helpers;

public static class PromptHelper
{
    private const string PromptTemplate = """
SAMPLE1: "{{ $question }}"
SAMPLE2: "{{ $answer }}"
SAMPLE3: "{{ $ground_truth }}"
""";

    public static string BuildAnswerRelevancePrompt(string question, string answer, string ground_truth)
    {
        string promptFormat = """
On a scale of 1 to 5, how relevant is the provided SAMPLE2 compared to the provided SAMPLE3 in the context of the given SAMPLE1? (1 - Not relevant, 5 - Highly relevant)
Relevance refers to the quality or state of being closely connected or appropriate. It’s the degree to which something is related or useful to what is happening or being talked about.
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
On a scale of 1 to 5, how accurate is the provided SAMPLE2 compared to the provided SAMPLE3 in the context of the given SAMPLE1? (1 - Not accurate, 5 - Highly accurate)
Accuracy is the degree of closeness of a measurement, calculation, or specification to its correct value or standard. It’s about precision and exactness in various contexts, such as science, engineering, statistics, and more.
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
On a scale of 1 to 5, how correct is the provided SAMPLE2 compared to the provided SAMPLE3 in the context of the given SAMPLE1? (1 - Not correct, 5 - Highly correct)
Correctness is the degree to which something is being free from error, conforming to accepted standards, and being in agreement with the true facts or a particular political or ideological orthodoxy.
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
On a scale of 1 to 5, how complete is the provided SAMPLE2 compared to the provided SAMPLE3 in the context of the given SAMPLE1? (1 - Not complete, 5 - Highly complete)
Completeness refers to the degree to which something is whole, perfect, and having nothing missing.
Please respond ONLY with one if these values: 1, 2, 3, 4, or 5.
Answer completeness: 
"""
        ;

        var prompt = PromptTemplate
        .Replace("{{ $question }}", question)
            .Replace("{{ $answer }}", answer)
            .Replace("{{ $ground_truth }}", ground_truth);

        return $"{prompt}\n{promptFormat}";
    }

    public static string BuildAnswerClarityPrompt(string question, string answer, string ground_truth)
    {
        string promptFormat = """
On a scale of 1 to 5, how clear or opaque is the provided SAMPLE2 compared to the provided SAMPLE3 in the context of the given SAMPLE1? (1 - Not clear, 5 - Highly clear)
Clarity refers to the degree to which something is clear, free from ambiguity, distinct and easy to understand.
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
On a scale of 1 to 5, how depth of insight or superficial is the provided SAMPLE2 compared to the provided SAMPLE3 in the context of the given SAMPLE1? (1 - Superficial, 5 - Deep insights)
Deep of insights refers to having profound understanding an individual or organization gains from analyzing information on a particular issue, or the opposite, being superficial.
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
