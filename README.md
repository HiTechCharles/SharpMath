# SharpMath

A console-based math practice application for Windows that helps users improve their arithmetic skills through interactive problem sets.

## Features

- **Multiple Math Operations**: Practice addition, subtraction, multiplication, division, or mixed problems
- **Customizable Difficulty**: Set the highest number allowed in problems and choose how many problems to solve
- **Text-to-Speech Support**: Optional voice narration of problems and feedback
- **Performance Tracking**: Detailed statistics including:
  - Correct/incorrect answer counts
  - Percentage score
  - Total elapsed time
  - Average time per problem
- **Session Logging**: Automatic saving of practice sessions to:
  - `Last Problem Set.txt` - Most recent session
  - `Full Log.txt` - Complete history of all sessions
- **Smart Problem Generation**:
  - Subtraction problems avoid negative results
  - Division problems always have integer quotients (no remainders)
  - Randomized encouraging messages for correct and incorrect answers

## Requirements

- Windows operating system
- .NET Framework 4.8.1 or higher
- System.Speech library (for text-to-speech functionality)

## Installation

1. Clone the repository:
   ```
   git clone https://github.com/HiTechCharles/SharpMath.git
   ```
2. Open `SharpMath.sln` in Visual Studio
3. Build the solution
4. Run the executable

## Usage

1. Launch the application
2. Select a math operation from the menu:
   - **A** - Addition
   - **S** - Subtraction
   - **M** - Multiplication
   - **D** - Division
   - **E** - Mixed (random operations)
   - **X** - Exit
3. Enter the highest number allowed in problems (e.g., 100)
4. Enter how many problems you want to solve
5. Choose whether to enable text-to-speech (y/n)
6. Solve the problems!
7. Review your performance report card

## Log Files

Session logs are automatically saved to:
```
%USERPROFILE%\Documents\SharpMath\
```

Each log includes:
- Date and time
- Math type
- Difficulty level (highest number)
- Number of problems
- Correct and incorrect counts
- Percentage score
- Total elapsed time
- Average seconds per problem

## Project Structure

- `Program.cs` - Main application logic and user interface
- `ProblemGenerator.cs` - Problem generation algorithms
- `App.config` - Application configuration
- `Properties/AssemblyInfo.cs` - Assembly metadata

## Author

**Charles Martin**

## License

See the repository for license information.

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.
