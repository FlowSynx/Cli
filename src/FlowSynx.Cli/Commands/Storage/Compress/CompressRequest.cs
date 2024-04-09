﻿namespace FlowSynx.Cli.Commands.Storage.Compress;

internal class CompressRequest
{
    public required string Path { get; set; }
    public string? Kind { get; set; } = ItemKind.FileAndDirectory.ToString();
    public string? Include { get; set; }
    public string? Exclude { get; set; }
    public string? MinAge { get; set; }
    public string? MaxAge { get; set; }
    public string? MinSize { get; set; }
    public string? MaxSize { get; set; }
    public bool? CaseSensitive { get; set; } = false;
    public bool? Recurse { get; set; } = false;
    public int? MaxResults { get; set; }
    public bool? Hashing { get; set; } = false;
    public string? CompressType { get; set; } = FlowSynx.IO.Compression.CompressType.Zip.ToString();
}