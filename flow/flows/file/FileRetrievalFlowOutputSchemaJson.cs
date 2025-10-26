using System.Text.Json.Serialization;
using luminary.mapping.functions;
using luminary.util;

namespace luminary.flow;

public class FileRetrievalFlowOutputSchemaJson
{

    /// <summary>
    /// The full contents of the requested file.
    /// </summary>
    [JsonPropertyName(FILE_CONTENTS_UTF8_JSON_NAME)]
    public string? FileContentsUtf8 { get; set; }
    public const string FILE_CONTENTS_UTF8_JSON_NAME = "file-contents-utf8";

    /// <summary>
    /// Date time when the file was originally created.
    /// </summary>
    [JsonPropertyName(FILE_CREATION_DATETIME_JSON_NAME)]
    public string? FileCreationDatetime { get; set; }
    public const string FILE_CREATION_DATETIME_JSON_NAME = "file-creation-datetime";
    
    /// <summary>
    /// Date time when the file was last modified.
    /// </summary>
    [JsonPropertyName(FILE_LAST_MODIFIED_DATETIME_JSON_NAME)]
    public string? FileLastModifiedDatetime { get; set; }
    public const string FILE_LAST_MODIFIED_DATETIME_JSON_NAME = "file-last-modified-datetime";

    public PrismOperator BuildOperator()
    {
        if(!DateTimeOffset.TryParse(FileCreationDatetime, out DateTimeOffset creationDatetimeOffset))
        {
            throw new Exception($"Could not parse datetime '{FileCreationDatetime}' to a datetime offset object.");
        }
        if(!DateTimeOffset.TryParse(FileLastModifiedDatetime, out DateTimeOffset lastmodifiedDatetimeOffset))
        {
            throw new Exception($"Could not parse datetime '{FileLastModifiedDatetime}' to a datetime offset object.");
        }

        return new PrismOperator(
            new()
            {
                { FILE_CONTENTS_UTF8_JSON_NAME, new StringOperator(FileContentsUtf8) },
                { FILE_CREATION_DATETIME_JSON_NAME, new DateTimeOffsetOperator(creationDatetimeOffset) },
                { FILE_LAST_MODIFIED_DATETIME_JSON_NAME, new DateTimeOffsetOperator(lastmodifiedDatetimeOffset) }
            }
        );
    }

    public static async Task<Panicable<FileRetrievalFlowOutputSchemaJson>> BuildFromPathAsync(string _path)
    {
        Panicable<FileRetrievalFlowOutputSchemaJson> output = new();
        try
        {
            FileInfo fileInfo = new FileInfo(_path);

            // gather the file's creation date/time
            DateTimeOffset creationTime = fileInfo.CreationTime;

            // gather  the file's last modified date/time
            DateTimeOffset lastModifiedTime = fileInfo.LastWriteTime;

            string? fileContents = await File.ReadAllTextAsync(_path);

            output.ReturnValue = new()
            {
                FileContentsUtf8 = fileContents,
                FileCreationDatetime = creationTime.ToString(),
                FileLastModifiedDatetime = lastModifiedTime.ToString()
            };
        }
        catch (Exception e)
        {
            output.ActivatePanic(e);
        }

        return output;
    }

}