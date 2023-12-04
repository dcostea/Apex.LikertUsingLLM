# Apex.LikertUsingLLM
This app is using open source, on premises LLM like LLaMa and LLamaSharp library to measure the quality of answers given to questions considering a ground truth, or best answer.

## Prompts
Prompts to measure metrics using Likert scale (https://en.wikipedia.org/wiki/Likert_scale)

Metrics:
- Relevance
- Accuracy
- Correctness
- Completeness
- Clarity
- Depth of Insight

```
On a scale of 1 to 5, how {{metric}} is the provided Sample2 compared to the provided Sample3 in the context of the given Sample1? (1 - Not relevant, 5 - Highly relevant)
Please respond ONLY with one if these values: 1, 2, 3, 4, or 5.
Answer {{metric}}: 
```

## LLM Model (compatible with LLamaSharp) used in this demo:

https://huggingface.co/TheBloke/zephyr-7B-beta-GGUF/blob/main/zephyr-7b-beta.Q4_K_M.gguf

## Nuget libraries

- LLamaSharp.Backend.Cpu
- LLamaSharp.Backend.Cuda11 (GPU accelerated, compatible with Windows 10)
- LLamaSharp.Backend.Cuda12 (GPU accelerated, compatible with Windows 10, Windows 11)

LLamaSharp.Backend.Cuda12 is used in this demo
