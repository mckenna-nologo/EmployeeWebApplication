# Employee Management CRUD Application

**Author**: McKenna Makran (Nologo) <br>
**Manager**: Liresh Kaulasar (Nologo) <br>
**Tech Stack**: C#, LINQ, .NET 10, ASP.NET Razor Pages


## Overview
Currently developing a simple Employee Management System that allows users to create, view, update, and delete employee records stored in a database *(temporarily a List)*. The purpose of this project is to demonstrate a solid understanding of database design, CRUD operations, object-oriented programming, and basic user interface development.


## Targets Met 
The application:

*	Utilizes a List that stores Employee Data.
*	Allows users to add new employees.
*	Displays existing employee records.
*	Edits employee details.
*	Deletes employee records.
*	Validates user input before saving to the List.
* Exports data into a CSV.

## CRUD Functions:

### READ
Users are able to view all employees in a table in on the Employees page. Here they can see when the table has been updated/altered.

<img width="1350" height="638" alt="image" src="https://github.com/user-attachments/assets/582189fe-09bf-4c0c-a04b-3f2fdda6bfe5" />

Users are also able to search employees by first name, department or both. On search, they can view individual employee details.

<img width="1006" height="496" alt="image" src="https://github.com/user-attachments/assets/be636b2a-2b11-43a3-9d83-d0232e78e4e5" />

There is also CSV Functionality. Users are able to download a CSV file with the employee data as well as import data from a csv to the employee database.

<img width="896" height="463" alt="image" src="https://github.com/user-attachments/assets/946c77f6-d83c-4a78-a240-a4331dee6606" />

---

### CREATE

Users are able to add a new employee with the following fields:

*	First Name
*	Last Name
*	Email Address
*	Department

With these fields auto-generated with every new user:

*	Employee ID (Auto-generated)
*	Date Created (Auto-generated)

<img width="889" height="562" alt="image" src="https://github.com/user-attachments/assets/a2acf974-24b7-4b60-bb07-a4e54ed1977f" />


Upon Success, you will get a success notification and you will be able to see the updated employee data on the table.

<img width="447" height="158" alt="image" src="https://github.com/user-attachments/assets/76a301ab-8195-4788-84f0-8eafc8f17fe1" />

<img width="1247" height="136" alt="image" src="https://github.com/user-attachments/assets/83fd35ff-818a-46e2-a6e7-a67b519b4689" />


#### Validation Rules

*	First Name IS REQUIRED 
*	Last Name IS REQUIRED 
*	Email Address IS REQUIRED 
*	Department IS REQUIRED

If there is an invalid entry while creating a new employee, you will get an error message. Your entry will not be processed unless valid.

**_Validation Error Example 1:_**

<img width="886" height="470" alt="image" src="https://github.com/user-attachments/assets/6ed7703d-f5e4-44ec-a625-22bf9f1abfa1" />


**_Validation Error Example 2:_**

<img width="445" height="150" alt="image" src="https://github.com/user-attachments/assets/78dc7fac-5b16-4722-ba9a-72184e03dae4" />

---

### UPDATE

Users are able to edit employee information, and save changes to the data.

1. Search through the data to find an employee.

<img width="898" height="332" alt="image" src="https://github.com/user-attachments/assets/ee23a474-0259-42bb-839b-6fe4cc1fa47d" />


2. If the employee exists in the data, their original details will appear in the respective input fields. To update their details, change replace the original details with the new details and press "Update Employee"

<img width="576" height="521" alt="image" src="https://github.com/user-attachments/assets/fb93cb3f-4203-4598-af7c-5f84c757338d" />


3. Upon success, you will receive a success message. And the employee table will refresh for you to see the updated data.

<img width="1047" height="92" alt="image" src="https://github.com/user-attachments/assets/481af6f5-783d-4787-b7a8-70cdc577eaa3" />


---

### DELETE

1. Remove an employee record by entering their employee ID.

<img width="898" height="278" alt="image" src="https://github.com/user-attachments/assets/30dcc337-83ad-4395-8a4e-b4df29e72569" />

2. You will receive a confirmation prompt before deletion.

<img width="444" height="140" alt="image" src="https://github.com/user-attachments/assets/77b37eb1-f7cb-4ebc-b1a8-ae41163afe69" />

3. You will be able to see the updated employee data on the table.

<img width="1095" height="134" alt="image" src="https://github.com/user-attachments/assets/22daa059-ef74-4450-86f2-9d47ee475669" />
