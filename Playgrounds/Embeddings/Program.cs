﻿using Forge.OpenAI;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Embeddings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Embeddings
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            // This example demonstrates, how you can use embedding feature of OpenAI.
            // This feature is useful for search, clustering, recommendations, anomaly detection, etc
            // More information: https://platform.openai.com/docs/guides/embeddings/what-are-embeddings
            //
            // The very first step to create an account at OpenAI: https://platform.openai.com/
            // Using the loggedIn account, navigate to https://platform.openai.com/account/api-keys
            // Here you can create apiKey(s)

            using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((builder, services) =>
            {
                services.AddForgeOpenAI(options => {
                    options.AuthenticationInfo = builder.Configuration["OpenAI:ApiKey"]!;
                });
            })
            .Build();

            IOpenAIService openAi = host.Services.GetService<IOpenAIService>()!;

            EmbeddingsRequest request = new EmbeddingsRequest();
            request.InputTextsForEmbeddings.Add("The food was delicious and the waiter...");

            HttpOperationResult<EmbeddingsResponse> response = 
                await openAi.EmbeddingsService
                    .GetAsync(request, CancellationToken.None)
                        .ConfigureAwait(false);

            if (response.IsSuccess)
            {
                Console.WriteLine(response.Result!);
            }
            else
            {
                Console.WriteLine(response);
            }

        }

    }

}