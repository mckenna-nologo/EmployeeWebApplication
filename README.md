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
Users are able to view all employees in a table in on the home page. Here they can see when the table has been updated/altered.

<img width="1348" height="583" alt="image" src="https://github.com/user-attachments/assets/fbf8a716-29c6-43b8-b935-045467906907" />

Users are also able to search employees by first name, department or both. On search, they can view individual employee details.

<img width="372" height="264" alt="image" src="https://github.com/user-attachments/assets/10ae787c-2e6a-4113-9af0-1f0c739d1926" />

Users are able to download a CSV file with the data.

<img width="238" height="97" alt="image" src="https://github.com/user-attachments/assets/b5244e1c-7831-42f6-a5bd-503ba56082c4" />

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

<img width="334" height="221" alt="image" src="https://github.com/user-attachments/assets/6f732f3b-050b-4ef5-8d8a-789ce61a44f2" />

Upon Success, you will get a success notification and you will be able to see the updated employee data on the table.

<img width="447" height="158" alt="image" src="https://github.com/user-attachments/assets/76a301ab-8195-4788-84f0-8eafc8f17fe1" />

<img width="1087" height="132" alt="image" src="https://github.com/user-attachments/assets/f61c6bb7-9f7b-4c75-bd77-f55f18ef14db" />


#### Validation Rules

*	First Name IS REQUIRED 
*	Last Name IS REQUIRED 
*	Email Address IS REQUIRED 
*	Department IS REQUIRED

If there is an invalid entry while creating a new employee, you will get an error message. Your entry will not be processed unless valid.

**_Validation Error Example 1:_**

<img width="598" height="212" alt="image" src="https://github.com/user-attachments/assets/0b409b27-843e-4616-81ea-788c5005a413" />

**_Validation Error Example 1:_**

<img width="445" height="150" alt="image" src="https://github.com/user-attachments/assets/78dc7fac-5b16-4722-ba9a-72184e03dae4" />

---

### UPDATE

Users are able to edit employee information, and save changes to the data.

1. Search through the data to find an employee.

<img width="539" height="147" alt="image" src="https://github.com/user-attachments/assets/da7bef65-9211-4230-989f-3894f454c340" />

2. If the employee exists in the data, their original details will appear in the respective input fields. To update their details, change replace the original details with the new details and press "Update Employee"

<img width="434" height="267" alt="image" src="https://github.com/user-attachments/assets/fc3000e6-0ca1-4b82-8e89-a52076dff923" />

3. Upon success, you will receive a success message. And the employee table will refresh for you to see the updated data.

<img width="1074" height="85" alt="image" src="https://github.com/user-attachments/assets/064f3efd-120b-4613-a4dc-22fead6efe20" />

---

### DELETE

1. Remove an employee record by entering their employee ID.

<img width="319" height="134" alt="image" src="https://github.com/user-attachments/assets/51526d13-3b55-4543-aa1f-b79702d118bf" />

2. You will receive a confirmation prompt before deletion.

<img width="444" height="140" alt="image" src="https://github.com/user-attachments/assets/77b37eb1-f7cb-4ebc-b1a8-ae41163afe69" />

3. You will be able to see the updated employee data on the table.

<img width="1115" height="116" alt="image" src="https://github.com/user-attachments/assets/8743ba22-b4a5-414b-ae16-2a0e455f62fb" />
