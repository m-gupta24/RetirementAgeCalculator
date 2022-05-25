# RetirementAgeCalculator
Retirement Age Calculator


The goal of this task is to create an application to calculate the retirement age based on certain input parameters.
Input Parameters
•	Name
•	Age
•	Gender 
•	Monthly savings 
•	Target funds required for retirement. 

The application makes the following assumptions while calculating the age -

- Annual Interest Rate on invested savings - 5% (ROI)
- Current Retirement Savings = 0
- Current age should be 18

First the yearly savings are calculated.
Assuming that the savings are invested with a return of 5% compounded annually.
So, when the yearly savings + annual interest on invested amount becomes equal to the Target Funds, that's the retiremnt age.


High Level Design
![image](https://user-images.githubusercontent.com/106247411/170268629-41f51c95-f3c0-42d6-b061-8c2b9e687ef1.png)

**Identity.WebApi Microservice**-
One API endpoint to authenticate with JWT and accessing a restricted route with JWT:

- /user/authenticate - accepts HTTP POST requests containing the username and password in the body. If the username and password are correct then a JWT authentication token and the user details are returned.
- For simplicity the identity microservice stores the users in memory. 
- New user creation facility is not provided.
- In a production application it is recommended to store user records in a database with hashed passwords.

**RetirementAgeApi Microservice**
Two API endpoints 
**/Retirement/GetRetirementAge** - accepts HTTP  POST requests containing the following detail : Name,	Age, Gender,	Monthly savings, Target funds required for retirement
  Returns - 1) Retirement Age
            2) Retirement Details ID (if the changes are stored in db)
 It provides necessary validations and an option to persist the details. If Save Details - is set to true, the details are stored in DB. The retirement age is then caculated and returened to the client.
     
      
**/Retirement/{id}** - accepts HTTP GET requests. For the ID specified in the request, details are fetched from the DB, Retirement age is calculated and the result along with the other details are returned. If the Id details are not in DB the NOT FOUND is returned.

High Level Design of RetirementAgeApi microservice
![image](https://user-images.githubusercontent.com/106247411/170290937-fb66df6d-c9e8-4291-8fba-550058f8fc5f.png)

**Authorization**
Middleware checks if there is a token in the request Authorization header, and if so attempts to Validate the token using the secret stored in the appsettings.json file. If there is no token in the request header or if validation fails, then the user does not have access to the secure routes.

**Exception Handling**
 The application uses middleware to handle Exceptions as part of your http pipeline.
 
 **RetirementDBScript**
 Create the required schema using the script. \RetirementAgeCalculator\RetirementDBScript.sql

