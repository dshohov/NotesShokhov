# NotesShokhov
A test task whose purpose is to demonstrate my skills.


## Content
- [About](#About)
- [Architecture](#Architecture)
- [Technologies](#Technologies)
- [Helpers](#Helpers)
- [Contributing](#contributing)
- [The project team](#the-project-team)

## About
Notes apps. In this application, all users who use this application have the same notes and when a note changes, everyone sees it. Authentication is not required to use the application. Each user can create a note and edit any other note as well as their own. The user can search for any note through the search engine.

## Architecture
The project has a monolith architecture since the project is small and it is more convenient to work with it this way rather than using microservices. But if necessary and the project grows, it can easily be transferred to a microservice architecture.

In addition, the project has repositories with basic functions such as adding, changing and retrieving data. All requests to the database are asynchronous, so threads do not stop waiting for a response from the database.

The project uses only one method to obtain data from the database and this is to obtain all data and it is written through IQuerible.

During development, I planned to use a database query to find the necessary notes, and even wrote one. It was an IQuerible type query, I thought for a long time about what to use and chose it because I read that it is better for the database to filter itself and give me only the necessary data, than for it to transmit everything (especially when there are a lot of records). That is, filtering on the database side and sending the required data will load the database less than sending all records from the database and filtering them in the application if I used IEnumerable. But then I realized that why should I make another request to the database if I can search for everything on the client side via js.

Also, when creating the skeleton of the program, I thought about how best to implement the note change. The first option was to receive a note with the same id from the database and check whether the title or text has changed, and if not, then there is no need to update anything and there is no need to make an update request in vain. Then I thought that I would waste time and database resources to get the necessary note. Thanks to this, I came to the conclusion that you can use js and a button to send a changed note and show it to the user only when the field with the name or text of the note changes from the base value. This will allow you to make queries to the database only when something has actually changed.

I also minimized the database work using SignalR. My application implements SignalR, which prevents users from constantly reloading the page while waiting for new notes. Now the user receives new notes from other users through SignalR and does not perform constant queries to the database in the hope of receiving new notes, he will receive them through SignalR. SignalR is also used when changing notes, although now when notes are changed by other users, our user does not receive updated notes online, he only receives a message indicating that some notes have been changed.

I also added a little protection, although it is not mandatory. I added ValidateAntiForgeryToken. If our application had authentication, the number of protective elements would increase.

This is how I solved this problem, if you have any questions, write to me by email: dshohov@gmail.com)

## Technologies
- [C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [EntityFrameworkCore](https://learn.microsoft.com/en-us/ef/core/)
- [PostgreSQL](https://www.postgresql.org/)
- [Bootstrap](https://getbootstrap.com/)
- [Html](https://www.w3schools.com/html/)
- [Css](https://www.w3schools.com/Css/)
- [JavaScript](https://www.javascript.com/)

## Helpers
- [SignalR(For real-time message processing)](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr)

## Contributing
For help in development, please contact me via my e-mail. The subject for help can be a bug or suggestions for improving the code, adding new options for the development of the application.

## The project team
- [Shokhov Dmytro](https://t.me/f_a_g_e) — Back-End Engineer
