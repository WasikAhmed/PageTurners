# PageTurners

PageTurners is a web application for managing a collection of books. It allows users to view, create, edit, and delete book entries, including details such as title, author, ISBN, description, and category. The application is built using ASP.NET Core and Entity Framework Core with a MySQL database.

## Table of Contents

- [PageTurners](#pageturners)
  - [Table of Contents](#table-of-contents)
  - [Features](#features)
  - [Technologies](#technologies)
  - [Setup](#setup)
  - [Usage](#usage)
  - [Project Structure](#project-structure)
  - [Contributing](#contributing)
  - [License](#license)

## Features

- View a list of books with details
- Add new books to the collection
- Edit existing book details
- Delete books from the collection
- Categorize books
- Search and filter books by category

## Technologies

- ASP.NET Core
- Entity Framework Core
- MySQL
- Bootstrap
- jQuery

## Setup

1. Clone the repository:
    ```sh
    git clone https://github.com/WasikAhmed/PageTurners.git
    cd PageTurners
    ```

2. Set up the database:
    - Ensure you have MySQL installed and running.
    - Update the connection string in `appsettings.json` in both `PageTurnersWeb` and `DataAccess` projects to match your MySQL configuration.

3. Apply migrations to set up the database schema:
    ```sh
    cd DataAccess
    dotnet ef database update
    ```

4. Build and run the application:
    ```sh
    cd ..
    dotnet build
    dotnet run --project PageTurnersWeb
    ```

## Usage

- Navigate to `https://localhost:5001` in your web browser.
- Use the navigation menu to access different sections of the application.
- Add, edit, or delete books as needed.

## Project Structure
```
.
├── DataAccess                # Data access layer
│   ├── Data                  # Database context
│   ├── Migrations            # Database migrations
│   └── Repository            # Repository pattern implementation
│       └── IRepository       # Interfaces for repository classes
├── PageTurners.Models        # Data models and view models
│   └── ViewModels            # View models for passing data to views
├── PageTurnersWeb            # Main web application
│   ├── Areas                 # Segmented areas for user roles
│   │   ├── Admin             # Admin area
│   │   │   ├── Controllers   # Controllers for admin functionality
│   │   │   └── Views         # Razor views for admin area
│   │   │       ├── Category  # Views for category management
│   │   │       └── Product   # Views for product management
│   │   └── Customer          # Customer-facing area
│   │       ├── Controllers   # Controllers for customer functionality
│   │       └── Views         # Razor views for customer area
│   │           └── Home      # Views for customer home page
│   ├── Properties            # Project properties and configurations
│   ├── Views                 # Shared views across the application
│   │   └── Shared            # Shared partial views and layout files
│   └── wwwroot               # Static files
│       ├── css               # CSS files for styling
│       ├── images            # Image assets
│       │   └── product       # Product images
│       ├── js                # JavaScript files
│       └── lib               # Libraries and frameworks
│           ├── bootstrap     # Bootstrap library
│           │   └── dist      # Bootstrap distribution files
│           ├── jquery        # jQuery library
│           │   └── dist      # jQuery distribution files
│           ├── jquery-validation  # jQuery validation library
│           │   └── dist
│           └── jquery-validation-unobtrusive  # Unobtrusive validation library
└── Utility                   # Utility classes and helper functions

```

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. Ensure your code follows the project's coding standards and includes appropriate tests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.