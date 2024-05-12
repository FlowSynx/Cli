﻿namespace FlowSynx.Cli.Commands.Storage.Check;

internal class CheckCommandOptions : ICommandOptions
{
    public required string SourcePath { get; set; }
    public required string DestinationPath { get; set; }
    public string? Include { get; set; }
    public string? Exclude { get; set; }
    public string? MinAge { get; set; }
    public string? MaxAge { get; set; }
    public string? MinSize { get; set; }
    public string? MaxSize { get; set; }
    public bool? CaseSensitive { get; set; } = false;
    public bool? Recurse { get; set; } = false;
    public bool? CheckSize { get; set; } = true;
    public bool? CheckHash { get; set; } = false;
    public bool? OneWay { get; set; } = false;
    public string? Address { get; set; } = string.Empty;
    public Output Output { get; set; } = Output.Json;
}