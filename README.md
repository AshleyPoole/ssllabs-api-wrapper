SSLLWrapper
===========

SSLLWrapper stands for SSL Labs Wrapper which is the first publicly available .NET wrapper developed for the [SSL Labs' Assessment API's](https://github.com/ssllabs/ssllabs-scan/blob/master/ssllabs-api-docs.md) that allow the consumer to test SSL servers on the public internet.

This wrapper easies the communication to the API's for .NET developers which allows you as the developer to focus on your project rather than managing the plumbing and overhead required to consume the API's.

**Notes**
- SSL Labs' Assessment API's are currently still in development and are subject to change. This therefore may impact the wrapper though updates will be provided.
- The wrapper does **NOT** use web scrapping like other wrappers which don't use the assessment API's.

### NuGet Package
The wrapper can easily be imported into your project using the [SSLLWrapper NuGet package](https://www.nuget.org/packages/SSLLWrapper/). The NuGet install command for this package is:

**PM> Install-Package SSLLWrapper**

### Wrapper Usage
When creating a new instance of SSLLWrapper you must supply the API url during the initialization. For example in C# this would be expressed as the following: 
```C#
var ssllService = new SSLLWrapper.SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4");

// Or if you use the SSLWrapper namespace this can be shorten to
var ssllService = new SSLLService("https://api.dev.ssllabs.com/api/fa78d5a4");
```
#### Methods

Below are the method signatures of the SSLLWrapper Service.

##### Info()

The Info method is used to determine if the API is online and returns an [Info response object](https://github.com/AshleyPoole/SSLLWrapper/blob/master/README.md#info-1). No input parameters are taken.

```C#
public Info Info()
```

##### Analyze()

The Analyze method is used to initiate an assessment or retrieve results. The results may only be partial so see SSL Labs documentation for more information as as GetEndpointDetails call may be needed to view the whole result set.

```C#
public Analyze Analyze(string host, Publish publish, ClearCache clearCache, FromCache fromCache, All all)
```

The wrapper also contains an overloaded Analyze method which only requires the host parameter. Internal is uses the following parameter options - Publish.Off, ClearCache.On, FromCache.Ignore, All.On.
```C#
public Analyze Analyze(string host)
```

##### AutomaticAnalyze()

The Analyze method is used to initiate and wait for an assessment to complete before retrieving results. Compared to the normal Analyze() method this method keeps checking the Api and only when a scan has finished does it return. This saves the comsumer from having to write their own logic for handling an assessment in progress.
Another call to GetEndpointDetails() may be needed to view the whole result set for a given endpoint.

```C#
public Analyze AutomaticAnalyze(string host, Publish publish, ClearCache clearCache, FromCache fromCache, All all)
```

The wrapper also contains an overloaded AutomaticAnalyze method which only requires the host parameter. Internal is uses the following parameter options - Publish.Off, ClearCache.On, FromCache.Ignore, All.On.
```C#
public Analyze AutomaticAnalyze(string host)
```

##### GetEndpointData()

The GetEndpointData method is used to retrieve a fully results set.
```C#
public Endpoint GetEndpointData(string host, string s, FromCache fromCache)
```

The wrapper also contains an overloaded GetEndpointData method which only requires the host and s parameter. Internal is uses FromCache.Off.
```C#
public Endpoint GetEndpointData(string host, string s)
```

##### GetStatusCodes()

The GetStatusCodes method is use to retrieve a list of status codes and messages.
```C#
public StatusCodes GetStatusCodes()
```

#### Response Objects

All the response objects are static .NET objects which are populated from the SSL Labs Assessment API's result.  Due to the response models are based on those derived from the API itself I will only provide a top level response model map. For more information on the properties available check out [their documentation](https://github.com/ssllabs/ssllabs-scan/blob/master/ssllabs-api-docs.md#response-objects).

All response objects also inherit from a custom BaseModel to extent the usability to you as the developer, as well as core functionality needed to consume the API's. The properties are listed below for all top level or custom response objects.

A property may be NULL or 0 if the field was NULL or not listed in the API's response.

##### BaseModel
```C#
public Header Header { get; set; }
public bool HasErrorOccurred { get; set; }
public List<Error> Errors { get; set; }

public class Header
{
  public int statusCode { get; set; }
  public string statusDescription { get; set; }
}

public class Error
{
	public string field { get; set; }
	public string message { get; set; }
}
```

##### Info
```C#
public string engineVersion { get; set; }
public string criteriaVersion { get; set; }
public int clientMaxAssessments { get; set; }
public string notice { get; set; }
public bool Online { get; set; }
```

##### Analyze
```C#
public string host { get; set; }
public int port { get; set; }
public string protocol { get; set; }
public bool isPublic { get; set; }
public string status { get; set; }
public long startTime { get; set; }
public string engineVersion { get; set; }
public string criteriaVersion { get; set; }
public List<Endpoint> endpoints { get; set; }
```

##### Endpoint
```C#
public string ipAddress { get; set; }
public string statusMessage { get; set; }
public string statusDetails { get; set; }
public string statusDetailsMessage { get; set; }
public int progress { get; set; }
public int eta { get; set; }
public int delegation { get; set; }
public int duration { get; set; }
public string grade { get; set; }
public bool hasWarnings { get; set; }
public bool isExceptional { get; set; }
public Details Details { get; set; }
```

##### StatusCodes
```C#
public StatusDetails StatusDetails { get; set; }
```

#### Custom Parameters
Custom parameters are as follows:

```C#
public enum Publish
{
    On,
    Off
}

public enum ClearCache
{
    On,
    Off,
	Ignore
}

public enum FromCache
{
    On,
    Off,
	Ignore
}

public enum All
{
    On,
    Done
}
```

### Author
Ashley Poole - www.ashleypoole.co.uk.

[SSLWrapper project's home page](http://www.ashleypoole.co.uk/ssllwrapper?utm_source=github&utm_medium=githubproject&utm_campaign=ssllwrapper)

Please contact me if you have any questions, issues or recommendations either via [my website](http://www.ashleypoole.co.uk), [Twitter](http://twitter.com/geekypants92) or [by email](mailto:git@ashleypoole.co.uk).


