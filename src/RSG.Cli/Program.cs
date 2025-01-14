using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RSG.Extensions;
using RSG.Services;
using RSG.Strings;
using Sweaj.Patterns.Dates;

var builder = CoconaApp.CreateBuilder();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSingleton<IRandomProvider<int>, SystemRandomProvider>();
builder.Services.AddSingleton<RandomStringGenerator>();
builder.Services.AddSingleton<IDateTimeProvider, SystemTimeProvider>();

var app = builder.Build();

// Command to generate a string
app.AddCommand("string", static async (
    [Option('l')] int? length,
    [Option('i')] int? iterations,
    [Option('c')] string? characters,
    [Option('s')] bool? stats,
    [Cocona.FromService] RandomStringGenerator service, [FromService] IDateTimeProvider dateTimeProvider) =>
{
    // Set default values for the parameters if they are not provided
    length ??= 10; // Default length 10
    iterations ??= 1; // Default iterations 1
    characters ??= CharacterSetHolder.Default.Flatten(); // Default character set
    stats ??= false; // Default to not showing stats


    var randomMethod = RandomizationMethod.Psuedorandom;
    var req = new StringRequest(iterations.Value, length.Value, characters, randomMethod, stats.Value, null);
    var cts = new CancellationTokenSource();

    var result = await service.GenerateAsync(req, cts.Token);

    Console.WriteLine("Result: Completed Successfully: {0}", result.OperationStatus.IsCompletedSuccessfully);
    Console.WriteLine("Result: Cancelled?: {0}", result.OperationStatus.IsCancelled);
    Console.WriteLine("Generate Duration: {0}", result.OperationStatus.Duration);
    
    var startTime = dateTimeProvider.Now();
    
    foreach (var str in result.Strings)
    {
        Console.WriteLine(str);
    }

    var endTime = dateTimeProvider.Now();

    Console.WriteLine("Printing Duration: {0}", endTime - startTime);
    if (result.StringStatistics is not null)
    {
        Console.WriteLine("Permutations: {0}", result.StringStatistics.Permutations.ToString("n0"));
        Console.WriteLine("Probability of string occuring: 1 / {0}", result.StringStatistics.Permutations.ToString("n0"));
        foreach(var cf in result.StringStatistics.CharacterFrequencies)
        {
            Console.WriteLine(cf.ToString());
        }       
    }
});


await app.RunAsync();
