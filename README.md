Software Requirement Specification (SRS)   
of   
E-Gram Panchayat                                        
***************************************************************************  
College Name:United college of Enginering & Research  
Project Manager’s Name:- Er. Adarsh Dubey
Team Leader Name:- Er. Atul Pandey  
Project Co-ordinatior’s Name:-   Er.Vibhu Dubey 
The major goal of this project is to improve the delivery of citizen services in 
the village by computerizing applications for gram panchayat services. Gram 
panchayat is a decentralized institution that manages applications and 
provides information about gram panchayat services. The suggested system 
will allow users to submit applications for various services and track their 
progress. The suggested system E-Services for E-Gram Panchayat develops a 
web application with the goal of providing government information about 
services or schemes, and public users can apply for services using an online 
application. Admin and staff will manage the application for approval and 
creation of the scheme. 
 Villages will get the information about government services and related 
documents digitally from gram panchayat.  
 To provide the list of benefits of different schemes. 
 To provide anytime, anywhere access. 
 The Transparency of communication between gram panchayat and 
service users.  
 To complain by the villagers. 
1  
 To feedback from the villagers. 
 Villages & gramsevak work under and get information on their account .  
 This is to reduce the time of villagers who visits the panchayat office 
frequently to get the information about scheme /services  
3) Modules 
1.  
2.  
Home page (General Zone) 
About us 
3.  
4.  
Registration (Beneficiary) 
Contact Us 
5.  
6.  
Enquiry 
Login As (Beneficiary, Admin) 
7.  
8.  
Apply Services 
Beneficiary Home Dashboard 
Search Services 
9.  
10.  Feedback 
11.  My Application Status 
12.  My profile 
13.  Change Password 
14.  Logout 
15.  View Services 
16.  Admin Dashboard 
17.  Add Services 
18.  Manage Beneficiary Details 
19.  Add Notification 
20.  Manage Notification 
21.  Manage Services 
22.  Manage Feedback 
23.  Change Password 
24.  Send Email 
25.  Logout 
2  
 
 
3  
  
 
 
 
  
4) Flow Diagrams:-   
  
 
 
 
 
 
 
 
   
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 4) Architecture of E-Gram Panchayat 
E-Gram Panchayat 
General Zone User Zone Admin Zone 
 
 
4  
  
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
5  
  
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 5) 0 Level DFD 
 6) 1 Level DFD 
 
 
6  
  
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
  
 
 
 
 
 
 
 
 
 
 
 
 
 
E- Gram Panchayat 
General Zone (Home Page) 
Registration 
Login 
Admin 
Dashboard  User 
Dashboard 
Search Service 
Give Feedback 
Manage profile 
Send Email Feedback Mgnt Manage User Manage Staff Manage Service Add Service Enquiry Mgnt 
7) Tools & Technologies Used:- 
i) 
ii) 
iii) 
iv) 
v) 
IDE:-  
Visual Studio 2022 
Database Management Tool:-  
Sql Server Management Studio 2019 
Technology & Framework:- 
.NET MVC Framework  
Programming Language:- 
C# 
Database:- 
MSSQL 
This page will be in the General zone: - 
1. Home 
7  
 
 
8  
  
 
2. About Us 
 
 
 
 
 
 
 
 
 
 
3. Contact Us 
 
 
 
 
 
 
 
 
 
 
 
4. Registration 
 
 
 
 
 
 
 
 
 
 
 
 
9  
  
 
 
5. Services 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
6. Login 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
10  
  
 
This page will be in the User zone: - 
 
1. Dashboard 
 
 
 
 
 
 
 
 
 
 
2. Feedback 
 
 
 
 
 
 
 
 
 
 
3. My Profile 
 
 
 
 
 
 
 
 
 
 
11  
  
 
4. Change Password 
 
 
 
 
 
 
 
 
 
 
5. View Services 
 
 
 
 
 
 
 
 
 
 
 
6. Apply Services 
 
 
 
 
 
 
 
 
 
 
 
 
12  
  
 
This page will be in the Admin zone: - 
 
1. Admin Dashboard 
 
 
 
 
 
 
 
 
 
 
2. Add Services 
 
 
 
 
 
 
 
 
 
 
3. Manage Beneficiary 
 
 
 
 
 
 
 
 
 
 
13  
  
 
4. Manage Feedback 
 
 
 
 
 
 
 
 
 
 
5. Manage Enquiry 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
14  
  
 
            
  
  
i) Table Name:Beneficiary_Registration  
  
  
 
 
ii) Table Name:- EnquiryMaster 
  
Sno  Variable Name  DataType  Size  Remark 
1  EnquiryId  int -  PK(AI) 
2  Name  varchar  50   
3  MobileNo  varchar  20   
4  Topic varchar  200   
Sno  Variable Name  DataType  Size  Remark 
1 Name  varchar  70   
2 Gender varchar 12  
3 FatherName varchar 70  
4 MobileNo varchar  20   
5 EmailId varchar  200 PK 
6 AdharNo Varchar 20  
7 RelatedStateId int   
8 RelatedCityId int    
9  RelatedBlockId  int   
10  Village varchar  100   
11  Address varchar 200  
12 PinCode int   
13 BeneficiaryPicName varchar 200  
14 AdharPicName varchar 200  
15 RegDT datetime   
16 IsDel bit   
 8) Data Dictionary 
 
 
