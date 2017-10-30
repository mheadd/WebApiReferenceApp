# Web API Reference Application

A simple .NET Core Web API application to demonstrate how to connect to various backend data sources (SOAP, REST, RDBMS, etc.)

## Connecting to a SOAP Service

This example uses the [`SoapHttpClient`](https://www.nuget.org/packages/SoapHttpClient/) and borrows heavily from [that project's documentation](https://github.com/pmorelli92/SoapHttpClient). It shows how to connect to NASA's Heliocentric Trajectories Web Services and uses the [`Newtonsoft.Json` package](https://www.nuget.org/packages/Newtonsoft.Json/) to convert the XML SOAP response to JSON format.

## Connecting to a REST Service

This example uses the [REST APIs](https://www.data.gov/developers/apis) in the Data.gov data portal and makes calls using the native C# [`HttpClient` Class](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.110).aspx). It shows how to retrieve JSON content from the Data.gov APIs and parse it the [`Newtonsoft.Json` package](https://www.nuget.org/packages/Newtonsoft.Json/) to return a fragment of the JSON.

## Connecting to a RDBMS

Set up Docker container with SQL Server:

```bash
~$ docker pull microsoft/mssql-server-linux:2017-latest

~$ sudo docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<YourStrong!Passw0rd>' \
   -p 1401:1433 --name sqlserver \
   -d microsoft/mssql-server-linux:2017-latest
```

Connect to the Docker instance and use `sqlcmd` to create a DB and populate it with test data.

```bash
sudo docker exec -it sqlserver "bash"

/# sqlcmd -S localhost -U SA -P <YourStrong!Passw0rd>

1> CREATE DATABASE TestDB
2> GO
1> USE TestDB
2> CREATE TABLE dbo.people(id INT NOT NULL PRIMARY KEY, name [VARCHAR](64), enabled [VARCHAR](16));
3> GO
1> INSERT INTO people([id],[name],[enabled]) VALUES (1,N'Waldo',N'True'), (2,N'Paul',N'True'), (3,N'Devin',N'True'), (4,N'Thor',N'False');
2> GO
1> quit
```

This can all be [encapsulated](https://github.com/mheadd/WebApiReferenceApp/issues/1) in a `Docker` file.
