Feature: AddCustomer

A short summary of the feature

@tag1
Scenario: Given a customer repository with the following customers:
| first name   | last name   | DateOfBirth   |  PhoneNumber         |    email        | bank account     |
| meysam       | mozaffari   |  1991-02-02   |   00989182906145     |  meysam@gmail   | 6104337572897112 |
When the Steve James tries to register with email meysam@gmail.com and 
Then the registration should fail with "Email already registered"

Examples:

| email               | 
| me.sam@gmail.com     | 
| meysam+test@gmail.com |
| Meysam@gmail.com      |
| meysam@Gmail.com      |
| meysam@googlemail.com |
