# Web API Reference Application

A simple .NET Core Web API application to demonstrate how to connect to various backend data sources (SOAP, REST, RDBMS, etc.)

## Connecting to a SOAP Service

This example uses the [`SoapHttpClient`](https://www.nuget.org/packages/SoapHttpClient/) and borrows heavily from [that project's documentation](https://github.com/pmorelli92/SoapHttpClient). It shows how to connect to NASA's Heliocentric Trajectories Web Services and uses the [`Newtonsoft.Json` package](https://www.nuget.org/packages/Newtonsoft.Json/) to convert the XML SOAP response to JSON format.

## Connecting to a REST Service

This example uses the [REST APIs]()https://www.data.gov/developers/apis in the Data.gov data portal and makes calls using the native C# [`HttpClient` Class](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.110).aspx). It shows how to retrieve JSON content from the Data.gov APIs and parse it the [`Newtonsoft.Json` package](https://www.nuget.org/packages/Newtonsoft.Json/) to return a fragment of the JSON.

## Connecting to a RDBMS

Coming soon.

