using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RFC.Models;
using RFC.conndb;

namespace RFC.Controllers;

public class EmployeeControl : Controller
{
    Transfer _tranfer = new Transfer();


    public IActionResult List()
    {  
        return View();
    }

    [HttpPost]
    public IActionResult List(string id, string rfc, string name, string lastName, string bornDate, string Status)
    {   
        
        //Console.WriteLine(employee.ID + " - " + employee.Name + " - " + employee.LastName + " - " + employee.BornDate + " - " + employee.Status);
        var listemployee = _tranfer.Employeelist(id, rfc, name, lastName, bornDate, Status);

        return View(listemployee);
    }

    public IActionResult Save()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Save(EmployeeModel employee)
    {
        //Console.Write("control" + employee.Name + ", " + employee.LastName + ", " + employee.RFC + ", " + employee.BornDate + ", " + employee.Status);

        if(!ModelState.IsValid)
            return View();
        
        var resp = _tranfer.Save(employee);

        /*if(resp)
            alert('@alertMessage');
        else*/
        return View();
    }

}