15  
  
5 Enquiry_Msg varchar MAX  
6 Enquiry_dt datetime 30  
  
 
iii)  Table Name:-Login  
  
Sno  Variable  DataType  Size  Remark 
1 UserId varchar 200  
2 UserType varchar 200  
3 Pass varchar 250  
4 Status bit   
5 LCount int    Pk 
6  LastLogin_dt datetime   
 
 
iv) Table Name:- Service_Master 
  
i) Table Name:- Apply_Master 
Sno  Column Name  DataType  Size  Remark 
1  Id   int -  PK(AI) 
2  Servises varchar  50   
3  Title  varchar  50   
4  Description Datetime 300  
5  Service_Dt varchar  30  
Sno  Column Name  DataType  Size  Remark 
1  AId   int -  PK(AI) 
2  Name varchar  100   
3  Email varchar  200   
4  MobileNo Datetime 100  
5  Services varchar  100  
6 Apply_DT datetime   
 
 
16  
  
 
 
ii) Table Name:-Notification  
       
Sno  Variable  DataType  Size  Remark 
1  NId Int  -  PK(AI) 
2  Message varchar  MAX  
3  N_Dt datetime 30  
   
 
iii) Table Name:- Feedback_Master 
 
 
Team Members and their Roles 
 
SNO. Name Job Role Description 
1. Drishti Singh Software Developer Designer/Programmer 
2. Pradeep Yadav Software Developer Designer/Programmer 
3. Satyam Singh Software Developer Designer/Programmer 
4. Prnaw Kesharwani Software Developer Designer/Programmer 
5. Vivek Singh Software Developer Designer/Programmer 
6. Abhishek Kumar Software Developer Designer/Programmer 
7. Anuj Tiwari Software Developer Designer 
8. Sudhanshu Software Developer Designer 
 
 
 
 
Sno  Column Name  DataType  Size  Remark 
1  FeedbackId  int -  PK(AI) 
2  Feedback_Title varchar  100  
3  Feedback_Detail varchar  100  
4  UserId varchar 200 FK to BeneficiaryMaster 
(EmailId Column) 
5  Feedback_DT Datetime -   
8) Summary of architecture :   
General Zone:   
Home:-  It should contain the name and logo ,light weight slider ,menu bar 
,Header site title ,News & Notification ,Social media icon and enquiry 
popup.  
Register: - In this page user can register themselves online.  
This page contains following attributes:  
Name ,Father’s Name ,Gender ,DOB ,Current Address, Contact no, Email 
id ,Profile Pic ,Enter Captcha Code ,password ,confirm password, The login 
id will be email id of user.  
Login:-  This page is used by the user and provider for login into their 
zone.It should contain following properties:  
UserId,Password,forget password,new user Sign up here.  
Change Password:-  This page user can change your password .It should 
contain following properties:  
Old Password,New Password and Confirm password  
User:  
Home:- This page will work as a dashboard.  
Search Services:- In this page user can search all services which provided 
by the government of india.  
Apply Services:- In this page user can apply all services which provides by 
the government of india in rural area/gram panchayat.  
My application status:- In this page user can check our application 
status.That is valid application or not .  
My Profile:- In this page user can see our all detail related to us and edit 
information.Like name,mobile no,email etc.  
17  
Logout:- In this page user can logout from website.  
Officer/Admin:  
Home:- This page will work as a dashboard.  
Login:- This page is used by the admin and provider for login into their  
zone.It should contain following properties:-  
AdminId,password,forget password,new admin sign up here.  
Create Services:- In this page admin can create one or many services for 
user that allow government of india .  
Update/Delete Services:- In this page admin can modify or update and 
delete services which is expire or not approved by the government of 
india.  
Update application status:- In this page admin can modify or update user 
or farmer details .  
Logout:- In this page admin can logout from website to secure our website    
18  
 
 
19  
  
 
 
Grantt Chart 
 
 
  
  
  
           
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
   
 
 
 
 
 
 
 
 
 
 
Conclusion:    
This web based application will be helpful to the villagers of that village; it will 
bring transparency, accountability, and efficiency in administration. Document 
and their related record will be available on this application. It helps to make 
administration more accountable as well as more transparent. The above survey 
and proposed system will help the Gram panchayat system to work efficiently. 
This system provides ID and password for the villager. Account history provides 
information about the services that are previously submitted by the villagers. 
This will help to minimize corruption in the system, and also save the effort and 
t
 ime of common man and government officers. 
Future Scope:  
This system has been designed keeping in mind the requirements of gram 
panchayat staff and enables the admin and staff of panchayat to make entries in 
the database about villagers, personal details, and their related services. This 
system also provides him the authority to manipulate his account. We can add 
much more feature in the system i.e., alert system, receive notification to user 
and gram panchayat staff about some action, we can provide a transaction 
system in which all the money related work handled will save time and will 
reduce corruption. By using this system data collected from different gram 
panchayat will be helpful for implementing different schemes and will help in 
natural calamities and data can be useful in other fields.    
Project Status:-  80% Completed    
20  
