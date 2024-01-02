# Word Search - .NET and React Application

This is a combining a .NET backend and a React frontend to deliver a Word Search application.

## Prerequisites

- [Node.js](https://nodejs.org/) - version 16.X.X
- [npm](https://www.npmjs.com/)
- [.NET SDK](https://dotnet.microsoft.com/download) - version 7.X.X

## Installation and Configuration

1. Clone this repository.
2. Navigate to the root directory.
3. Follow installation and configuration instructions for both the backend (.NET) and frontend (React) as detailed below.

### Frontend (React)

1. Navigate to the `ClientApp/` directory.
2. Run `npm install` to install dependencies.

The main configuration file to adjust the backend server URL is located at `ClientApp/src/service/finder.js`.

### Backend (.NET)

1. Navigate to the `Finder/` directory.
2. Run `dotnet restore` to install project dependencies.
3. Run `dotnet run` to start the backend server.

## Usage

Once both servers are up, access the application from your web browser using the URL provided by the React development server.

## Features

- Random word search generation.
- Auto search and select a word in the grid.
- Save application state even if page is reloaded.

## Screenshots

<image src='https://i.imgur.com/e6VigaW.png'/>

