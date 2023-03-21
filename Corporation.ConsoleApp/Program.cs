﻿using Corporation.Core.Entities;
using Corporation.Infrastructure.DbContextSim;
using Corporation.Infrastructure.Services;
using Corporation.Infrastructure.Utilities;
using Corporation.Infrastructure.Utilities.Exceptions;

CompanyServices newCompany = new CompanyServices();
DepartmentServices newDepartment = new DepartmentServices();
EmployeeServices newEmployee = new EmployeeServices();

while (true)
{
    Console.WriteLine("WELCOME! Please select your option");
    Console.WriteLine("0 - Exit \n1 - To Create a Company \n" +
        "2 - Get list of Companies \n" +
        "3 - Create Department \n" +
        "4 - Get list of Departments by Company ID \n" +
        "5 - Get list of Departments by Company Name \n" +
        "6 - Update Department \n" +
        "7 - Add Employee to Companies Department \n" +
        "8 - Get List of All Employees \n" +
        "9 - Get List of Employees by Department Id");
    int menuItems;
    string? userRes = Console.ReadLine();
    bool response = int.TryParse(userRes, out menuItems);

    if (response)
    {
        switch (menuItems)
        {
            case (int)Helper.ConsoleMenu.Exit:
                Environment.Exit(0);
                break;
            case (int)Helper.ConsoleMenu.CreateCompany: // SEEMS OK, FURTHER INSPECTION SHOULD BE DONE
                try
                {
                    Console.WriteLine("Enter Company name: ");
                    string companyName = Console.ReadLine();
                    newCompany.Create(companyName);
                    Console.WriteLine($"\nCongratulations {companyName} has been created!\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Helper.ConsoleMenu.GetCompanyList: // SEEMS OK, FURTHER INSPECTION SHOULD BE DONE
                newCompany.GetAll();
                break;
            case (int)Helper.ConsoleMenu.CreateDepartment:
            dep_name:
                Console.WriteLine("Enter Department Name");
                string departmentName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(departmentName) || departmentName.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(departmentName, "[^a-zA-Z0-9 -]"))
                {
                    Console.WriteLine("Department name can not be left blank, contain only integer and/or contain symbols");
                    goto dep_name;
                }
            limit_range: // RENAME
                Console.WriteLine("Enter Employee Limit");
                int employeeLimit;
                string employeeLimitResponse = Console.ReadLine();
                bool employeeInLimitRange = int.TryParse(employeeLimitResponse, out employeeLimit);
                if (!employeeInLimitRange)
                {
                    Console.WriteLine("The limit range can only contain integer");
                    goto limit_range;
                }
                else if (employeeLimit <= 0)
                {
                    Console.WriteLine("Employee limit can not be set to zero or negative");
                    goto limit_range;
                }
            company_Id:
                Console.WriteLine("Enter Company ID department belongs to: ");
                newCompany.GetAll();
                int departmentCompanyId;
                string depCompIdResponse = Console.ReadLine();
                if (!int.TryParse(depCompIdResponse, out departmentCompanyId))
                {
                    Console.WriteLine("Incorrect format please enter an integer corresponding to your Companies unique ID");
                    goto company_Id;
                }
            company_Name:
                Console.WriteLine("Enter Company name department belongs to: ");
                string company_name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(company_name) || company_name.All(char.IsDigit))
                {
                    Console.WriteLine("This section may not be left blank and/or Company name may not consist of only integers");
                    goto company_Name;
                }
                newDepartment.Create(departmentName, employeeLimit, departmentCompanyId, company_name);
                break;

            case (int)Helper.ConsoleMenu.GetListOfDepartmentsByID:
            listing_departments_byId:
                Console.WriteLine("Enter the Unique ID of your Company to list all your departments");
                newCompany.GetAll();
                int listById;
                string listByIdResponse = Console.ReadLine();
                if (!int.TryParse(listByIdResponse, out listById))
                {
                    Console.WriteLine("Incorrect format please enter an integer corresponding to your Companies unique ID");
                    goto listing_departments_byId;
                }
                newCompany.GetAllDepartmentsByID(listById);
                break;

            case (int)Helper.ConsoleMenu.GetListOfDepartmentsByName:
            lisiting_departments_byName:
                Console.WriteLine("Enter the name of your Company");
                newCompany.GetAll();
                string listByNameResponse = Console.ReadLine();
                newCompany.GetAllDepartmentsByName(listByNameResponse);
                break;
            default:
                Console.WriteLine("Please select a valid option from the provided menu.");
                break;

            case (int)Helper.ConsoleMenu.UpdateDepartment: // NOT FINISHED

            company_response:
                Console.WriteLine("Please input the name of your Company to modify department parameters");
                string companyResponse = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(companyResponse) || companyResponse.All(char.IsDigit))
                {
                    Console.WriteLine("Please enter a");
                }
                newCompany.GetAllDepartmentsByName(companyResponse);
                Console.WriteLine("Enter Department ID you wish to modify");
                int updateByName;
                string updateResponse = Console.ReadLine();
                if (!int.TryParse(updateResponse, out updateByName))
                {
                    Console.WriteLine("Enter correct format");
                }
                Console.WriteLine("Enter new Name for your Department");
                string newDepName = Console.ReadLine();
                Console.WriteLine("Enter new emp limit");
                int newEmpLimit = int.Parse(Console.ReadLine());
                newDepartment.Update(updateByName, newDepName, newEmpLimit);
                break;

            case (int)Helper.ConsoleMenu.AddEmployee:
            name_response:
                Console.WriteLine("Enter employee name: ");
                string nameResponse = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nameResponse) || nameResponse.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(nameResponse, "[^a-zA-Z0-9 -]"))
                {
                    Console.WriteLine("Name field can not be empty,contain symbols or include digits");
                    goto name_response;
                }
            surname_response:
                Console.WriteLine("Enter employee surname");
                string surnameResponse = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(surnameResponse) || surnameResponse.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(surnameResponse, "[^a-zA-Z0-9 -]"))
                {
                    Console.WriteLine("Surname field can not be empty,contain symbols or include digits");
                    goto surname_response;
                }
            salary_response:
                Console.WriteLine("Enter employee Salary");
                double doubleSalaryResponse;
                string salaryResponse = Console.ReadLine();
                if (!double.TryParse(salaryResponse, out doubleSalaryResponse))
                {
                    Console.WriteLine("Incorrect format for Salary"); // show the correct format
                    goto salary_response;
                }
            employee_company:
                Console.WriteLine("Enter your company name: ");
                newCompany.GetAll();
                string companyNameResponse = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(companyNameResponse) || companyNameResponse.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(companyNameResponse, "[^a-zA-Z0-9 &-]"))
                {
                    Console.WriteLine("Company name field can not be empty,contain symbols that are not allowed or include digits");
                    goto employee_company;
                }
                newCompany.GetAllDepartmentsByName(companyNameResponse);
                Console.WriteLine();
                Console.WriteLine("Enter department ID: ");
                int departmentIdResponse;
                string depIdResponse = Console.ReadLine();
                if (!int.TryParse(depIdResponse, out departmentIdResponse))
                {
                    Console.WriteLine("You must enter unique ID of the department from the list");
                }
                newEmployee.Create(nameResponse, surnameResponse, doubleSalaryResponse, companyNameResponse, departmentIdResponse);
                Console.WriteLine("Employee Successfully added!");
                break;

            case (int)Helper.ConsoleMenu.GetListOfAllEmployees:
                newEmployee.GetAll();
                break;

            case (int)Helper.ConsoleMenu.GetListOfEmployeesByDepID:
                Console.WriteLine("Enter Company ID: ");
                newCompany.GetAll();
                int companyIdResponse;
                string compIdResponse = Console.ReadLine();
                if (!int.TryParse(compIdResponse, out companyIdResponse))
                {
                    Console.WriteLine("Choose a valid Company ID");
                }
                Console.WriteLine("Enter Department ID: ");
                newCompany.GetAllDepartmentsByID(companyIdResponse);
                int department_id_response;
                string dep_id_response = Console.ReadLine();
                if (!int.TryParse(dep_id_response, out department_id_response))
                {
                    Console.WriteLine("Choose valid Department ID");
                }
                newEmployee.GetAllByDepartmentId(department_id_response);
                break;
        }
    }
    else
    {
        Console.WriteLine("Ensure that the input follows the correct format.");
    }
}