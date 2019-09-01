# Ticketing-System

## Descripton

Ticketing System Web API that supports opening a new ticket, closing and editing a ticket.
Users can register, login and view their tickets, add a new ticket, edit or close an existing ticket.
Ticket types, service types, priorities, and ticket statuses can be extended.
There is one admin with user privileges, but on top of that it can add new users, add new ticket properties (mentioned above), 
add and change UI settings (allow users to edit customer name, ticket type or service type)

API endpoints can be viewed with SwaggerUI on the index page (default opening page)

## Configuration

Don't forget to change the `connection string` from the `appsettings.json`

## Default user

When the app is run for the first time, it seeds some data into the database (`priorities`, `ticket types`, `service types`, `statuses`) and a user with admin rights.
the `username` is *admin* and the `password` is *password123*

Every other user created using the `register` endpoint is created with `user` privileges.

## Client

The cliend is an Angular 8 webapp and can be found [here](https://github.com/s00190970/Ticketing-System-Frontend)
