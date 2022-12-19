Feature: AddCustomer

A short summary of the feature

@mytag
Scenario Outline: Add customer
	Given I create a new customer (<FirstName>,<LastName>,<DateOfBirth>,<PhoneNumber>,<Email>,<BankAccountNumber>)
    And ModelState is correct
	Then the system should return <true>

Examples: 
	| FirstName           | LastName| DateOfBirth | PhoneNumber         | Email            | BankAccountNumber  |
	|Meysam               | Amiri   | 1991-02-06  | 09182906145         | Meysam@gmail.com |6104337572897112    |
	|Ali                  | Ahmadi  | 1991-03-06  | 09182906144         | Ali@hahoo.com    |6104337572897114    |
	| Hadi                | Rastgar | 1991-04-09  | 09182906143         | Hadi@gmail.com   |6104337572897115    |
	| Hero                | Niyazi  | 1990-01-02  | 09182906142         | Hero@yahoo.com   |6104337572897116    |
	| Neda                | Moradi  | 1993-03-03  | 09182906141         | Neda@hotmail.com |6104337572897117    |
	| Iman                | Afzali  | 1995-07-08  | 09182906140         | Iman@gmail.com   |6104337572897118    |