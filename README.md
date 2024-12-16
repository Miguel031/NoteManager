# Note App

This is a note-taking application built with a backend in ASP.NET Core and a frontend in React. This README file provides instructions on how to run the application from the project's root directory.

## Prerequisites

Make sure you have the following software installed:
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Node.js and npm](https://nodejs.org/)
- [Git](https://git-scm.com/)

## Backend Setup

1. **Navigate to the Backend Directory**:

    ```bash
    cd Backend
    ```

2. **Install Dependencies**:

    ```bash
    dotnet restore
    ```

3. **Configure Environment Variables (if needed)**:
   
    Ensure to set up the required environment variables, such as the database connection string.

4. **Run the Backend Application**:

    ```bash
    dotnet run
    ```

    The backend application should be running at `https://localhost:5001`.

## Frontend Setup

1. **Navigate to the Frontend Directory**:

    ```bash
    cd ../Frontend
    ```

2. **Install Dependencies**:

    ```bash
    npm install
    ```

3. **Build the Application**:

    ```bash
    npm run build
    ```

4. **Run the Frontend Application**:

    ```bash
    npm start
    ```

    The frontend application should be running at `http://localhost:3000`.

## Default Credentials

To log in to the application, use the following default credentials:

- **Username**: `Admin`
- **Password**: `123`

## Project Structure

```plaintext
.
├── Backend
│   ├── Controllers
│   ├── Models
│   ├── Program.cs
│   ├── Startup.cs
│   └── ...
├── Frontend
│   ├── public
│   ├── src
│   │   ├── components
│   │   ├── App.js
│   │   └── ...
│   ├── package.json
│   └── ...
├── README.md
└── ...
