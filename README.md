SSLLWrapper
===========

SSLLWrapper stands for SSL Labs Wrapper which I believe is the first .NET wrapper developed for the new [SSL Labs' Assessment API's](https://github.com/ssllabs/ssllabs-scan/blob/master/ssllabs-api-docs.md) that allow the consumer to test SSL servers on the public internet.

This wrapper easies the communication to the API's for .NET developers which allows you as the developer to focus on your project rather than managing the plumbing and overhead required to consume the API's.

**Notes**
- SSL Labs' Assessment API's are currently still in development and are subject to change.
- The wrapper is currently still in development aswell though is in a functional beta stage.
- The wrapper will be available as NuGet package shortly.
- The wrapper does **NOT** use web scrapping like other wrappers which don't use the assesment API's.

## Wrapper Usage

### Methods
**Info()**

**Anaylze()**

**GetEndpointDetails()**

**GetStatusCodes()**

### Response Objects
**Info**

**Anaylze**

**Endpoint**

**StatusDetails**

## To Do
- Flesh out SSLWrapper.Tests project to ensure as most code as appropiate is tesed
- General refractor 

## Author
Ashley Poole - www.ashleypoole.co.uk.

Please contact me if you have any questions, issues or recommendations either via [my website](http://www.ashleypoole.co.uk), [Twitter](http://twitter.com/geekypants92) or [by email](mailto:git@ashleypoole.co.uk).
