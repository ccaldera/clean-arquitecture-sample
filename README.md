# Contributing to Clean Architecture Sample

We welcome contributions to the Clean Architecture Sample project! To ensure a smooth collaboration, please follow these guidelines:

## Table of Contents

- [Project Structure](#project-structure)
- [Technologies Used](#technologies-used)
- [Creating a New Branch](#creating-a-new-branch)
- [Making Changes](#making-changes)
- [Submitting a Pull Request](#submitting-a-pull-request)
- [Code Review](#code-review)
- [License](#license)

## Project Structure

The project is organized into the following modules:

- **API**: This module contains code related to the user interface and communication with external systems.

- **Application**: The Application module houses business logic and use case implementations.

- **Domain**: Domain entities and objects are placed in this module, encapsulating the core business logic.

- **Infrastructure**: The Infrastructure module manages external dependencies, frameworks, and tools.

- **Persistence**: This module handles data storage and retrieval, including database access and interactions with external data sources.

## Technologies Used

- **Programming Language**: C#
- **Framework**: .NET 8
- **Dependency Management**: NuGet

### NuGet Packages

- **Entity Framework**: [Link to Entity Framework NuGet Package](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
- **FluentValidation**: [Link to FluentValidation NuGet Package](https://www.nuget.org/packages/FluentValidation/)
- **JwtBearer**: [Link to JwtBearer NuGet Package](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/)
- **Swashbuckle (Swagger)**: [Link to Swashbuckle NuGet Package](https://www.nuget.org/packages/Swashbuckle.AspNetCore/)

## Creating a New Branch

1. Fork the repository on GitHub.
2. Clone your forked repository to your local machine:

    ```bash
    git clone https://github.com/your-username/clean-arquitecture-sample.git
    ```

3. Create a new branch for your changes:

    ```bash
    git checkout -b feature/your-feature-name
    ```

   Ensure that your branch name is descriptive and reflects the purpose of your changes.

## Making Changes

1. Make your changes and ensure that the code follows the project's coding standards.
2. Write tests for any new functionality or fix.
3. Update relevant documentation if necessary.

## Submitting a Pull Request

1. Push your changes to your forked repository:

    ```bash
    git push origin feature/your-feature-name
    ```

2. Open a Pull Request (PR) against the original repository (ccaldera/clean-arquitecture-sample).
3. Provide a descriptive title and a clear description of your changes in the PR.
4. Reference any related issues in the PR description using GitHub's syntax (e.g., "Closes #123").
5. Wait for feedback. Your contribution will be reviewed by the maintainers.

## Code Review

Your changes will be reviewed by the project maintainers. Please be responsive to any feedback or change requests. The goal is to ensure code quality, maintainability, and adherence to project standards.

## License

By contributing to this project, you agree that your contributions will be licensed under the [MIT License](LICENSE). This ensures that the project remains open and accessible to everyone.

Thank you for contributing to Clean Architecture Sample!

---

**Note:** Replace placeholders such as "feature/your-feature-name" with actual branch names or details specific to your contribution.