using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RFC.Models;

namespace RFC.conndb;

public class Connection
{
    public string SQLSTRING = string.Empty;

    public Connection(){

        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        
        SQLSTRING = builder.GetSection("ConnectionString:stringSQL").Value;
        //Console.WriteLine("-----" + SQLSTRING);

    }

    public string getstringsql(){
        return SQLSTRING;
    }

}

