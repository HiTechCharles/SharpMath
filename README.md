# SharpMath

SharpMath is a console-based math practice application implemented in C#. The project in this repository targets .NET 10 and is maintained for development with Visual Studio 2026 and the dotnet CLI.

## Features

- Multiple math operations: addition, subtraction, multiplication, division, or mixed problems
- Customizable difficulty and problem count
- Optional text-to-speech where supported by the OS/runtime
- Performance tracking (counts, percentage score, elapsed time, average time)
- Session logging (recent session and full history)

## Requirements

- .NET 10 SDK
- Visual Studio 2026 or later (optional)

Note: text-to-speech features may require additional platform-specific packages or OS support.

## Installation & Build

1. Clone the repository:

   git clone https://github.com/HiTechCharles/SharpMath.git

2. From the repo root build with the dotnet CLI:

   dotnet build

Or open `SharpMath.slnx` in Visual Studio 2026 and build from the IDE.

## Run

If there is an executable project in the solution, run it with:

dotnet run --project <path-to-project>

Replace <path-to-project> with the folder or .csproj of the executable project.

## Logs

Session logs are saved to a documents folder by default (check the app settings or source for the exact path on your system).

## Project structure (examples)

- SharpMath/SharpMath.cs - core application or entry point
- SharpMath.slnx - solution file

## Contributing

Contributions and bug reports are welcome. Please open issues or pull requests on the repository.

## License

Add a LICENSE file to define reuse terms for this repository.
