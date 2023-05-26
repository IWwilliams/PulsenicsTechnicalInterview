# Pulsenics Technical Interview
## ASP.net Core 7.0 MVC Application To Interview Question
### Summary
- I approached this project as an internal application developed for an organization, focusing primarily on technical requirement deliverance. The application features two primary pages, the root page pertaining ot the File model and listing all files in the folder. The alternate page facilitates user management and lists all users.

- To chagne the folder path directory navigate to data/folderPath.cs and update the hardcoded static path string

### Criteria & Instructions
  1) List all files inside the folder
  - Upon program entry users will be met with a table depicting all files within the given folder sturcture. 
  <img width="1512" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/0aa8ef2c-2f42-4e31-b338-5c59e348a92e">

  2) Save file and user into sql database
  - A mysql database was used for this project and contains three tables in the pulsenics schema: Users, Files and UserFiles
      a) Users
          <img width="395" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/9b829ab0-b740-45ed-a0c5-7d60bd25c48d">
      b) Files
          <img width="701" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/b64bb9c9-2c49-44dc-aedf-894c54adce7b">
      c) UserFiles
          <img width="338" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/cc15674e-b63c-4b84-ad69-f1fbada102ac">
          
  3) Search files by filename
  - Search functionality occurs on the root display all files page and allows for term searching and clearing
    <img width="1429" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/4fd40f82-0534-432a-ae72-2ac0414cef2d">

  4) Create & update users (name, email & phone) 
      a) Create User
      - Navigate to the create new link
          <img width="1135" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/b057cf20-8587-4c7e-9e02-7ba91d4e21aa">
      - Enter information and press enter
      
          <img width="550" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/5fa67498-5b09-4356-a701-5f70902334bf">
          
      - Final State User Added
          <img width="1275" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/de2b4db1-23b2-4948-a12e-aed18965adc6">

      b) Update User Information
      - Select edit Profile Infomation to change User Info
      
<img width="1406" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/35736cbd-31a5-46b7-b64d-42a7e008cd08">
      
      - Update user information page
      <img width="541" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/bf446ef5-e910-4c1d-b331-7fdd2d968b0b">

      
      - Final State User Updated
<img width="1236" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/59b625dc-d0de-4638-87b4-52683f585c0c">


  5) Assign files to users
  ** select assign users on root file page
  - Assign User to File Page/ Selecting a User to Assign
    <img width="935" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/10c8bda7-6cbe-4bb0-a33f-2ee1aa0a057f">
  - Final State User Isaiah added to the file
    <img width="1202" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/803b4a3a-490e-41ae-9093-58e3d7d53336">

  9) Reflect changes made outside the application
  - Removing Bass file from the folder:: Before
  <img width="1374" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/8ade206e-6584-4c29-94ac-2d39b88acffe">
 
 - Bass file outside the folder
  <img width="421" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/67e178fa-c648-4f0d-8e89-8d3fb12a8255">
  
  - File removed from application 
  <img width="1403" alt="image" src="https://github.com/IWwilliams/PulsenicsTechnicalInterview/assets/104895866/572cd54a-9ce5-44b1-81f0-9753d3c52049">
