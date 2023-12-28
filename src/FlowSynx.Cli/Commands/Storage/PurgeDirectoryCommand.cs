﻿using System.CommandLine;
using FlowSynx.Abstractions;
using FlowSynx.Environment;
using FlowSynx.Net;
using EnsureThat;
using FlowSynx.Cli.Formatter;

namespace FlowSynx.Cli.Commands.Storage;

internal class PurgeDirectoryCommand : BaseCommand<PurgeDirectoryCommandOptions, PurgeDirectoryCommandOptionsHandler>
{
    public PurgeDirectoryCommand() : base("purge", "List of entities regarding specific path")
    {
        var pathOption = new Option<string>(new[] { "-p", "--path" }, "The path to get about") { IsRequired = true };

        AddOption(pathOption);
    }
}

internal class PurgeDirectoryCommandOptions : ICommandOptions
{
    public string Path { get; set; } = string.Empty;
}

internal class PurgeDirectoryCommandOptionsHandler : ICommandOptionsHandler<PurgeDirectoryCommandOptions>
{
    private readonly IOutputFormatter _outputFormatter;
    private readonly ISpinner _spinner;
    private readonly IEndpoint _endpoint;
    private readonly IHttpRequestService _httpRequestService;

    public PurgeDirectoryCommandOptionsHandler(IOutputFormatter outputFormatter, ISpinner spinner,
        IEndpoint endpoint, IHttpRequestService httpRequestService)
    {
        EnsureArg.IsNotNull(outputFormatter, nameof(outputFormatter));
        EnsureArg.IsNotNull(endpoint, nameof(endpoint));
        EnsureArg.IsNotNull(httpRequestService, nameof(httpRequestService));
        _outputFormatter = outputFormatter;
        _spinner = spinner;
        _endpoint = endpoint;
        _httpRequestService = httpRequestService;
    }

    public async Task<int> HandleAsync(PurgeDirectoryCommandOptions options, CancellationToken cancellationToken)
    {
        await _spinner.DisplayLineSpinnerAsync(async () => await CallApi(options, cancellationToken));
        return 0;
    }

    private async Task CallApi(PurgeDirectoryCommandOptions options, CancellationToken cancellationToken)
    {
        try
        {
            const string relativeUrl = "storage/purge";
            var result = await _httpRequestService.DeleteAsync<PurgeDirectoryCommandOptions, Result<object?>>($"{_endpoint.GetDefaultHttpEndpoint()}/{relativeUrl}", options, cancellationToken);

            if (!result.Succeeded)
                _outputFormatter.WriteError(result.Messages);
            else
            {
                _outputFormatter.Write(result.Data ?? result.Messages);
            }
        }
        catch (Exception ex)
        {
            _outputFormatter.WriteError(ex.Message);
        }
    }
}