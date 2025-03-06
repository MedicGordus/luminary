namespace luminary.mapping;

public struct FlowType
{
    /// <summary>
    /// goto( <a> )
    /// 
    /// a == mapper index to goto
    /// 
    /// Nothing special, go to mapper at this index.
    /// </summary>
    public const string GOTO = "goto";

    /// <summary>
    /// If( <a> , <b> , <c> )
    /// 
    /// a ==  <input property name>
    /// b == mapper index to goto if a == true
    /// c == mapper index to goto if a == false
    /// 
    /// If a == true, b will be executed, otherwise c.
    /// </summary>
    public const string IF = "if";

    /// <summary>
    /// while( <a> , <b> )
    /// 
    /// a == <input property name>
    /// b == mapper index to goto while a == true
    /// 
    /// Continually calls b while a is true.
    /// </summary>
    public const string WHILE = "while";

    /// <summary>
    /// for( <a> , <b>, <c> )
    /// 
    /// a ==  <input property name>
    /// b == ulong start
    /// c == ulong end
    /// 
    /// Loops from b thru c using a as the iterator.
    /// </summary>
    public const string FOR = "for";

    /// <summary>
    /// foreach( <a> , <b> , <c> )
    /// 
    /// a == <input property name that is an array>
    /// b == <array type>
    /// c == mapper index to call for each element of a 
    /// 
    /// Calls c while looping thru a, array holding type b.
    /// </summary>
    public const string FOR_EACH = "foreach";
}

public class MappingFlow
{
    public static readonly Dictionary<string, Func<Mapper, Mapper>> Flows = new() {
        { FlowType.GOTO , },
        { FlowType.IF , },
        { FlowType.WHILE , },
        { FlowType.FOR , },
        { FlowType.FOR_EACH , },
    };

    public Dictionary<ulong, Mapper> MappersByIndex;

    public MappingFlow(Dictionary<ulong, Mapper> _mappersByIndex)
    {
        MappersByIndex = _mappersByIndex;
    }

    public Prism ExecuteIf()
    {}

    public void ExecuteWhile()
    {}

    public void ExecuteForLoop()
    {}

    public void ExecuteForEachLoop()
    {}
}

// everything below is from grok for ideas

public class HttpDataClient
{
    private readonly HttpClient HttpClient;
    
    public HttpDataClient(HttpClient _httpClient)
    {
        HttpClient = _httpClient;
    }

    public async Task<Mapper> FetchDataAsync(Mapper _input)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "your-endpoint");
        
        // Configure request based on input
        foreach (var prop in _input.Properties)
        {
            request.Headers.TryAddWithoutValidation(prop.Key, prop.Value);
        }

        var response = await HttpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        
        // Transform response into CustomObject
        return new Mapper
        {
            Properties = new Dictionary<string, string>
            {
                { "Response", content }
                // Add more properties as needed
            }
        };
    }
}

public class WorkflowOrchestrator
{
    private readonly List<Func<Mapper, Mapper>> Steps;
    
    public WorkflowOrchestrator()
    {
        Steps = new List<Func<Mapper, Mapper>>();
    }

    // Add a processing step
    public WorkflowOrchestrator AddStep(Mapper _processor)
    {
        Steps.Add(_input => _processor.Process(_input));
        return this;
    }

    // Add HTTP client step
    public WorkflowOrchestrator AddHttpStep(HttpDataClient _client)
    {
        Steps.Add(_input => _client.FetchDataAsync(_input).GetAwaiter().GetResult());
        return this;
    }

    // Add conditional step
    public WorkflowOrchestrator AddIf(Func<Mapper, bool> _condition, Mapper _ifTrueProcessor, Mapper _ifFalseProcessor)
    {
        Steps.Add(input => 
            _condition(input) 
                ? _ifTrueProcessor.Process(input) 
                : _ifFalseProcessor.Process(input));
        return this;
    }

    // Add while loop step
    public WorkflowOrchestrator AddWhile(Func<Mapper, bool> _condition, Mapper _processor)
    {
        Steps.Add(
            _input =>
            {
                var result = _input;
                while (_condition(result))
                {
                    result = _processor.Process(result);
                }
                return result;
            }
        );
        return this;
    }

    // Add for loop step
    public WorkflowOrchestrator AddFor(int _start, int _end, Func<Mapper, int, Mapper> _processor)
    {
        Steps.Add(
            _input =>
            {
                var result = _input;
                for (int i = _start; i < _end; i++)
                {
                    result = _processor(result, i);
                }
                return result;
            }
        );
        return this;
    }

    // Execute the workflow
    public Mapper Execute(Mapper _input)
    {
        var result = _input;
        foreach (var step in Steps)
        {
            result = step(result);
        }
        return result;
    }
}