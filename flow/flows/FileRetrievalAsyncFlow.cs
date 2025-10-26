using luminary.mapping;
using luminary.mapping.functions;
using luminary.util;

namespace luminary.flow;


public class FileRetrievalAsyncFlow : AsyncFlow
{


    public static readonly SchemaJson FileFlowInputSchema = new()
    {
        Type = SchemaJson.JsonTypes.OBJECT,
        Properties = new()
        {
            {
                FileRetrievalFlowInputSchemaJson.FILE_PATH_JSON_NAME, new SchemaJson ()
                {
                    Title = "The full path of the requested file.",
                    Type = SchemaJson.JsonTypes.STRING
                }
            }
        }
    };

    public static readonly SchemaJson FileFlowOutputSchema = new()
    {
        Type = SchemaJson.JsonTypes.OBJECT,
        Properties = new ()
        {
            {
                FileRetrievalFlowOutputSchemaJson.FILE_CONTENTS_UTF8_JSON_NAME, new SchemaJson ()
                {
                    Title = "The full contents of the requested file.",
                    Type = SchemaJson.JsonTypes.STRING
                }
            },
            {
                FileRetrievalFlowOutputSchemaJson.FILE_CREATION_DATETIME_JSON_NAME, new SchemaJson ()
                {
                    Title = "Date time when the file was originally created.",
                    Type = SchemaJson.JsonTypes.STRING,
                    Format = OperatorValue.OperatorValueTypeFormat.DateTimeOffset
                }
            },
            {
                FileRetrievalFlowOutputSchemaJson.FILE_LAST_MODIFIED_DATETIME_JSON_NAME, new SchemaJson ()
                {
                    Title = "Date time when the file was last modified.",
                    Type = SchemaJson.JsonTypes.STRING,
                    Format = OperatorValue.OperatorValueTypeFormat.DateTimeOffset
                }
            }
        }
    };
    public override async Task<Panicable<Prism?>> ProcessAsync(Prism _input)
    {
        var output = new Panicable<Prism?>();

        try
        {
            var input = FileRetrievalFlowInputSchemaJson.BuildFromPrism(_input);

            var fileResponse = await FileRetrievalFlowOutputSchemaJson.BuildFromPathAsync(
                input.Path ?? throw new Exception("Cannot retrieve a file from a null path.")
            ).ConfigureAwait(false);

            if (fileResponse.Paniced)
            {
                output.ActivatePanic(fileResponse.GetException());
            }
            else if (fileResponse.ReturnValue == null)
            {
                output.ActivatePanic(
                    new Exception("Could not retrieve file, the build of the file object unexpectedly returned null.")
                );
            }
            else
            {
                output.ReturnValue = new Prism(fileResponse.ReturnValue.BuildOperator(), FileFlowOutputSchema);
            }
        }
        catch (Exception e)
        {
            output.ActivatePanic(e);
        }

        return output;
    }
}