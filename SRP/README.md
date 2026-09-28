# SrpLab — SRP Refactoring

The original repository contains 10 intentionally tricky Single Responsibility Principle violations.

This version refactors the classes by separating independent reasons to change:

- domain/state management
- business rules and calculations
- formatting/export
- messaging/integration side effects

No interfaces are required.

The runner keeps the same meaningful scenarios as the original project.
